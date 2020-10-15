using System.Reflection;

namespace Pata_WPF_Commerce.ViewModels.DataBinding
{
	public class DataAchat : BaseProperty
	{
		private int _id;
		private int _idStock;
		private int _idCommande;
		private int _quantite;
		private decimal _prix;

		public int Id
		{
			get => _id;
			set => AssignField(ref _id, value, MethodBase.GetCurrentMethod().Name);
		}

		public int IdStock
		{
			get => _idStock;
			set => AssignField(ref _idStock, value, MethodBase.GetCurrentMethod().Name);
		}

		public int IdCommande
		{
			get => _idCommande;
			set => AssignField(ref _idCommande, value, MethodBase.GetCurrentMethod().Name);
		}

		public int Quantite
		{
			get => _quantite;
			set => AssignField(ref _quantite, value, MethodBase.GetCurrentMethod().Name);
		}

		public decimal Prix
		{
			get => _prix;
			set => AssignField(ref _prix, value, MethodBase.GetCurrentMethod().Name);
		}
	}
}
