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

		public BaseCommand CommandClients { get; set; }
		public BaseCommand CommandStocks { get; set; }
		public BaseCommand CommandAchats { get; set; }
	}
}
