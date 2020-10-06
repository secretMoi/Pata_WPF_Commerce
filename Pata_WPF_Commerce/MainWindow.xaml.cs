using System;
using System.Windows;
using System.Windows.Controls;
using Pata_WPF_Commerce.ViewModels;

namespace Pata_WPF_Commerce
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly ClientViewModel _viewModel = new ClientViewModel();

		public MainWindow()
		{
			InitializeComponent();

			DataContext = _viewModel;
		}

		private void DataGridClients_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			
		}
	}
}
