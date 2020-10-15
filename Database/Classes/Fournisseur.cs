using System;
using System.Collections.Generic;
using Database.Acces;

namespace Database.Classes
{
	public class Fournisseur : Base
	{
		public Fournisseur()
		{
		}

		public Fournisseur(int id, string nom)
		{
			Id = id;
			Nom = nom;
		}

		public override List<(string, Type)> GetChamps()
		{
			if (_champs.Count == 0)
			{
				_champs.Add(("id", typeof(int)));
				_champs.Add(("nom", typeof(string)));
			}

			return _champs;
		}

		public override Type GetAcces()
		{
			return typeof(AccesFournisseur);
		}

		public int Id { get; set; }

		public string Nom { get; set; }
	}
}
