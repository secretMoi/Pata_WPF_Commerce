using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Database.Classes;
using Pata_WPF_Commerce.Repositories;
using Pata_WPF_Commerce.ViewModels.DataBinding;

namespace Pata_WPF_Commerce.ViewModels
{
	public class CategorieViewModel : BaseProperty
	{
		private DataCategorie _itemInForm; // données bindée dans le formulaire
		private CategorieComposant _selectedItem; // données du Fournisseur sélectionné dans la dgv

		private readonly CategoriesRepository _categoriesRepository = CategoriesRepository.Instance;

		public DataCategorie ItemInForm // données bindée dans le formulaire
		{
			get => _itemInForm;
			set => AssignField(ref _itemInForm, value, MethodBase.GetCurrentMethod().Name);
		}

		public CategorieComposant SelectedItem // données du Fournisseur sélectionné dans la dgv
		{
			get => _selectedItem;
			set => AssignField(ref _selectedItem, value, MethodBase.GetCurrentMethod().Name);
		}

		public ObservableCollection<DataCategorie>
			Categories { get; set; } // données bindée dans la dgv du Fournisseur sélectionné

		public CategorieViewModel()
		{
			Categories = LoadCategories(); // récupère les Fournisseur dans la bdd

			ItemInForm = Map(Categories.FirstOrDefault(), ItemInForm); // injecte le premier Fournisseur trouvé

			// bind les commandes au xaml
			CommandeConfirm = new BaseCommand(Confirm);
			CommandAdd = new BaseCommand(Add);
			CommandModify = new BaseCommand(Modify);
		}

		/**
		 * <summary>Charge les Fournisseurs depuis la bdd vers la dgv</summary>
		 * <returns>Une liste des Fournisseurs bindable</returns>
		 */
		private ObservableCollection<DataCategorie> LoadCategories()
		{
			ObservableCollection<DataCategorie> list = new ObservableCollection<DataCategorie>();
			IList<CategorieComposant> temp = _categoriesRepository.Lire();

			foreach (var cateogrie in temp)
				list.Add(Map(cateogrie, new DataCategorie()));

			return list;
		}

		/**
		 * <summary>Fais correspondre le contenu de l'élément sélectionné de la dgv dans le formulaire</summary>
		 */
		public void ChangedSelectedItem()
		{
			ItemInForm = Map(SelectedItem, ItemInForm);
		}

		public void Confirm()
		{
			ItemInForm.Nom = "coucou";
		}

		/**
		 * <summary>Ajoute un élément à la dgv et la bdd</summary>
		 */
		private async void Add()
		{
			// ajoute le nouveau fournisseur à la bdd
			CategorieComposant model = new CategorieComposant();
			model = Map(ItemInForm, model);
			int res = await _categoriesRepository.AjouterAsync(model);

			// ajoute ce nouveau fournisseur à la dgv
			model = await _categoriesRepository.LireIdAsync(res);
			Categories.Add(Map(model, new DataCategorie()));
		}

		/**
		 * <summary>Modifie un élément de la dgv et la bdd</summary>
		 */
		private async void Modify()
		{
			// map le Fournisseur entre le formulaire et le model
			CategorieComposant model = new CategorieComposant();
			model = Map(ItemInForm, model);

			await _categoriesRepository.ModifierAsync(model); // ajoute à la bdd

			// rafraichit la dgv
			int index = Categories.IndexOf(Map(SelectedItem, new DataCategorie()));
			Categories[index] = Map(model, new DataCategorie());
		}

		#region Commands

		public BaseCommand CommandeConfirm { get; set; }
		public BaseCommand CommandAdd { get; set; }
		public BaseCommand CommandModify { get; set; }

		#endregion
	}
}