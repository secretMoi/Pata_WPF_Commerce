using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using Database.Classes;
using Pata_WPF_Commerce.Core;
using Pata_WPF_Commerce.Repositories;
using Pata_WPF_Commerce.ViewModels.DataBinding;

namespace Pata_WPF_Commerce.ViewModels
{
	public class AssemblageViewModel : BaseProperty    
	{
		private DataAssemblage _selectedItemInDgv; // données dans la dgv
		private DataAssemblage _itemInForm; // données bindée dans le formulaire

		public  ObservableCollection<DataStock> ItemsList { get; set; } // données bindée dans la listbox

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
		public ObservableCollection<Stock> Alimentation { get; set; } // données bindée dans la combobox
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
			set
			{
				AssignField(ref _selectedProcesseur, value, MethodBase.GetCurrentMethod().Name);
				UpdatePrice(SelectedProcesseur);
			}
		}

		public Stock SelectedCarteMere // item sélectionné dans la combo box
		{
			get => _selectedCarteMere;
			set
			{
				AssignField(ref _selectedCarteMere, value, MethodBase.GetCurrentMethod().Name);
				UpdatePrice(SelectedCarteMere);
			}
		}

		public Stock SelectedRam // item sélectionné dans la combo box
		{
			get => _selectedRam;
			set
			{
				AssignField(ref _selectedRam, value, MethodBase.GetCurrentMethod().Name);
				UpdatePrice(SelectedRam);
			}
		}

		public Stock SelectedCarteGraphique // item sélectionné dans la combo box
		{
			get => _selectedCarteGraphique;
			set
			{
				AssignField(ref _selectedCarteGraphique, value, MethodBase.GetCurrentMethod().Name);
				UpdatePrice(SelectedCarteGraphique);
			}
		}

		public Stock SelectedDisqueDur1 // item sélectionné dans la combo box
		{
			get => _selectedDisqueDur1;
			set
			{
				AssignField(ref _selectedDisqueDur1, value, MethodBase.GetCurrentMethod().Name);
				UpdatePrice(SelectedDisqueDur1);
			}
		}

		public Stock SelectedDisqueDur2 // item sélectionné dans la combo box
		{
			get => _selectedDisqueDur2;
			set
			{
				AssignField(ref _selectedDisqueDur2, value, MethodBase.GetCurrentMethod().Name);
				UpdatePrice(SelectedDisqueDur2);
			}
		}

		public Stock SelectedBoitier // item sélectionné dans la combo box
		{
			get => _selectedBoitier;
			set
			{
				AssignField(ref _selectedBoitier, value, MethodBase.GetCurrentMethod().Name);
				UpdatePrice(SelectedBoitier);
			}
		}

		public Stock SelectedAlimentation // item sélectionné dans la combo box
		{
			get => _selectedAlimenation;
			set
			{
				AssignField(ref _selectedAlimenation, value, MethodBase.GetCurrentMethod().Name);
				UpdatePrice(SelectedAlimentation);
			}
		}

		public Stock SelectedRefroidissement // item sélectionné dans la combo box
		{
			get => _selectedRefroidissement;
			set
			{
				AssignField(ref _selectedRefroidissement, value, MethodBase.GetCurrentMethod().Name);
				UpdatePrice(SelectedRefroidissement);
			}
		}

		public AssemblageViewModel()
		{
			ItemInForm = new DataAssemblage();

			ItemsList = new ObservableCollection<DataStock>();

			Pcs = LoadPcs(); // récupère les Pcs dans la bdd

			Processeurs = LoadPart("Processeur"); // récupère les parties dans la bdd
			Ram = LoadPart("RAM"); // récupère les parties dans la bdd
			CarteGraphique = LoadPart("Carte graphique"); // récupère les parties dans la bdd
			Alimentation = LoadPart("Alimentation"); // récupère les parties dans la bdd
			Boitier = LoadPart("Boitier"); // récupère les parties dans la bdd
			DisqueDur1 = LoadPart("Disque dur 1"); // récupère les parties dans la bdd
			DisqueDur2 = LoadPart("Disque dur 2"); // récupère les parties dans la bdd
			Refroidissement = LoadPart("Refroidissement"); // récupère les parties dans la bdd
			CarteMere = LoadPart("Carte mère"); // récupère les parties dans la bdd


			// bind les commandes au xaml
			CommandAdd = new BaseCommand(Add);
			CommandModify = new BaseCommand(Modify);
			CommandDelete = new BaseCommand(Delete);
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
			CategorieComposant category = _categories.FirstOrDefault(item => item.Nom == part);

			if (category == null)
				return null;

			IList<Stock> stocks = _stocks.Where(stock => stock.Categorie == category.Id).ToList();

			ObservableCollection<Stock> list = new ObservableCollection<Stock>();

			// injecte dans la liste
			foreach (var stock in stocks)
				list.Add(stock);

			return list;
		}

