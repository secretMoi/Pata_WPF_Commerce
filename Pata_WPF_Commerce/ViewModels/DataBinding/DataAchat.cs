using System.Reflection;
using Database.Classes;

namespace Pata_WPF_Commerce.ViewModels.DataBinding
{
	public class DataAchat : BaseProperty
	{
		private int _quantite;
		private decimal _prixTotal;

		private Stock _stock;
		private Fournisseur _fournisseur;

		public Stock Stock
		{
			get => _stock;
			set => AssignField(ref _stock, value, MethodBase.GetCurrentMethod().Name);
		}

		public Fournisseur Fournisseur
		{
			get => _fournisseur;
			set => AssignField(ref _fournisseur, value, MethodBase.GetCurrentMethod().Name);
		}

		public int Quantite
		{
			get => _quantite;
			set => AssignField(ref _quantite, value, MethodBase.GetCurrentMethod().Name);
		}

		public decimal PrixTotal
		{
			get => _prixTotal;
			set => AssignField(ref _prixTotal, value, MethodBase.GetCurrentMethod().Name);
		}
	}
}
