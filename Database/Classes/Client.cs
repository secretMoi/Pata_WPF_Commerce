using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Classes
{
	public class Client : Base
	{
		public Client()
		{
		}

		public Client(string nom, string prenom, DateTime naissance)
		{
			Nom = nom;
			Prenom = prenom;
			Naissance = naissance;
		}

		public Client(int id, string nom, string prenom, DateTime naissance)
			: this(nom, prenom, naissance)
		{
			Id = id;
		}

		public void Hydrate(params object[] arguments)
		{

		}

		public override List<(string, Type)> GetChamps()
		{
			if (_champs.Count == 0)
			{
				_champs.Add(("id", typeof(int)));
				_champs.Add(("nom", typeof(string)));
				_champs.Add(("prenom", typeof(string)));
				_champs.Add(("naissance", typeof(DateTime)));
			}

			return _champs;
		}

		public int Id { get; set; }

		public string Nom { get; set; }
		public string Prenom { get; set; }
		public DateTime Naissance { get; set; }
	}
}
