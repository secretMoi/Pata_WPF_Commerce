using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Database.Classes;
using Database.Gestions;
using Pata_WPF_Commerce.ViewModels.DataBinding;

namespace Pata_WPF_Commerce.ViewModels
{
	public class ClientViewModel : BaseProperty
	{
		private DataClient _clientMap;

		public ClientViewModel()
		{
			Clients = LoadClients(); // récupère les clients dans la bdd

			ClientMap = Map(Clients.FirstOrDefault(), ClientMap); // injecte le premier client trouvé

			// bind les commandes au xaml
			CommandeConfirm = new BaseCommand(Confirmer);
			CommandeCancel = new BaseCommand(Annuler);
		}

		private ObservableCollection<Client> LoadClients()
		{
			ObservableCollection<Client> listClients = new ObservableCollection<Client>();
			List<Client> tempClients = new GestionClient(Configuration.Instance.Connexion).Lire("id");

			foreach (var client in tempClients)
			{
				listClients.Add(client);	
			}

			return listClients;
		}

		public DataClient ClientMap
		{
			get => _clientMap;
			set => AssignField<DataClient>(ref _clientMap, value, MethodBase.GetCurrentMethod().Name);
		}

		public ObservableCollection<Client> Clients { get; set; }

		public void Confirmer()
		{
			ClientMap.Nom = "coucou";
		}

		public void Annuler()
		{
			ClientMap.Prenom = "test";
		}

		#region Commands
		public BaseCommand CommandeConfirm { get; set; }
		public BaseCommand CommandeCancel { get; set; }
		#endregion
	}
}
