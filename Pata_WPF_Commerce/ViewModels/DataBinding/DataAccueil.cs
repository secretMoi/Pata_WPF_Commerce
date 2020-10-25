using System.Reflection;

namespace Pata_WPF_Commerce.ViewModels.DataBinding
{
	public class DataAccueil : BaseProperty
	{
		private string _chiffreAffaire;
		private string _source, _password, _destination, _title, _body;

		public string ChiffreAffaire
		{
			get => _chiffreAffaire;
			set => AssignField(ref _chiffreAffaire, value, MethodBase.GetCurrentMethod().Name);
		}

		public string Source
		{
			get => _source;
			set => AssignField(ref _source, value, MethodBase.GetCurrentMethod().Name);
		}

		public string Password
		{
			get => _password;
			set => AssignField(ref _password, value, MethodBase.GetCurrentMethod().Name);
		}

		public string Destination
		{
			get => _destination;
			set => AssignField(ref _destination, value, MethodBase.GetCurrentMethod().Name);
		}

		public string Title
		{
			get => _title;
			set => AssignField(ref _title, value, MethodBase.GetCurrentMethod().Name);
		}

		public string Body
		{
			get => _body;
			set => AssignField(ref _body, value, MethodBase.GetCurrentMethod().Name);
		}
	}
}
