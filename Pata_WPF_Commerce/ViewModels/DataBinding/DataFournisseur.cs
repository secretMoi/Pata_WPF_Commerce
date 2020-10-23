using System.Reflection;

namespace Pata_WPF_Commerce.ViewModels.DataBinding
{
	public class DataFournisseur : BaseProperty
	{
		private int _id;
		private string _nom;

		public int Id
		{
			get => _id;
			set => AssignField(ref _id, value, MethodBase.GetCurrentMethod().Name);
		}

		public string Nom
		{
			get => _nom;
			set => AssignField(ref _nom, value, MethodBase.GetCurrentMethod().Name);
		}
	}
}
