using System.Windows.Documents;
using Database.Classes;
using Pata_WPF_Commerce.Views;

namespace Pata_WPF_Commerce.Core
{
	public class Viewer
	{
		private readonly FlowDocument _flowDocument = new FlowDocument();
		private readonly List _elements = new List();

		public void SetTitle(string title)
		{
			Paragraph paragraph = new Paragraph();

			paragraph.Inlines.Add(new Bold(new Run(title)));

			_flowDocument.Blocks.Add(paragraph);
		}

		public void AddElement(string element)
		{
			Paragraph paragraph = new Paragraph(new Run(element));

			_elements.ListItems.Add(new ListItem(paragraph));
		}

		public FlowDocument Execute()
		{
			_flowDocument.Blocks.Add(_elements);

			return _flowDocument;
		}
	}
}
