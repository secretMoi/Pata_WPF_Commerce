using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Colorspace;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace Pata_WPF_Commerce.Core
{
	public class Pdf
	{
		private readonly Document _document;

		private Table _table;
		private List<Cell> _cells;

		private readonly List<object> _elements = new List<object>();

		public Pdf(string path)
		{
			PdfWriter writer = new PdfWriter( // création du fichier pdf
				path + ".pdf"
			);


			PdfDocument pdf = new PdfDocument(writer); // permet de manipuler un document pdf
			_document = new Document(pdf); // permet de s'abstraire aux limites pdf
		}

		public bool Logo(string name)
		{
			string path = @"Ressources/Images/" + name + ".png";

			if (!File.Exists(path)) return false;

			Image logo = new Image(ImageDataFactory.Create(path))
				.SetHeight(100)
				.SetWidth(100)
				.SetTextAlignment(TextAlignment.LEFT);

			_elements.Add(logo);

			return true;
		}

		public void RightColumn(StringBuilder text)
		{
			RightColumn(text.ToString());
		}

		public void RightColumn(string text)
		{
			Paragraph paragraph = new Paragraph(text)
				.SetMarginLeft(400)
				.SetMarginBottom(10)
				.SetTextAlignment(TextAlignment.LEFT)
				.SetFontSize(12);

			_elements.Add(paragraph);
		}

		public void Title(string title)
		{
			Paragraph paragraph = new Paragraph(title)
				.SetMarginBottom(10)
				.SetTextAlignment(TextAlignment.CENTER)
				.SetFontSize(16);

			_elements.Add(paragraph);
		}

		public bool MakeTable(int columnNumber)
		{
			if (columnNumber < 1) return false;

			_table = new Table(columnNumber, false).UseAllAvailableWidth();

			_elements.Add(_table);

			return true;
		}

		public bool MakeTableHeader(string header)
		{
			if (_table == null) return false;

			if (_cells == null) _cells = new List<Cell>();

			Cell cell = new Cell(1, 1)
				/*.SetBackgroundColor(Color.MakeColor(new PdfDeviceCs.Gray())) // couleur arrière-plan
				.SetFontColor(new DeviceRgb(System.Windows.Media.Color.FromRgb(0, 0, 0))) // couleur texte*/
				.Add(new Paragraph(header)); // texte

			_table.AddCell(cell);

			return true;
		}

		public bool MakeTableData(string data)
		{
			if (_table == null) return false;

			Cell cell = new Cell(1, 1)
				//.SetFontColor(new DeviceRgb(Theme.BackDark)) // couleur texte
				.Add(new Paragraph(data)); // texte

			_table.AddCell(cell);

			return true;
		}

		public void Close()
		{
			foreach (object element in _elements)
				_document.Add((dynamic)element); // ligne magique pour convertir l'objet en son premier type

			_document.Close();
		}

		public static void DirectoryAvailable(string directory)
		{
			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);
		}

		public static string SetDate()
		{
			string date = DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(':', '_');
			return date.Replace('/', '_');
		}
	}
}
