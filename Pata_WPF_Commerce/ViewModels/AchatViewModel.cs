using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Database.Classes;
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

		public ObservableCollection<Stock> Stocks { get; set; } // données bindée dans la dgv du client sélectionné
		public ObservableCollection<Fournisseur> Fournisseurs { get; set; } // données bindée dans la dgv du client sélectionné

		public DataAchat ItemInForm // données bindée dans le formulaire
		{
			get => _itemInForm;
			set => AssignField(ref _itemInForm, value, MethodBase.GetCurrentMethod().Name);
		}

		public Stock SelectedItem // données du client sélectionné dans la dgv
		{
			get => _selectedItemInDgv;
			set => AssignField(ref _selectedItemInDgv, value, MethodBase.GetCurrentMethod().Name);
		}

		public Fournisseur SelectedProvider // données du client sélectionné dans la combo box
		{
			get => _selectedItemInComboBox;
			set => AssignField(ref _selectedItemInComboBox, value, MethodBase.GetCurrentMethod().Name);
		}

		public AchatViewModel()
		{
			Stocks = LoadStocks(); // récupère les stocks dans la bdd
			Fournisseurs = LoadFournisseurs(); // récupère les stocks dans la bdd

			ItemInForm = Map(Stocks.FirstOrDefault(), ItemInForm); // injecte le premier stock trouvé

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

		public void ChangedSelected()
		{
			ItemInForm.Stock = Map(SelectedItem, ItemInForm.Stock);
		}

		public void ChangeQuantite(string quantiteText)
		{
			if (decimal.TryParse(quantiteText, out var quantite))
			{
				ItemInForm.PrixTotal = quantite;
			}
		}

		public BaseCommand CommandAdd { get; set; }
		public BaseCommand CommandModify { get; set; }
	}
}
