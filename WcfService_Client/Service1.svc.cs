using System;
using System.Collections.Generic;
using System.Linq;
using Database.Classes;
using Database.Gestions;

namespace WcfService_Client
{
	// REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
	// REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
	public class Service1 : IService1
	{
		private readonly Gestion<Vente> _gestion = new Gestion<Vente>(Configuration.Instance.Connexion);
		private readonly Gestion<Stock> _stocks = new Gestion<Stock>(Configuration.Instance.Connexion);

		public string GetData(int value)
		{
			return $"You entered: {value}";
		}

		public CompositeType GetDataUsingDataContract(CompositeType composite)
		{
			if (composite == null)
			{
				throw new ArgumentNullException(nameof(composite));
			}
			if (composite.BoolValue)
			{
				composite.StringValue += "Suffix";
			}
			return composite;
		}

		public List<Stock> GetVentesFromCommande(int id)
		{
			try
			{
				var ventes = _gestion
					.Lire("id")
					.Where(item => item.IdCommande == id)
					.ToList();

				return ventes
					.Select(
						vente => _stocks.LireId(vente.IdStock)
					)
					.ToList();
			}
			catch
			{
				Console.WriteLine("Impossible de lire l'id " + id);
				return null;
			}
		}
	}
}
