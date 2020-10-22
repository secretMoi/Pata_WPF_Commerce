using System;
using System.Collections.Generic;
using Database.Acces;

namespace Database.Classes
{
	public class CommandesVente : Base
	{
		public CommandesVente()
		{
		}

		public CommandesVente(int id, int idClient, DateTime date)
		{
			Id = id;
			IdClient = idClient;
			Date = date;
		}

		public override List<(string, Type)> GetChamps()
		{
			if (_champs.Count == 0)
			{
				_champs.Add(("id", typeof(int)));
				_champs.Add(("id_client", typeof(int)));
				_champs.Add(("date", typeof(DateTime)));
			}

			return _champs;
		}

		public override Type GetAcces()
		{
			return typeof(AccesCommandesVente);
		}

		public int Id { get; set; }

		public int IdClient { get; set; }

		public DateTime Date { get; set; }
	}
}
