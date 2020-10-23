using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;
using Database.Classes;
using Pata_WPF_Commerce.Core;
using Pata_WPF_Commerce.Repositories;
using Pata_WPF_Commerce.ViewModels.DataBinding;
using Pata_WPF_Commerce.Views;
using Achat = Database.Classes.Achat;
using Stock = Database.Classes.Stock;

namespace Pata_WPF_Commerce.ViewModels
{
	public class AchatViewModel : BaseProperty
	{
		private DataAchat _itemInForm; // données bindée dans le formulaire
		private DataStock _selectedItemInDgv; // données dans la dgv
		private Fournisseur _selectedItemInComboBox; // données dans la combo box

		private readonly StocksRepository _stockRepository = StocksRepository.Instance;
		private readonly FournisseursRepository _fournisseurRepository = FournisseursRepository.Instance;

		private readonly IList<Acheter> _acheter = new List<Acheter>(); // liste des éléments à acheter dans la rtb

		public ObservableCollection<DataStock> Stocks { get; set; } // données bindée dans la dgv
		public ObservableCollection<Fournisseur> Fournisseurs { get; set; } // données bindée dans la dgv du client sélectionné

		public DataAchat ItemInForm // données bindées dans le formulaire
		{
			get => _itemInForm;
			set => AssignField(ref _itemInForm, value, MethodBase.GetCurrentMethod().Name);
		}

		public DataStock SelectedItem // élément sélectionné dans la dgv
		{
			get => _selectedItemInDgv;
			set => AssignField(ref _selectedItemInDgv, value, MethodBase.GetCurrentMethod().Name);
		}

		public Fournisseur SelectedProvider // élément sélectionné dans la combo box
		{
			get => _selectedItemInComboBox;
			set => AssignField(ref _selectedItemInComboBox, value, MethodBase.GetCurrentMethod().Name);
		}

		public BaseCommand CommandConfirm { get; set; }

		public AchatViewModel()
		{
			ItemInForm = new DataAchat();

			Stocks = LoadStocks(); // récupère les stocks dans la bdd
			Fournisseurs = LoadFournisseurs(); // récupère les fournisseurs dans la bdd

			// bind les commandes au xaml
			CommandConfirm = new BaseCommand(Confirm);
		}

		/**
		 * <summary>Charge les items de la bdd pour hydrater la dgv</summary>
		 * <returns>Retourne une liste d'éléments</returns>
		 */
		private ObservableCollection<DataStock> LoadStocks()
		{
			ObservableCollection<DataStock> list = new ObservableCollection<DataStock>();
			IList<Stock> tempsList = _stockRepository.Lire(); // lit la bdd

			// injecte dans la liste
			foreach (var stock in tempsList)
				list.Add(Map(stock, new DataStock()));

			return list;
		}

		/**
		 * <summary>Charge les items de la bdd pour hydrater la dgv</summary>
		 * <returns>Retourne une liste d'éléments</returns>
		 */
		private ObservableCollection<Fournisseur> LoadFournisseurs()
		{
			ObservableCollection<Fournisseur> list = new ObservableCollection<Fournisseur>();
			IList<Fournisseur> tempsList = _fournisseurRepository.Lire(); // lit la bdd

			// injecte dans la liste
			foreach (var element in tempsList)
				list.Add(element);

			return list;
		}

		/**
		 * <summary>Adapte le formulaire en fonction de l'élément sélectionné</summary>
		 */
		public void ChangedSelected()
		{
			ItemInForm.Stock = Map(SelectedItem, ItemInForm.Stock);
			ItemInForm.Stock.PrixAchat = Money.Round(ItemInForm.Stock.PrixAchat);
		}

		/**
		 * <summary>Adapte le prix total lorsque la quantité change</summary>
		 * <param name="quantiteText">Quantité sous format texte</param>
		 */
		public void ChangeQuantite(string quantiteText)
		{
			if(ItemInForm.Stock == null) return;

			if (decimal.TryParse(quantiteText, out var quantite))
				ItemInForm.PrixTotal = Money.Round(quantite * ItemInForm.Stock.PrixAchat);
		}

		/**
		 * <summary>Ajoute un élément aux items à acheter</summary>
		 * <returns>Un document formatté contenant la liste des items à acheter</returns>
		 */
		public FlowDocument AddItem()
		{
			if (ItemInForm == null || SelectedProvider == null || ItemInForm.Quantite < 1)
			{
				MessageBox.Show("Veuillez remplir correctement tous les champs !");
				return Viewer.LastDocument;
			}

			Acheter elementExistant = _acheter.FirstOrDefault(item => item.Stock.Id == SelectedItem.Id);
			if (elementExistant == null)
			{
				Acheter acheter = new Acheter()
				{
					Quantite = ItemInForm.Quantite,
					Stock = ItemInForm.Stock
				};
				_acheter.Add(acheter);
			}
			else
				_acheter.First(item => item == elementExistant).Quantite += ItemInForm.Quantite;

			return GenerateDocument();
		}

