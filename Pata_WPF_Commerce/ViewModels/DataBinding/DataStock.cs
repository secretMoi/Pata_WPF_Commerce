using System.Reflection;

namespace Pata_WPF_Commerce.ViewModels.DataBinding
{
	public class DataStock : BaseProperty
	{
		private int _id;
		private string _nom;
		private int _quantiteActuelle, _quantiteMin;
		private decimal _prixAchat, _prixVente;

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

		public int QuantiteActuelle
		{
			get => _quantiteActuelle;
			set => AssignField(ref _quantiteActuelle, value, MethodBase.GetCurrentMethod().Name);
		}

		public int QuantiteMin
		{
			get => _quantiteMin;
			set => AssignField(ref _quantiteMin, value, MethodBase.GetCurrentMethod().Name);
		}

		public decimal PrixAchat
		{
			get => _prixAchat;
			set => AssignField(ref _prixAchat, value, MethodBase.GetCurrentMethod().Name);
		}

		public decimal PrixVente
		{
			get => _prixVente;
			set => AssignField(ref _prixVente, value, MethodBase.GetCurrentMethod().Name);
		}
	}
}
