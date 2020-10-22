using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;
using Database.Classes;
using Pata_WPF_Commerce.Core;
using Pata_WPF_Commerce.Repositories;
using Pata_WPF_Commerce.ViewModels.DataBinding;

namespace Pata_WPF_Commerce.ViewModels
{
	public class VenteViewModel : BaseProperty
	{
		private DataVente _itemInForm; // données bindée dans le formulaire
		private Stock _selectedItemInDgv; // données dans la dgv
		private Client _selectedItemInComboBox; // données dans la combo box

		private readonly StocksRepository _stockRepository = StocksRepository.Instance;
		private readonly ClientsRepository _clientRepository = ClientsRepository.Instance;
		private readonly DetailVentesRepository _detailVentesRepository = DetailVentesRepository.Instance;
		private readonly CommandesVentesRepository _commandesVentesRepository = CommandesVentesRepository.Instance;

		private readonly IList<Vendre> _acheter = new List<Vendre>(); // liste des éléments à acheter dans la rtb

		public ObservableCollection<Stock> Stocks { get; set; } // données bindée dans la dgv
		public ObservableCollection<Client> Clients { get; set; } // données bindée dans la dgv du client sélectionné

		public DataVente ItemInForm // données bindées dans le formulaire
		{
			get => _itemInForm;
			set => AssignField(ref _itemInForm, value, MethodBase.GetCurrentMethod().Name);
		}

		public Stock SelectedItem // élément sélectionné dans la dgv
		{
			get => _selectedItemInDgv;
			set => AssignField(ref _selectedItemInDgv, value, MethodBase.GetCurrentMethod().Name);
		}

		public Client SelectedClient // élément sélectionné dans la combo box
		{
			get => _selectedItemInComboBox;
			set => AssignField(ref _selectedItemInComboBox, value, MethodBase.GetCurrentMethod().Name);
		}

		public BaseCommand CommandConfirm { get; set; }

		public VenteViewModel()
		{
			ItemInForm = new DataVente();

			Stocks = LoadStocks(); // récupère les stocks dans la bdd
			Clients = LoadClients(); // récupère les fournisseurs dans la bdd

			// bind les commandes au xaml
			CommandConfirm = new BaseCommand(Confirm);
		}

		/**
		 * <summary>Charge les items de la bdd pour hydrater la dgv</summary>
		 * <returns>Retourne une liste d'éléments</returns>
		 */
		private ObservableCollection<Stock> LoadStocks()
		{
			ObservableCollection<Stock> list = new ObservableCollection<Stock>();
			IList<Stock> tempsList = _stockRepository.Lire(); // lit la bdd

			// injecte dans la liste
			foreach (var stock in tempsList)
				list.Add(stock);

			return list;
		}

		/**
		 * <summary>Charge les items de la bdd pour hydrater la dgv</summary>
		 * <returns>Retourne une liste d'éléments</returns>
		 */
		private ObservableCollection<Client> LoadClients()
		{
			ObservableCollection<Client> list = new ObservableCollection<Client>();
			IList<Client> tempsList = _clientRepository.Lire(); // lit la bdd

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
			if (ItemInForm.Stock == null) return;

			if (decimal.TryParse(quantiteText, out var quantite))
				ItemInForm.PrixTotal = Money.Round(quantite * ItemInForm.Stock.PrixAchat);
		}

		/**
		 * <summary>Ajoute un élément aux items à acheter</summary>
		 * <returns>Un document formatté contenant la liste des items à acheter</returns>
		 */
		public FlowDocument AddItem()
		{
			if (ItemInForm == null || SelectedClient == null || ItemInForm.Quantite < 1)
			{
				MessageBox.Show("Veuillez remplir correctement tous les champs !");
				return Viewer.LastDocument;
			}

			Vendre elementExistant = _acheter.FirstOrDefault(item => item.Stock.Id == SelectedItem.Id);
			if (elementExistant == null)
			{
				Vendre acheter = new Vendre()
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
			if (SelectedItem == null || SelectedClient == null)
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
			if (ItemInForm == null || SelectedClient == null || ItemInForm.Quantite < 1)
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
			//if(_acheter.Count < 1) return Viewer.LastDocument;

			Viewer viewer = new Viewer();
			viewer.SetTitle($"{SelectedClient?.Nom} achète pour {SommeTotale()}");

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

			// vérifie que l'on a assez de stock pour la vente
			foreach (var achatForm in _acheter)
			{
				Stock stockInDb = _stockRepository.LireId(achatForm.Stock.Id);
				if (achatForm.Quantite > stockInDb.QuantiteActuelle)
				{
					MessageBox.Show($"Vous ne pouvez pas commander plus de {stockInDb.QuantiteActuelle} exemplaires de {stockInDb.Nom}");
					return;
				}
			}

			// cée la commande
			CommandesVente commande = new CommandesVente()
			{
				IdClient = SelectedClient.Id,
				Date = DateTime.Now
			};
			int idCommande = await _commandesVentesRepository.AjouterAsync(commande);

			// ajoute tous les éléments de la commande
			foreach (var achatForm in _acheter)
			{
				Vente vente = new Vente()
				{
					IdStock = achatForm.Stock.Id,
					Prix = achatForm.Stock.PrixAchat,
					Quantite = achatForm.Quantite,
					IdCommande = idCommande
				};

				await _detailVentesRepository.AjouterAsync(vente);

				// modifie la quantité des stocks actuels
				Stock stock = _stockRepository.LireId(vente.IdStock);
				stock.QuantiteActuelle -= vente.Quantite;
				await _stockRepository.ModifierAsync(stock);
			}

			MessageBox.Show("Commande effectuée par " + SelectedClient.Nom);
		}

		private class Vendre
		{
			public Stock Stock { get; set; }
			public int Quantite { get; set; }
		}
	}
}
