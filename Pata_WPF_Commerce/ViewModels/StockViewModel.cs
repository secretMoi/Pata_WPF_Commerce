using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
		private Stock _selectedItemInDgv; // données du client sélectionné dans la dgv
		private readonly StocksRepository _repository = StocksRepository.Instance;

		public DataStock ItemInForm // données bindée dans le formulaire
		{
			get => _itemInForm;
			set => AssignField(ref _itemInForm, value, MethodBase.GetCurrentMethod().Name);
		}

		public Stock SelectedItem // données du client sélectionné dans la dgv
		{
			get => _selectedItemInDgv;
			set => AssignField(ref _selectedItemInDgv, value, MethodBase.GetCurrentMethod().Name);
		}

		public ObservableCollection<Stock> Stocks { get; set; } // données bindée dans la dgv du client sélectionné

		public StockViewModel()
		{
			Stocks = LoadStocks(); // récupère les stocks dans la bdd

			ItemInForm = Map(Stocks.FirstOrDefault(), ItemInForm); // injecte le premier stock trouvé

			// bind les commandes au xaml
			CommandAdd = new BaseCommand(Add);
			CommandDelete = new BaseCommand(Delete);
			CommandModify = new BaseCommand(Modify);
			CommandHtml = new BaseCommand(HtmlWindow);
		}

		/**
		 * <summary>Ajoute un élément à la dgv et la bdd</summary>
		 */
		private async void Add()
		{
			// ajoute le nouveau client à la bdd
			Stock model = new Stock();
			model = Map(ItemInForm, model);
			await _repository.AjouterAsync(model);

			// ajoute ce nouveau client à la dgv
			Stocks.Add(model);
		}

		/**
		 * <summary>Supprime un élément de la dgv et la bdd</summary>
		 */
		private async void Delete()
		{
			// todo supprimer ou gérer les clé étrangères
			if (SelectedItem == null) return; // si aucun n'est sélectionné on quitte

			// confirmation de suppression
			var result = MessageBox.Show(
				$"Voulez-vous vraiment supprimer le stock {SelectedItem.Nom} ?",
				"Suppression", MessageBoxButton.YesNo
			);

			if (result == MessageBoxResult.No) return;

			// supprime l'item de la bdd
			await _repository.SupprimerAsync(SelectedItem.Id);

			// supprime l'item de la dgv
			Stocks.Remove(SelectedItem);
		}

		/**
		 * <summary>Modifie un élément de la dgv et la bdd</summary>
		 */
		private async void Modify()
		{
			// map le client entre le formulaire et le model
			Stock model = new Stock();
			model = Map(ItemInForm, model);

			await _repository.ModifierAsync(model); // ajoute à la bdd

			// rafraichit la dgv
			int index = Stocks.IndexOf(SelectedItem);
			Stocks[index] = model;
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
		private ObservableCollection<Stock> LoadStocks()
		{
			ObservableCollection<Stock> list = new ObservableCollection<Stock>();
			IList<Stock> tempsList = _repository.Lire(); // lit la bdd

			// injecte dans la liste
			foreach (var stock in tempsList)
				list.Add(stock);

			return list;
		}

		public void ChangedSelectedClient()
		{
			ItemInForm = Map(SelectedItem, ItemInForm);
		}

		public void AdaptBackColor(DataGridRowEventArgs e)
		{
			if (((Stock)e.Row.DataContext).QuantiteMin > ((Stock)e.Row.DataContext).QuantiteActuelle)
				e.Row.Background = new SolidColorBrush(Color.FromRgb(255, 85, 66));
		}

		public BaseCommand CommandAdd { get; set; }
		public BaseCommand CommandDelete { get; set; }
		public BaseCommand CommandModify { get; set; }
		public BaseCommand CommandHtml { get; set; }
	}
}

