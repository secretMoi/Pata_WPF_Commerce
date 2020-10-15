using System;
using System.Collections.Generic;
using Database.Acces;

namespace Database.Classes
{
	public class CommandesAchat : Base
	{
		public CommandesAchat()
		{
		}

		public CommandesAchat(int id, int idFournisseur, DateTime date)
		{
			Id = id;
			IdFournisseur = idFournisseur;
			Date = date;
		}

		public override List<(string, Type)> GetChamps()
		{
			if (_champs.Count == 0)
			{
				_champs.Add(("id", typeof(int)));
				_champs.Add(("id_fournisseur", typeof(int)));
				_champs.Add(("date", typeof(DateTime)));
			}

			return _champs;
		}

		public override Type GetAcces()
		{
			return typeof(AccesCommandesAchat);
		}

		public int Id { get; set; }

		public int IdFournisseur { get; set; }

		public DateTime Date { get; set; }
	}
}
