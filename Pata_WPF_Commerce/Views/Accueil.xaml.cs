using System.Windows;
using Pata_WPF_Commerce.ViewModels;

namespace Pata_WPF_Commerce.Views
{
	/// <summary>
	/// Logique d'interaction pour Accueil.xaml
	/// </summary>
	public partial class Accueil : Window
	{
		private readonly AccueilViewModel _viewModel = new AccueilViewModel();

		public Accueil()
		{
			InitializeComponent();

			DataContext = _viewModel;
		}
	}
}
