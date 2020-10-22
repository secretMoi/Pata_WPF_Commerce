using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Pata_WPF_Commerce.Repositories;
using Pata_WPF_Commerce.Views;

namespace Pata_WPF_Commerce.ViewModels
{
	public class AccueilViewModel : BaseProperty
	{
		public AccueilViewModel()
		{
			// bind les commandes au xaml
			CommandClients = new BaseCommand(Clients);
			CommandStocks = new BaseCommand(Stocks);
			CommandAchats = new BaseCommand(Achats);
			CommandVentes = new BaseCommand(Ventes);
			CommandCheckStock = new BaseCommand(CheckStock);
		}

		public void CheckStock()
		{
			IList<Database.Classes.Stock> stocks = StocksRepository.Instance.Lire().Where(item => item.QuantiteActuelle < item.QuantiteMin).ToList();

			if (stocks.Count == 0) return;

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

		public BaseCommand CommandClients { get; set; }
		public BaseCommand CommandStocks { get; set; }
		public BaseCommand CommandAchats { get; set; }
		public BaseCommand CommandVentes { get; set; }
		public BaseCommand CommandCheckStock { get; set; }
	}
}
