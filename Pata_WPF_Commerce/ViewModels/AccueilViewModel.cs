using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Pata_WPF_Commerce.Core;
using Pata_WPF_Commerce.Repositories;
using Pata_WPF_Commerce.ViewModels.DataBinding;
using Pata_WPF_Commerce.Views;

namespace Pata_WPF_Commerce.ViewModels
{
	public class AccueilViewModel : BaseProperty
	{
		private DataAccueil _item = new DataAccueil();
		private readonly CommandesVentesRepository _commandesVentesRepository = CommandesVentesRepository.Instance;
		private readonly DetailVentesRepository _ventesRepository = DetailVentesRepository.Instance;

		public DataAccueil Item // élément sélectionné dans la dgv
		{
			get => _item;
			set => AssignField(ref _item, value, MethodBase.GetCurrentMethod().Name);
		}

		public AccueilViewModel()
		{
			Item.ChiffreAffaire = Money.Display(ChiffreAffaireHebdomadaire());

			// bind les commandes au xaml
			CommandClients = new BaseCommand(Clients);
			CommandFournisseurs = new BaseCommand(Fournisseurs);
			CommandStocks = new BaseCommand(Stocks);
			CommandAchats = new BaseCommand(Achats);
			CommandVentes = new BaseCommand(Ventes);
			CommandCheckStock = new BaseCommand(CheckStock);
			CommandCategories = new BaseCommand(Categories);
		}

		/**
		 * <summary>Calcul le chiffre d'affaire de la semaine</summary>
		 */
		private decimal ChiffreAffaireHebdomadaire()
		{
			// récupère la liste des ventes des 7 derniers jours
			var commandesVentes =
				_commandesVentesRepository.Lire().Where(item => item.Date >= DateTime.Now.AddDays(-7)).ToList();

			decimal chiffreAffaire = 0;
			foreach (var commande in commandesVentes)
			{
				// récupère les articles vendus par chaque commande
				var ventes = _ventesRepository.Lire().Where(item => item.IdCommande == commande.Id).ToList();

				// pour chaque élément vendu
				foreach (var vente in ventes)
					chiffreAffaire += vente.Prix * vente.Quantite;
			}

			return chiffreAffaire;
		}

		/**
		 * Vérifie que les stocks soient en bonne quantité
		 */
		public void CheckStock()
		{
			// récupère les stocks en quantité insuffisante
			IList<Database.Classes.Stock> stocks = StocksRepository.Instance.Lire().Where(item => item.QuantiteActuelle < item.QuantiteMin).ToList();

			// si tous les stocks sont bons
			if (stocks.Count == 0)
			{
				MessageBox.Show("Tous vos stocks sont en quantités abondantes !");
				return;
			}

			var result = MessageBox.Show(
				"Il existe des stocks en quantité insuffisantes !\nVoulez-vous aller sur la fenêtre de stock ?",
				"Attention",
				MessageBoxButton.YesNo);

			if(result == MessageBoxResult.Yes)
				Stocks();
		}

		public void Clients()
		{
			Clients fenetre = new Clients();
			fenetre.ShowDialog();
		}

		public void Fournisseurs()
		{
			Fournisseurs fenetre = new Fournisseurs();
			fenetre.ShowDialog();
		}

		private void Stocks()
		{
			Stock fenetre = new Stock();
			fenetre.ShowDialog();
		}

		private void Achats()
		{
			Achat fenetre = new Achat();
			fenetre.ShowDialog();
		}

		private void Ventes()
		{
			Vente fenetre = new Vente();
			fenetre.ShowDialog();
		}

		private void Categories()
		{
			Categories fenetre = new Categories();
			fenetre.ShowDialog();
		}

		public BaseCommand CommandClients { get; set; }
		public BaseCommand CommandFournisseurs { get; set; }
		public BaseCommand CommandStocks { get; set; }
		public BaseCommand CommandAchats { get; set; }
		public BaseCommand CommandVentes { get; set; }
		public BaseCommand CommandCheckStock { get; set; }
		public BaseCommand CommandCategories { get; set; }
	}
}
