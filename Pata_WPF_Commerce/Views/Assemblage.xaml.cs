using System.Windows;
using System.Windows.Controls;
using Pata_WPF_Commerce.ViewModels;

namespace Pata_WPF_Commerce.Views
{
	/// <summary>
	/// Logique d'interaction pour Assemblage.xaml
	/// </summary>
	public partial class Assemblage : Window
	{
		private readonly AssemblageViewModel _viewModel = new AssemblageViewModel();

		public Assemblage()
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
