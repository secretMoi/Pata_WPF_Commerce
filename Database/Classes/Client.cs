﻿using System;
using System.Collections.Generic;
using Database.Acces;

namespace Database.Classes
{
	public class Client : Base
	{
		public Client()
		{
		}

		public Client(string nom, string prenom, DateTime naissance, string mail)
		{
			Nom = nom;
			Prenom = prenom;
			Naissance = naissance;
			Mail = mail;
		}

		public Client(int id, string nom, string prenom, DateTime naissance, string mail)
			: this(nom, prenom, naissance, mail)
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
				_champs.Add(("mail", typeof(string)));
			}

			return _champs;
		}

		public override Type GetAcces()
		{
			return typeof(AccesClients);
		}

		public int Id { get; set; }

		public string Nom { get; set; }
		public string Prenom { get; set; }
		public DateTime Naissance { get; set; }
		public string Mail { get; set; }
	}
}
