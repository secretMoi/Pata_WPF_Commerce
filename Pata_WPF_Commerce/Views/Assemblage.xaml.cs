using System.Windows;
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
	}
}
