using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Database.Classes;
using Pata_WPF_Commerce.Repositories;
using Pata_WPF_Commerce.ViewModels.DataBinding;

namespace Pata_WPF_Commerce.ViewModels
{
	public class FournisseurViewModel : BaseProperty
	{
		private DataFournisseur _itemInForm; // données bindée dans le formulaire
		private DataFournisseur _selectedItem; // données du Fournisseur sélectionné dans la dgv

		public DataFournisseur ItemInForm // données bindée dans le formulaire
		{
			get => _itemInForm;
			set => AssignField(ref _itemInForm, value, MethodBase.GetCurrentMethod().Name);
		}

		public DataFournisseur SelectedItem // données du Fournisseur sélectionné dans la dgv
		{
			get => _selectedItem;
			set => AssignField(ref _selectedItem, value, MethodBase.GetCurrentMethod().Name);
		}

		public ObservableCollection<DataFournisseur> Fournisseurs { get; set; } // données bindée dans la dgv du Fournisseur sélectionné

		public FournisseurViewModel()
		{
			Fournisseurs = LoadFournisseurs(); // récupère les Fournisseur dans la bdd

			ItemInForm = Map(Fournisseurs.FirstOrDefault(), ItemInForm); // injecte le premier Fournisseur trouvé

			// bind les commandes au xaml
			CommandeConfirm = new BaseCommand(Confirm);
			CommandAdd = new BaseCommand(Add);
			CommandModify = new BaseCommand(Modify);
		}

		/**
		 * <summary>Charge les Fournisseurs depuis la bdd vers la dgv</summary>
		 * <returns>Une liste des Fournisseurs bindable</returns>
		 */
		private ObservableCollection<DataFournisseur> LoadFournisseurs()
		{
			ObservableCollection<DataFournisseur> list = new ObservableCollection<DataFournisseur>();
			IList<Fournisseur> temp = FournisseursRepository.Instance.Lire();

			foreach (var fournisseur in temp)
				list.Add(Map(fournisseur, new DataFournisseur()));

			return list;
		}

		/**
		 * <summary>Fais correspondre le contenu de l'élément sélectionné de la dgv dans le formulaire</summary>
		 */
		public void ChangedSelectedItem()
		{
			ItemInForm = Map(SelectedItem, new DataFournisseur());
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
			Fournisseur model = new Fournisseur();
			model = Map(ItemInForm, model);
			await FournisseursRepository.Instance.AjouterAsync(model);

			// ajoute ce nouveau fournisseur à la dgv
			Fournisseurs.Add(Map(model, new DataFournisseur()));
		}

		/**
		 * <summary>Modifie un élément de la dgv et la bdd</summary>
		 */
		private async void Modify()
		{
			// map le Fournisseur entre le formulaire et le model
			Fournisseur model = new Fournisseur();
			model = Map(ItemInForm, model);

			await FournisseursRepository.Instance.ModifierAsync(model); // ajoute à la bdd

			// rafraichit la dgv
			Fournisseurs.First(item => item.Id == model.Id).Nom = model.Nom;
		}

		#region Commands
		public BaseCommand CommandeConfirm { get; set; }
		public BaseCommand CommandAdd { get; set; }
		public BaseCommand CommandModify { get; set; }
		#endregion
	}
}
