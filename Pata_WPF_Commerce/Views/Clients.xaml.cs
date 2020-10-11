using System.Windows;
using System.Windows.Controls;
using Pata_WPF_Commerce.ViewModels;

namespace Pata_WPF_Commerce.Views
{
	/// <summary>
	/// Logique d'interaction pour Clients.xaml
	/// </summary>
	public partial class Clients : Window
	{
		private readonly ClientViewModel _viewModel = new ClientViewModel();

		public Clients()
		{
			InitializeComponent();

			DataContext = _viewModel;

			RichTextBoxClients.Document = _viewModel.FullClientsList();
		}

		private void DataGridClients_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (DataGridClients.SelectedIndex >= 0)
				_viewModel.ChangedSelectedClient();
		}
	}
}
