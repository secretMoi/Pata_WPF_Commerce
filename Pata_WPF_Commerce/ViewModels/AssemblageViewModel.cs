using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Database.Classes;
using Pata_WPF_Commerce.Repositories;
using Pata_WPF_Commerce.ViewModels.DataBinding;

namespace Pata_WPF_Commerce.ViewModels
{
	public class AssemblageViewModel : BaseProperty    
	{
		private DataAssemblage _selectedItemInDgv; // données dans la dgv
		private DataAssemblage _itemInForm; // données bindée dans le formulaire

		public ObservableCollection<DataAssemblage> Pcs { get; set; } // données bindée dans la dgv

		// repo
		private readonly StocksRepository _stockRepository = StocksRepository.Instance;
		private readonly CategoriesRepository _categoriesRepository = CategoriesRepository.Instance;
		private readonly PcRepository _pcRepository = PcRepository.Instance;

		private IList<CategorieComposant> _categories;
		private IList<Stock> _stocks;

		// données dans les combo box
		private Stock _selectedProcesseur;
		private Stock _selectedRam;
		private Stock _selectedCarteMere;
		private Stock _selectedCarteGraphique;
		private Stock _selectedDisqueDur1;
		private Stock _selectedDisqueDur2;
		private Stock _selectedBoitier;
		private Stock _selectedAlimenation;
		private Stock _selectedRefroidissement;

		public ObservableCollection<Stock> Processeurs { get; set; } // données bindée dans la combobox
		public ObservableCollection<Stock> Ram { get; set; } // données bindée dans la combobox
		public ObservableCollection<Stock> CarteMere { get; set; } // données bindée dans la combobox
		public ObservableCollection<Stock> CarteGraphique { get; set; } // données bindée dans la combobox
		public ObservableCollection<Stock> DisqueDur1 { get; set; } // données bindée dans la combobox
		public ObservableCollection<Stock> DisqueDur2 { get; set; } // données bindée dans la combobox
		public ObservableCollection<Stock> Boitier { get; set; } // données bindée dans la combobox
		public ObservableCollection<Stock> Alimenation { get; set; } // données bindée dans la combobox
		public ObservableCollection<Stock> Refroidissement { get; set; } // données bindée dans la combobox

		public DataAssemblage ItemInForm // données bindées dans le formulaire
		{
			get => _itemInForm;
			set => AssignField(ref _itemInForm, value, MethodBase.GetCurrentMethod().Name);
		}

		public DataAssemblage SelectedItem // élément sélectionné dans la dgv
		{
			get => _selectedItemInDgv;
			set => AssignField(ref _selectedItemInDgv, value, MethodBase.GetCurrentMethod().Name);
		}

		public Stock SelectedProcesseur // processeur sélectionné dans la combo box
		{
			get => _selectedProcesseur;
			set => AssignField(ref _selectedProcesseur, value, MethodBase.GetCurrentMethod().Name);
		}

		public Stock SelectedCarteMere // item sélectionné dans la combo box
		{
			get => _selectedCarteMere;
			set => AssignField(ref _selectedCarteMere, value, MethodBase.GetCurrentMethod().Name);
		}

		public Stock SelectedRam // item sélectionné dans la combo box
		{
			get => _selectedRam;
			set => AssignField(ref _selectedRam, value, MethodBase.GetCurrentMethod().Name);
		}

		public Stock SelectedCarteGraphique // item sélectionné dans la combo box
		{
			get => _selectedCarteGraphique;
			set => AssignField(ref _selectedCarteGraphique, value, MethodBase.GetCurrentMethod().Name);
		}

		public Stock SelectedDisqueDur1 // item sélectionné dans la combo box
		{
			get => _selectedDisqueDur1;
			set => AssignField(ref _selectedDisqueDur1, value, MethodBase.GetCurrentMethod().Name);
		}

		public Stock SelectedDisqueDur2 // item sélectionné dans la combo box
		{
			get => _selectedDisqueDur2;
			set => AssignField(ref _selectedDisqueDur2, value, MethodBase.GetCurrentMethod().Name);
		}

		public Stock SelectedBoitier // item sélectionné dans la combo box
		{
			get => _selectedBoitier;
			set => AssignField(ref _selectedBoitier, value, MethodBase.GetCurrentMethod().Name);
		}

		public Stock SelectedAlimentation // item sélectionné dans la combo box
		{
			get => _selectedAlimenation;
			set => AssignField(ref _selectedAlimenation, value, MethodBase.GetCurrentMethod().Name);
		}

		public Stock SelectedRefroidissement // item sélectionné dans la combo box
		{
			get => _selectedRefroidissement;
			set => AssignField(ref _selectedRefroidissement, value, MethodBase.GetCurrentMethod().Name);
		}

		public AssemblageViewModel()
		{
			ItemInForm = new DataAssemblage();

			Pcs = LoadPcs(); // récupère les Pcs dans la bdd
			Processeurs = LoadPart("Processeur"); // récupère les parties dans la bdd

			// bind les commandes au xaml
			//CommandConfirm = new BaseCommand(Confirm);
		}

		private ObservableCollection<DataAssemblage> LoadPcs()
		{
			ObservableCollection<DataAssemblage> list = new ObservableCollection<DataAssemblage>();
			IList<Pc> tempsList = _pcRepository.Lire(); // lit la bdd

			// injecte dans la liste
			foreach (var stock in tempsList)
				list.Add(Map(stock, new DataAssemblage()));

			return list;
		}

		/**
		 * <summary>Charge les items de la bdd pour hydrater la dgv</summary>
		 * <returns>Retourne une liste d'éléments</returns>
		 */
		private ObservableCollection<Stock> LoadPart(string part)
		{
			// récupère les catégories
			if (_categories == null)
				_categories = _categoriesRepository.Lire();

			// récupère les stocks
			if (_stocks == null)
				_stocks = _stockRepository.Lire();

			// sélectionne la catégorie demandée
			CategorieComposant category = _categories.First(item => item.Nom == part);

			IList<Stock> stocks = _stocks.Where(stock => stock.Categorie == category.Id).ToList();

			ObservableCollection<Stock> list = new ObservableCollection<Stock>();

			// injecte dans la liste
			foreach (var stock in stocks)
				list.Add(stock);

			return list;
		}
	}
}
