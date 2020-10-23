using System.Reflection;

namespace Pata_WPF_Commerce.ViewModels.DataBinding
{
	public class DataAccueil : BaseProperty
	{
		private string _chiffreAffaire;

		public string ChiffreAffaire
		{
			get => _chiffreAffaire;
			set => AssignField(ref _chiffreAffaire, value, MethodBase.GetCurrentMethod().Name);
		}
	}
}
