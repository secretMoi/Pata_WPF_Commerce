using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;
using Database.Classes;
using Pata_WPF_Commerce.Core;
using Pata_WPF_Commerce.Repositories;
using Pata_WPF_Commerce.ViewModels.DataBinding;

namespace Pata_WPF_Commerce.ViewModels
{
	public class AchatViewModel : BaseProperty
	{
		private DataAchat _itemInForm; // données bindée dans le formulaire
		private Stock _selectedItemInDgv; // données dans la dgv
		private Fournisseur _selectedItemInComboBox; // données dans la combo box

		private readonly StocksRepository _stockRepository = StocksRepository.Instance;
		private readonly FournisseursRepository _fournisseurRepository = FournisseursRepository.Instance;

		private IList<Acheter> _acheter = new List<Acheter>();

		public ObservableCollection<Stock> Stocks { get; set; } // données bindée dans la dgv
		public ObservableCollection<Fournisseur> Fournisseurs { get; set; } // données bindée dans la dgv du client sélectionné

		public DataAchat ItemInForm // données bindées dans le formulaire
		{
			get => _itemInForm;
			set => AssignField(ref _itemInForm, value, MethodBase.GetCurrentMethod().Name);
		}

		public Stock SelectedItem // élément sélectionné dans la dgv
		{
			get => _selectedItemInDgv;
			set => AssignField(ref _selectedItemInDgv, value, MethodBase.GetCurrentMethod().Name);
		}

		public Fournisseur SelectedProvider // élément sélectionné dans la combo box
		{
			get => _selectedItemInComboBox;
			set => AssignField(ref _selectedItemInComboBox, value, MethodBase.GetCurrentMethod().Name);
		}

		public AchatViewModel()
		{
			ItemInForm = new DataAchat();

			Stocks = LoadStocks(); // récupère les stocks dans la bdd
			Fournisseurs = LoadFournisseurs(); // récupère les fournisseurs dans la bdd

			// bind les commandes au xaml
			/*CommandAdd = new BaseCommand(Add);
			CommandModify = new BaseCommand(Modify);*/
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
		 */
		public void ChangeQuantite(string quantiteText)
		{
			if(ItemInForm.Stock == null) return;

			if (decimal.TryParse(quantiteText, out var quantite))
				ItemInForm.PrixTotal = Money.Round(quantite * ItemInForm.Stock.PrixAchat);
		}

		/**
		 * <summary>Génère la liste des items à acheter</summary>
		 * <returns>Un document formatté contenant la liste des items à acheter</returns>
		 */
		public FlowDocument AddItem()
		{
			if (ItemInForm == null || SelectedProvider == null)
			{
				MessageBox.Show("Veuillez remplir tous les champs !");
				return new FlowDocument();
			}

			Acheter acheter = new Acheter()
			{
				Fournisseur = SelectedProvider,
				Quantite = ItemInForm.Quantite,
				Stock = ItemInForm.Stock
			};
			_acheter.Add(acheter);

			Viewer viewer = new Viewer();
			viewer.SetTitle("Acheter chez " + acheter.Fournisseur.Nom);

			foreach (var achat in _acheter)
			{
				viewer.AddElement($"{achat.Quantite}X {achat.Stock.Nom} à {achat.Stock.PrixAchat}€");
			}

			return viewer.Execute();
		}

		public BaseCommand CommandAdd { get; set; }
		public BaseCommand CommandModify { get; set; }

		private class Acheter
		{
			public Stock Stock { get; set; }
			public int Quantite { get; set; }
			public Fournisseur Fournisseur { get; set; }
		}
	}
}
