﻿using Email.Models;
using MimeKit;
using MailKit.Net.Smtp;

namespace Email.Services
{
	public class EmailSendService
	{

		private readonly string email;
		private readonly string password;
		public EmailSendService(IConfiguration _configuration)
		{

			email = _configuration.GetSection("EmailService:Email").Get<string>();
			password = _configuration.GetSection("EmailService:Password").Get<string>();
		}

		public async Task SendEmail(UserMessage res, string message)
		{
			MimeMessage message1 = new MimeMessage();
			message1.From.Add(new MailboxAddress("MediCure", email));

			// Set the recipient's email address
			message1.To.Add(new MailboxAddress(res.Name, res.Email));

			message1.Subject = "Welcome to MediCure";

			var body = new TextPart("html")
			{
				Text = message.ToString()
			};
			message1.Body = body;

			var client = new SmtpClient();

			client.Connect("smtp.gmail.com", 587, false);

			client.Authenticate(email, password);

			await client.SendAsync(message1);

			await client.DisconnectAsync(true);
		}
	}
}
