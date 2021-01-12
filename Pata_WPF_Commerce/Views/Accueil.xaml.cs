using System.Collections.Generic;
using System.IO;
using System.Windows;
using OfficeOpenXml;
using Pata_WPF_Commerce.ServiceReference1;
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

			Loaded += MyWindow_Loaded;
		}

		private async void MyWindow_Loaded(object sender, RoutedEventArgs e)
		{
			Service1Client service = new Service1Client();

			HydrateExcel("examen.xls", await service.GetVentesFromCommandeAsync(1));
		}

		private void HydrateExcel(string path, Database.Classes.Stock[] ventes)
		{
			if (File.Exists(path))
				File.Delete(path);

			FileInfo file = new FileInfo(path);
			using (var package = new ExcelPackage(file))
			{
				var firstSheet = package.Workbook.Worksheets.Add("Feuil1");

				string column = "A";
				int cell = 1;

				foreach (var vente in ventes)
				{
					firstSheet.Cells[column + cell].Value = vente.Nom;
					cell++;
				}

				column = "B";
				cell = 1;

				foreach (var vente in ventes)
				{
					firstSheet.Cells[column + cell].Value = vente.QuantiteActuelle;
					cell++;
				}

				column = "C";
				cell = 1;

				foreach (var vente in ventes)
				{
					firstSheet.Cells[column + cell].Value = vente.PrixVente;
					cell++;
				}

				package.SaveAs(package.File);
			}
		}
	}
}