		/**
		 * <summary>Supprime un élément des items à acheter</summary>
		 * <returns>Un document formatté contenant la liste des items à acheter</returns>
		 */
		public FlowDocument DeleteItem()
		{
			if (SelectedItem == null || SelectedProvider == null)
				return Viewer.LastDocument;

			var elementToDelete = _acheter.FirstOrDefault(item => SelectedItem.Id == item.Stock.Id);
			_acheter.Remove(elementToDelete);

			return GenerateDocument();
		}

		/**
		 * <summary>Modifie un élément des items à acheter</summary>
		 * <returns>Un document formatté contenant la liste des items à acheter</returns>
		 */
		public FlowDocument ModifyItem()
		{
			if (ItemInForm == null || SelectedProvider == null || ItemInForm.Quantite < 1)
				return Viewer.LastDocument;

			_acheter.First(item => SelectedItem.Id == item.Stock.Id).Quantite = ItemInForm.Quantite;

			return GenerateDocument();
		}

		/**
		 * <summary>Génère le document des items à acheter</summary>
		 * <returns>Un document formatté contenant la liste des items à acheter</returns>
		 */
		private FlowDocument GenerateDocument()
		{
			Viewer viewer = new Viewer();
			viewer.SetTitle($"Acheter chez {SelectedProvider?.Nom} pour {SommeTotale()}");

			foreach (var achat in _acheter)
				viewer.AddElement($"{achat.Quantite}X {achat.Stock.Nom} à {achat.Stock.PrixAchat}€");

			return viewer.Execute();
		}

		/**
		 * Calcule la somme totale des éléments à commander
		 * <returns>La somme formattée</returns>
		 */
		private string SommeTotale()
		{
			decimal total = 0;
			foreach (var achat in _acheter)
				total += achat.Quantite * achat.Stock.PrixAchat;

			return Money.Display(total);
		}

		/**
		 * <summary>Actions à exécuter lors de la confirmation de la commande</summary>
		 */
		private async void Confirm()
		{
			if (_acheter.Count < 1)
			{
				MessageBox.Show("Veuillez commander des articles...");
				return;
			}

			// crée la commande
			CommandesAchat commandeAchat = new CommandesAchat()
			{
				IdFournisseur = SelectedProvider.Id,
				Date = DateTime.Now
			};
			int idCommande = await CommandesAchatsRepository.Instance.AjouterAsync(commandeAchat);

			// ajoute tous les éléments de la commande
			foreach (var achatForm in _acheter)
			{
				Achat achat = new Achat()
				{
					IdStock = achatForm.Stock.Id,
					Prix = achatForm.Stock.PrixAchat,
					Quantite = achatForm.Quantite,
					IdCommande = idCommande
				};

				await DetailAchatsRepository.Instance.AjouterAsync(achat);

				// modifie la quantité des stocks actuels
				Stock stock = _stockRepository.LireId(achat.IdStock);
				stock.QuantiteActuelle += achat.Quantite;
				await _stockRepository.ModifierAsync(stock);

				// modifie dans la datagrid
				Stocks.First(item => item.Id == stock.Id).QuantiteActuelle = stock.QuantiteActuelle;
			}

			Facture();
		}

		private void Facture()
		{
			HtmlView html = new HtmlView("Commande");

			html.GenerateColumn("Nom", "Quantité", "Prix unitaire", "Prix final"); // génère les colonnes

			// rempli les colonnes
			// données tableau
			foreach (var item in _acheter)
			{
				html.GenerateBody(item.Stock.Nom);
				html.GenerateBody(item.Quantite.ToString());
				html.GenerateBody(item.Stock.PrixAchat.ToString(CultureInfo.InvariantCulture));
				html.GenerateBody((item.Stock.PrixAchat * item.Quantite).ToString(CultureInfo.InvariantCulture));
			}

			html.SaveTo("FactureFournisseur", $"facture_{SelectedProvider.Nom}_{html.DateNow()}");

			Html fenetre = new Html(html.SourceCode);
			fenetre.ShowDialog();
		}

		private class Acheter
		{
			public Stock Stock { get; set; }
			public int Quantite { get; set; }
		}
	}
}
