using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Database.Classes;
using Pata_WPF_Commerce.Repositories;
using Pata_WPF_Commerce.ViewModels.DataBinding;

namespace Pata_WPF_Commerce.ViewModels
{
	public class ClientViewModel : BaseProperty
	{
		private DataClient _clientMap; // données bindée dans le formulaire
		private Client _selectedClient; // données bindée dans la dgv du client sélectionné

		public DataClient ClientMap
		{
			get => _clientMap;
			set => AssignField(ref _clientMap, value, MethodBase.GetCurrentMethod().Name);
		}

		public Client SelectedClient
		{
			get => _selectedClient;
			set => AssignField(ref _selectedClient, value, MethodBase.GetCurrentMethod().Name);
		}

		public ObservableCollection<Client> Clients { get; set; }

		public ClientViewModel()
		{
			Clients = LoadClients(); // récupère les clients dans la bdd

			ClientMap = Map(Clients.FirstOrDefault(), ClientMap); // injecte le premier client trouvé

			// bind les commandes au xaml
			CommandeConfirm = new BaseCommand(Confirm);
			CommandeCancel = new BaseCommand(Cancel);
			CommandAdd = new BaseCommand(Add);
			CommandDelete = new BaseCommand(Delete);
		}

		private ObservableCollection<Client> LoadClients()
		{
			ObservableCollection<Client> listClients = new ObservableCollection<Client>();
			IList<Client> tempClients = ClientsRepository.Instance.Lire();

			foreach (var client in tempClients)
			{
				listClients.Add(client);	
			}

			return listClients;
		}

		public void ChangedSelectedClient()
		{
			ClientMap = Map(SelectedClient, ClientMap);
		}

		public void Confirm()
		{
			ClientMap.Nom = "coucou";
		}

		public void Cancel()
		{
			ClientMap.Prenom = "test";
		}

		private async void Add()
		{
			// ajoute le nouveau client à la bdd
			Client client = new Client();
			client = Map(ClientMap, client);
			int res = await ClientsRepository.Instance.AjouterAsync(client);

			// ajoute ce nouveau client à la dgv
			client = await ClientsRepository.Instance.LireIdAsync(res);
			Clients.Add(client);
		}

		private async void Delete()
		{
			if(SelectedClient == null) return;

			// supprime le client de la bdd
			await ClientsRepository.Instance.SupprimerAsync(SelectedClient.Id);

			// supprime le client de la dgv
			Clients.Remove(SelectedClient);
		}

		#region Commands
		public BaseCommand CommandeConfirm { get; set; }
		public BaseCommand CommandeCancel { get; set; }
		public BaseCommand CommandAdd { get; set; }
		public BaseCommand CommandDelete { get; set; }
		#endregion
	}
}
