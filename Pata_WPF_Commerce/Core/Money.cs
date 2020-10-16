using System;

namespace Pata_WPF_Commerce.Core
{
	public class Money
	{
		public Money(decimal montant = Decimal.Zero)
		{
			Montant = Round(montant);
		}

		public override string ToString()
		{
			return Montant + " €";
		}

		public static string Display(decimal montant)
		{
			return Round(montant) + " €";
		}

		public static decimal Round(decimal montant)
		{
			return decimal.Round(montant, 2);
		}

		public static string CleanDevise(string montant)
		{
			return montant.Split(' ')[0];
		}

		public decimal Montant { get; set; }
	}
}
