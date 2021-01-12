using System;

namespace WcfService_Client
{
	public class Configuration
	{
		// singleton lazy et thread-safe
		private static readonly Lazy<Configuration> Lazy = new Lazy<Configuration>(() => new Configuration());

		private string _host = "192.168.1.124";
		private string _database = "magasin";
		private string _user = "sa";
		private string _password = "Nax2J,/nwdbLQGj";

		public static Configuration Instance => Lazy.Value;

		private Configuration()
		{

		}

		public string Connexion => $"Data Source={_host};Initial Catalog={_database};Persist Security Info=True;User ID={_user};Password={_password}";
	}
}
