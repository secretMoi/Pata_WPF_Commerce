using System;
using System.Reflection;

namespace Pata_WPF_Commerce.ViewModels.DataBinding
{
	public class DataClient : BaseProperty
	{
		private int _id;
		private string _nom, _prenom, _mail;
		private DateTime _naissance;

		public int Id
		{
			get => _id;
			set => AssignField<int>(ref _id, value, MethodBase.GetCurrentMethod().Name);
		}
		public string Prenom
		{
			get => _prenom;
			set => AssignField<string>(ref _prenom, value, MethodBase.GetCurrentMethod().Name);
		}
		public string Nom
		{
			get => _nom;
			set => AssignField<string>(ref _nom, value, MethodBase.GetCurrentMethod().Name);
		}

		public string Mail
		{
			get => _mail;
			set => AssignField<string>(ref _mail, value, MethodBase.GetCurrentMethod().Name);
		}

		public DateTime Naissance
		{
			get => _naissance;
			set => AssignField<DateTime>(ref _naissance, value, MethodBase.GetCurrentMethod().Name);
		}
	}
}
