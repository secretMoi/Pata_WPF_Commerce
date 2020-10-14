using System.Windows;
using System.Windows.Controls;
using Pata_WPF_Commerce.ViewModels;

namespace Pata_WPF_Commerce.Views
{
	/// <summary>
	/// Logique d'interaction pour Stock.xaml
	/// </summary>
	public partial class Stock : Window
	{
		private readonly StockViewModel _viewModel = new StockViewModel();

		public Stock()
		{
			InitializeComponent();

			DataContext = _viewModel;
		}

		private void DataGridClients_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (DataGridClients.SelectedIndex >= 0)
				_viewModel.ChangedSelectedClient();
		}
	}
}
