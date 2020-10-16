﻿using System.Collections.Generic;
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
	public class AchatViewModel : BaseProperty
	{
		private DataAchat _itemInForm; // données bindée dans le formulaire
		private Stock _selectedItemInDgv; // données dans la dgv
		private Fournisseur _selectedItemInComboBox; // données dans la combo box

		private readonly StocksRepository _stockRepository = StocksRepository.Instance;
		private readonly FournisseursRepository _fournisseurRepository = FournisseursRepository.Instance;

		private readonly IList<Acheter> _acheter = new List<Acheter>();

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
					Fournisseur = SelectedProvider,
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
			if (SelectedItem == null)
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
			if(_acheter.Count < 1) return Viewer.LastDocument;

			Viewer viewer = new Viewer();
			viewer.SetTitle("Acheter chez " + _acheter.Last().Fournisseur.Nom);

			foreach (var achat in _acheter)
				viewer.AddElement($"{achat.Quantite}X {achat.Stock.Nom} à {achat.Stock.PrixAchat}€");

			return viewer.Execute();
		}

		private class Acheter
		{
			public Stock Stock { get; set; }
			public int Quantite { get; set; }
			public Fournisseur Fournisseur { get; set; }
		}
	}
}
