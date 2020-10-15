using System.Windows;
using System.Windows.Controls;
using Pata_WPF_Commerce.ViewModels;

namespace Pata_WPF_Commerce.Views
{
	/// <summary>
	/// Logique d'interaction pour Achat.xaml
	/// </summary>
	public partial class Achat : Window
	{
		private readonly AchatViewModel _viewModel = new AchatViewModel();

		public Achat()
		{
			InitializeComponent();

			DataContext = _viewModel;
		}

		private void DataGridClients_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			
		}
	}
}
