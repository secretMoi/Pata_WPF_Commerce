using System.Reflection;

namespace Pata_WPF_Commerce.ViewModels.DataBinding
{
	public class DataAssemblage : BaseProperty
	{
		private int _id;
		private string _nom;
		private decimal _prix;
		private decimal _prixPromo;
		private int _processeur;
		private int _carteMere;
		private int _ram;
		private int _carteGraphique;
		private int _disqueDur1;
		private int _disqueDur2;
		private int _boitier;
		private int _alimentation;
		private int _refroidissement;

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

		public decimal Prix
		{
			get => _prix;
			set => AssignField(ref _prix, value, MethodBase.GetCurrentMethod().Name);
		}

		public decimal PrixPromo
		{
			get => _prixPromo;
			set => AssignField(ref _prixPromo, value, MethodBase.GetCurrentMethod().Name);
		}

		public int Processeur
		{
			get => _processeur;
			set => AssignField(ref _processeur, value, MethodBase.GetCurrentMethod().Name);
		}

		public int CarteMere
		{
			get => _carteMere;
			set => AssignField(ref _carteMere, value, MethodBase.GetCurrentMethod().Name);
		}

		public int Ram
		{
			get => _ram;
			set => AssignField(ref _ram, value, MethodBase.GetCurrentMethod().Name);
		}

		public int CarteGraphique
		{
			get => _carteGraphique;
			set => AssignField(ref _carteGraphique, value, MethodBase.GetCurrentMethod().Name);
		}

		public int DisqueDur1
		{
			get => _disqueDur1;
			set => AssignField(ref _disqueDur1, value, MethodBase.GetCurrentMethod().Name);
		}

		public int DisqueDur2
		{
			get => _disqueDur2;
			set => AssignField(ref _disqueDur2, value, MethodBase.GetCurrentMethod().Name);
		}

		public int Boitier
		{
			get => _boitier;
			set => AssignField(ref _boitier, value, MethodBase.GetCurrentMethod().Name);
		}

		public int Alimentation
		{
			get => _alimentation;
			set => AssignField(ref _alimentation, value, MethodBase.GetCurrentMethod().Name);
		}

		public int Refroidissement
		{
			get => _refroidissement;
			set => AssignField(ref _refroidissement, value, MethodBase.GetCurrentMethod().Name);
		}
	}
}
