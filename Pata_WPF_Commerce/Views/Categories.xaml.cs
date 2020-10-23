using System.Windows;
using System.Windows.Controls;
using Pata_WPF_Commerce.ViewModels;

namespace Pata_WPF_Commerce.Views
{
	/// <summary>
	/// Logique d'interaction pour Categories.xaml
	/// </summary>
	public partial class Categories : Window
	{
		private readonly CategorieViewModel _viewModel = new CategorieViewModel();

		public Categories()
		{
			InitializeComponent();

			DataContext = _viewModel;
		}

		private void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (DataGridClients.SelectedIndex >= 0)
				_viewModel.ChangedSelectedItem();
		}
	}
}
