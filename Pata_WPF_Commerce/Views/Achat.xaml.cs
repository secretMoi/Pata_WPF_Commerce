using System.Linq;
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

		private void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (DataGridStock.SelectedIndex >= 0)
				_viewModel.ChangedSelected();
		}

		private void TextBoxQuantite_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			_viewModel.ChangeQuantite(TextBoxQuantite.Text);
		}

		private void ButtonAjouter_OnClick(object sender, RoutedEventArgs e)
		{
			RichTextBox.Document = _viewModel.AddItem();
		}
	}
}
