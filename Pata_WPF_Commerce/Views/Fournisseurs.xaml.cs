using System.Windows;
using System.Windows.Controls;
using Pata_WPF_Commerce.ViewModels;
using Pata_WPF_Commerce.ViewModels.DataBinding;

namespace Pata_WPF_Commerce.Views
{
	/// <summary>
	/// Logique d'interaction pour Fournisseurs.xaml
	/// </summary>
	public partial class Fournisseurs : Window
	{
		private readonly FournisseurViewModel _viewModel = new FournisseurViewModel();

		public Fournisseurs()
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
