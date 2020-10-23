using System.Windows;

namespace Pata_WPF_Commerce.Views
{
	/// <summary>
	/// Logique d'interaction pour Html.xaml
	/// </summary>
	public partial class Html : Window
	{
		public Html(string htmlCode)
		{
			InitializeComponent();

			WebBrowser.NavigateToString(htmlCode);
		}
	}
}
