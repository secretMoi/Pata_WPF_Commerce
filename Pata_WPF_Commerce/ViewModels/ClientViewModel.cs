using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Database.Classes;
using Database.Gestions;

namespace Pata_WPF_Commerce.ViewModels
{
	public class ClientViewModel : BaseProperty
	{
		private const string Connexion = "Data Source=192.168.1.124;Initial Catalog=magasin;Persist Security Info=True;User ID=sa;Password=Nax2J,/nwdbLQGj";
		private DataClient _dataDataClient;

		public ClientViewModel()
		{
			Clients = LoadClients(); // récupère les clients dans la bdd
			DataClient = Map(Clients.FirstOrDefault(), DataClient); // injecte le premier client trouvé

			// bind les commandes au xaml
			CommandeConfirm = new BaseCommand(Confirmer);
			CommandeCancel = new BaseCommand(Annuler);
		}

		private ObservableCollection<Client> LoadClients()
		{
			ObservableCollection<Client> listClients = new ObservableCollection<Client>();
			List<Client> tempClients = new GestionClient(Connexion).Lire("id");

			foreach (var client in tempClients)
			{
				listClients.Add(client);	
			}

			return listClients;
		}

		public DataClient DataClient
		{
			get => _dataDataClient;
			set => AssignField<DataClient>(ref _dataDataClient, value, MethodBase.GetCurrentMethod().Name);
		}

		public ObservableCollection<Client> Clients { get; set; }

		public void Confirmer()
		{
			DataClient.Nom = "coucou";
		}

		public void Annuler()
		{
			DataClient.Prenom = "test";
		}

		#region Commands
		public BaseCommand CommandeConfirm { get; set; }
		public BaseCommand CommandeCancel { get; set; }
		#endregion

		/**
		 * <summary>Map une source vers une destination</summary>
		 * <param name="source">Objet source</param>
		 * <param name="destination">Objet destination (c'est le type qui importe)</param>
		 * <returns>Retourne un nouvel objet du type de la destination avec les valeurs de la source</returns>
		 */
		private TU Map<T, TU>(T source, TU destination)
		{
			// configure le mapper
			var config = new MapperConfiguration(cfg => cfg.CreateMap<T, TU>());

			var mapper = config.CreateMapper(); // crée le mapper
			return mapper.Map<TU>(source); // map et retourne le résultat
		}
	}

	public class DataClient : BaseProperty
	{
		private int _id;
		private string _nom, _prenom;
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
		public DateTime Naissance
		{
			get => _naissance;
			set => AssignField<DateTime>(ref _naissance, value, MethodBase.GetCurrentMethod().Name);
		}
	}
}
