using System.IO;
using System.Text;
using Color = System.Windows.Media.Color;

namespace Pata_WPF_Commerce.Core
{
	public class HtmlView
	{
		private StringBuilder _htmlCode;
		private int _colonneActuelle;

		public HtmlView(string titre, int nombreColonnes)
		{
			NombreColonnes = nombreColonnes;
			GenerateHead(titre);
		}

		private void GenerateHead(string titre)
		{
			_htmlCode = new StringBuilder();

			_htmlCode.Append(
				"<!doctype html>\r\n" +
				"<html lang=\"fr\">\r\n" +
				"<head>\r\n" +
				"<meta charset=\"utf-8\">\r\n" +
				"<title>" + titre + "</title>\r\n" +
				"</head>\r\n" +
				"<body>\r\n" +
				"<h1 align='center'>" + titre + "</h1>\r\n" +
				"<table align='center' cellpadding='5' cellspacing='0' style='border: 1px solid " +
				ColorToHex(Color.FromRgb(0, 0, 0)) + ";'>\r\n"
			);
		}

		// converti la classe couleur en code couleur hexa
		public static string ColorToHex(Color color)
		{
			return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
		}

		public void GenerateColumn(params string[] data)
		{
			if (data.Length != NombreColonnes) return;

			_htmlCode.Append("<tr>\r\n");

			foreach (string colonne in data)
			{
				_htmlCode.Append(
					"<th style='color: " + ColorToHex(Color.FromRgb(0, 0, 0)) + " ;background-color: " + ColorToHex(Color.FromRgb(255,255,255)) + "; border: 1px solid " + ColorToHex(Color.FromRgb(0, 0, 0)) + ";'>" +
					colonne +
					"</th>\r\n"
				);
			}

			_htmlCode.Append("</tr>\r\n\r\n");
		}

		public void GenerateBody(string data)
		{
			if (_colonneActuelle % NombreColonnes == 0)
			{
				_htmlCode.Append("<tr>\r\n");

				if (_colonneActuelle != 0)
					_htmlCode.Append("</tr>\r\n\r\n");
			}

			_htmlCode.Append(
				"<td style='border: 1px solid " + ColorToHex(Color.FromRgb(0, 0, 0)) + "'>" +
				data +
				"</td>\r\n"
			);

			_colonneActuelle++;
		}

		public void GenerateFooter()
		{
			_htmlCode.Append(
				"</tr>\r\n\r\n" +
				"</table>\r\n" +
				"</body>\r\n" +
				"</html>\r\n"
			);
		}

		public void SaveTo(string path, string file)
		{
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			GenerateFooter();
			File.WriteAllText(path + "/" + file + ".html", _htmlCode.ToString());
		}

		public string SourceCode => _htmlCode.ToString();

		public int NombreColonnes { get; set; }
	}
}
