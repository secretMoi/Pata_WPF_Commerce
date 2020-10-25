using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Pata_WPF_Commerce.Core
{
	public class Mailer
	{
		public string Source { get; set; }
		public string Destination { get; set; }
		public string Password { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }

		public async Task<bool> SendAsync()
		{
			if(!IsValidAdress(Destination) || !IsValidAdress(Source))
				return false;

			MailMessage mail = new MailMessage
			{
				From = new MailAddress(Source),
				To = { Destination },
				Subject = Title,
				Body = Body
			};

			SmtpClient smtp = new SmtpClient("smtp.live.com")
			{
				DeliveryMethod = SmtpDeliveryMethod.Network,
				Port = 587,
				Credentials = new NetworkCredential(Source, Password),
				EnableSsl = true
			};

			await smtp.SendMailAsync(mail);

			return true;
		}

		public static bool IsValidAdress(string mail)
		{
			try
			{
				MailAddress addr = new MailAddress(mail);
				return addr.Address == mail;
			}
			catch
			{
				return false;
			}
		}
	}
}
