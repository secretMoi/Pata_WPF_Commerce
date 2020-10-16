using System.Windows.Documents;
using Database.Classes;
using Pata_WPF_Commerce.Views;

namespace Pata_WPF_Commerce.Core
{
	public class Viewer
	{
		private readonly FlowDocument _flowDocument = new FlowDocument(); // document sur lequel on travaille
		private readonly List _elements = new List(); // liste des éléments du document

		// sauvegarde le dernier document généré
		public static FlowDocument LastDocument { get; private set; } = new FlowDocument();

		/**
		 * <summary>Définit le titre du document</summary>
		 * <param name="title">Titre à ajouter</param>
		 */
		public void SetTitle(string title)
		{
			Paragraph paragraph = new Paragraph();

			paragraph.Inlines.Add(new Bold(new Run(title)));

			_flowDocument.Blocks.Add(paragraph);
		}

		/**
		 * <summary>Ajoute un élément au document</summary>
		 * <param name="element">Elément à ajouter</param>
		 */
		public void AddElement(string element)
		{
			Paragraph paragraph = new Paragraph(new Run(element));

			_elements.ListItems.Add(new ListItem(paragraph));
		}

		/**
		 * <summary>Génère le document avec les éléments</summary>
		 */
		public FlowDocument Execute()
		{
			_flowDocument.Blocks.Add(_elements);

			LastDocument = _flowDocument;

			return _flowDocument;
		}
	}
}