		/**
		 * <summary>Actions à effectuer lors du changement de sélection dans la dgv</summary>
		 */
		public void ChangedSelectedItem()
		{
			ItemInForm = Map(SelectedItem, new DataAssemblage());

			SelectedProcesseur = Processeurs.FirstOrDefault(item => item.Id == SelectedItem.Processeur);
			SelectedCarteMere = CarteMere.FirstOrDefault(item => item.Id == SelectedItem.CarteMere);
			SelectedCarteGraphique = CarteGraphique.FirstOrDefault(item => item.Id == SelectedItem.CarteGraphique);

			SelectedRam = Ram.FirstOrDefault(item => item.Id == SelectedItem.Ram);
			SelectedRefroidissement = Refroidissement.FirstOrDefault(item => item.Id == SelectedItem.Refroidissement);
			SelectedAlimentation = Alimentation.FirstOrDefault(item => item.Id == SelectedItem.Alimentation);

			SelectedDisqueDur1 = DisqueDur1.FirstOrDefault(item => item.Id == SelectedItem.DisqueDur1);
			SelectedDisqueDur2 = DisqueDur2.FirstOrDefault(item => item.Id == SelectedItem.DisqueDur2);
			SelectedBoitier = Boitier.FirstOrDefault(item => item.Id == SelectedItem.Boitier);

		}

		private void UpdatePrice(Stock stock)
		{
			// si l'élément existe déjà
			var elementInList = ItemsList.FirstOrDefault(item => item.Categorie == stock.Categorie);
			if (elementInList != null)
			{
				int index = ItemsList.IndexOf(elementInList);
				ItemsList[index] = Map(stock, new DataStock());
			}
			else
				ItemsList.Add(Map(stock, new DataStock()));

			decimal prixTotal = 0;
			foreach (var item in ItemsList)
				prixTotal += item.PrixVente;

			ItemInForm.Prix = Money.Round(prixTotal);
		}

		/**
		 * Ajout un pc assemblé
		 */
		private async void Add()
		{
			if (!ChampsValides() && ItemInForm.Nom != "" && ItemInForm.PrixPromo > 0)
			{
				MessageBox.Show("Veuillez remplir les champs correctement !");
				return;
			}

			Pc pc = Map(ItemInForm, new Pc());
			pc.Processeur = SelectedProcesseur.Id;
			pc.CarteMere = SelectedCarteMere.Id;
			pc.CarteGraphique = SelectedCarteGraphique.Id;

			pc.Ram = SelectedRam.Id;
			pc.Refroidissement = SelectedRefroidissement.Id;
			pc.Alimentation = SelectedAlimentation.Id;

			pc.Boitier = SelectedBoitier.Id;
			pc.DisqueDur1 = SelectedDisqueDur1.Id;
			if(SelectedDisqueDur2 != null)
				pc.DisqueDur2 = SelectedDisqueDur2.Id;

			await _pcRepository.AjouterAsync(pc);

			MessageBox.Show($"Configuration {pc.Nom} ajoutée");
		}

		/**
		 * <summary>Modifie l'assemblage sélectionné</summary>
		 */
		private async void Modify()
		{
			if (!ChampsValides() && ItemInForm.Nom != "" && ItemInForm.PrixPromo > 0)
			{
				MessageBox.Show("Veuillez remplir les champs correctement !");
				return;
			}

			Pc pc = Map(ItemInForm, new Pc());
			pc.Processeur = SelectedProcesseur.Id;
			pc.CarteMere = SelectedCarteMere.Id;
			pc.CarteGraphique = SelectedCarteGraphique.Id;

			pc.Ram = SelectedRam.Id;
			pc.Refroidissement = SelectedRefroidissement.Id;
			pc.Alimentation = SelectedAlimentation.Id;

			pc.Boitier = SelectedBoitier.Id;
			pc.DisqueDur1 = SelectedDisqueDur1.Id;
			if (SelectedDisqueDur2 != null)
				pc.DisqueDur2 = SelectedDisqueDur2.Id;

			await _pcRepository.ModifierAsync(pc);

			MessageBox.Show($"Configuration {pc.Nom} modifiée");
		}

		/**
		 * <summary>Permet de supprimer l'assemblage sélectionné</summary>
		 */
		private async void Delete()
		{
			if (SelectedItem == null)
			{
				MessageBox.Show("Veuillez sélectionner une config à supprimer");
				return;
			}

			await _pcRepository.SupprimerAsync(SelectedItem.Id);

			Pcs.Remove(SelectedItem);
		}

		/**
		 * <summary>Vérifie que les combo box soient bien remplies</summary>
		 * <returns>true si elles le sont toutes, false si au moins une ne l'est pas</returns>
		 */
		private bool ChampsValides()
		{
			return
				SelectedProcesseur != null && SelectedRam != null && SelectedAlimentation != null &&
				SelectedCarteMere != null && SelectedCarteGraphique != null && SelectedBoitier != null &&
				SelectedDisqueDur1 != null && SelectedRefroidissement != null;
		}

		public BaseCommand CommandAdd { get; set; }
		public BaseCommand CommandModify { get; set; }
		public BaseCommand CommandDelete { get; set; }
	}
}
