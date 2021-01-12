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

		public Fournisseur(int id, string nom, string mail)
		{
			Id = id;
			Nom = nom;
			Mail = mail;
		}

		public override List<(string, Type)> GetChamps()
		{
			if (_champs.Count == 0)
			{
				_champs.Add(("id", typeof(int)));
				_champs.Add(("nom", typeof(string)));
				_champs.Add(("mail", typeof(string)));
			}

			return _champs;
		}

		public override Type GetAcces()
		{
			return typeof(AccesFournisseur);
		}

		public int Id { get; set; }
		public string Nom { get; set; }
		public string Mail { get; set; }
	}
}
