using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Database.Classes;
using Pata_WPF_Commerce.Core;
using Pata_WPF_Commerce.Repositories;
using Pata_WPF_Commerce.ViewModels.DataBinding;
using Pata_WPF_Commerce.Views;
using Stock = Database.Classes.Stock;

namespace Pata_WPF_Commerce.ViewModels
{
	public class StockViewModel : BaseProperty
	{
		private DataStock _itemInForm; // données bindée dans le formulaire
		private DataStock _selectedItemInDgv; // données du stock sélectionné dans la dgv
		private DataCategorie _selectedItemInComboBox; // données dans la combo box

		private readonly StocksRepository _repository = StocksRepository.Instance;
		private readonly CategoriesRepository _categoriesRepository = CategoriesRepository.Instance;

		public DataStock ItemInForm // données bindée dans le formulaire
		{
			get => _itemInForm;
			set => AssignField(ref _itemInForm, value, MethodBase.GetCurrentMethod().Name);
		}

		public DataStock SelectedItem // données du stock sélectionné dans la dgv
		{
			get => _selectedItemInDgv;
			set => AssignField(ref _selectedItemInDgv, value, MethodBase.GetCurrentMethod().Name);
		}

		public DataCategorie SelectedCategory // élément sélectionné dans la combo box
		{
			get => _selectedItemInComboBox;
			set => AssignField(ref _selectedItemInComboBox, value, MethodBase.GetCurrentMethod().Name);
		}

		public ObservableCollection<DataCategorie> Categories { get; set; } // données bindée dans la dgv du stock sélectionné

		public ObservableCollection<DataStock> Stocks { get; set; } // données bindée dans la dgv du stock sélectionné

		public StockViewModel()
		{
			Stocks = LoadStocks(); // récupère les stocks dans la bdd
			Categories = LoadCategories(); // récupère les Categories dans la bdd

			ItemInForm = Stocks.FirstOrDefault(); // injecte le premier stock trouvé
			SelectedCategory = Categories.First(item => item.Id == ItemInForm.Categorie);

			// bind les commandes au xaml
			CommandAdd = new BaseCommand(Add);
			CommandModify = new BaseCommand(Modify);
			CommandHtml = new BaseCommand(HtmlWindow);
		}

		/**
		 * <summary>Ajoute un élément à la dgv et la bdd</summary>
		 */
		private async void Add()
		{
			if (!ChampsValides())
			{
				MessageBox.Show("Veuillez remplir les champs correctement !");
				return;
			}

			// ajoute le nouveau stock à la bdd
			Stock model = Map(ItemInForm, new Stock());

			await _repository.AjouterAsync(model);

			// ajoute ce nouveau stock à la dgv
			Stocks.Add(Map(model, new DataStock()));
		}

		/**
		 * <summary>Modifie un élément de la dgv et la bdd</summary>
		 */
		private async void Modify()
		{
			if (!ChampsValides())
			{
				MessageBox.Show("Veuillez remplir les champs correctement !");
				return;
			}

			// map le stock entre le formulaire et le model
			Stock model = ConvertToModel(ItemInForm);

			await _repository.ModifierAsync(model); // ajoute à la bdd
		}

		/**
		 * <summary>Vérifie que les champs soient biens remplis</summary>
		 * <returns>true si ils le sont, false si au moins un ne l'est pas</returns>
		 */
		private bool ChampsValides()
		{
			return ItemInForm.Nom != "" && ItemInForm.QuantiteActuelle > 0 && ItemInForm.QuantiteMin > 0 &&
			       ItemInForm.Categorie > 0 && ItemInForm.PrixVente > 0 && ItemInForm.PrixAchat > 0;
		}

		/**
		 * <summary>Converti un model bindé en un nouveau model</summary>
		 * <returns>Renvoie un model pour la bdd hydraté</returns>
		 */
		private Stock ConvertToModel(DataStock dataStock)
		{
			return new Stock()
			{
				Id = dataStock.Id,
				Nom = dataStock.Nom,
				QuantiteActuelle = dataStock.QuantiteActuelle,
				QuantiteMin = dataStock.QuantiteMin,
				PrixAchat = dataStock.PrixAchat,
				PrixVente = dataStock.PrixVente,
				Categorie = dataStock.Categorie,
			};
		}

		/**
		 * <summary>Affiche l'état des stocks dans une vue html</summary>
		 */
		private void HtmlWindow()
		{
			HtmlView html = new HtmlView("Etat des stocks");

			html.GenerateColumn("Nom", "Quantité actuelle", "Quantité minimale", "Prix de vente"); // génère les colonnes

			// rempli les colonnes
			// données tableau
			foreach (var item in Stocks)
			{
				html.GenerateBody(item.Nom);
				html.GenerateBody(item.QuantiteActuelle.ToString());
				html.GenerateBody(item.QuantiteMin.ToString());
				html.GenerateBody(Money.Display(item.PrixVente));
			}

			Html fenetre = new Html(html.SourceCode);
			fenetre.ShowDialog();
		}

		/**
		 * <summary>Charge les items de la bdd pour hydrater la dgv</summary>
		 * <returns>Retourne une liste d'éléments</returns>
		 */
		private ObservableCollection<DataStock> LoadStocks()
		{
			ObservableCollection<DataStock> list = new ObservableCollection<DataStock>();
			IList<Stock> tempsList = _repository.Lire(); // lit la bdd

			// injecte dans la liste
			foreach (var stock in tempsList)
				list.Add(Map(stock, new DataStock()));

			return list;
		}

		/**
		 * <summary>Charge les items de la bdd pour hydrater la combobox</summary>
		 * <returns>Retourne une liste d'éléments</returns>
		 */
		private ObservableCollection<DataCategorie> LoadCategories()
		{
			ObservableCollection<DataCategorie> list = new ObservableCollection<DataCategorie>();
			IList<CategorieComposant> tempsList = _categoriesRepository.Lire(); // lit la bdd

			// injecte dans la liste
			foreach (var element in tempsList)
				list.Add(Map(element, new DataCategorie()));

			return list;
		}

		/**
		 * <summary>Actions à effectuer lors du changement de sélection dans la dgv</summary>
		 */
		public void ChangedSelectedItem()
		{
			ItemInForm = SelectedItem;
			SelectedCategory = Categories.First(item => item.Id == SelectedItem.Categorie);
		}


		/**
		 * <summary>Change la couleur de fond des stocks n'étant pas en quantité suffisante</summary>
		 * <param name="e">Contient les information de la ligne de la datagrid</param>
		 */
		public void AdaptBackColor(DataGridRowEventArgs e)
		{
			if (((DataStock)e.Row.DataContext).QuantiteMin > ((DataStock)e.Row.DataContext).QuantiteActuelle)
				e.Row.Background = new SolidColorBrush(Color.FromRgb(255, 85, 66));
		}

		public BaseCommand CommandAdd { get; set; }
		public BaseCommand CommandModify { get; set; }
		public BaseCommand CommandHtml { get; set; }
	}
}

