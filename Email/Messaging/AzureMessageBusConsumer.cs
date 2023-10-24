using Azure.Messaging.ServiceBus;
using Email.Models;
using Email.Services;
using Newtonsoft.Json;
using System.Text;

namespace Email.Messaging
{
	public class AzureMessageBusConsumer : IAzureMessageBusConsumer
	{
		private readonly IConfiguration _configuration;
		private readonly string Connectionstring;
		private readonly string QueueName;
		private readonly string topic;
		private readonly string subscription;
		private readonly ServiceBusProcessor _registrationProcessor;
		private readonly ServiceBusProcessor _orderEmails;
		private readonly EmailSendService _emailService;
		private readonly EmailService _saveToDb;
		public AzureMessageBusConsumer(IConfiguration configuration, EmailService service)
		{

			_configuration = configuration;
			Connectionstring = _configuration.GetSection("ServiceBus:ConnectionString").Get<string>();
			QueueName = _configuration.GetSection("QueuesandTopics:RegisterUser").Get<string>();
			topic = _configuration.GetSection("AzureService:Topic").Get<string>();
			subscription = _configuration.GetSection("AzureService:Subscription").Get<string>();


			var serviceBusClient = new ServiceBusClient(Connectionstring);
			_registrationProcessor = serviceBusClient.CreateProcessor(QueueName);
			_orderEmails = serviceBusClient.CreateProcessor(topic, subscription);
			_emailService = new EmailSendService(_configuration);
			_saveToDb = service;

		}

		public async Task Start()
		{
			//start Processing
			_registrationProcessor.ProcessMessageAsync += OnRegistartion;
			_registrationProcessor.ProcessErrorAsync += ErrorHandler;
			await _registrationProcessor.StartProcessingAsync();

			_orderEmails.ProcessMessageAsync += OnOrder;
			_orderEmails.ProcessErrorAsync += ErrorHandler;
			await _orderEmails.StartProcessingAsync();
		}

		private async Task OnOrder(ProcessMessageEventArgs arg)
		{
			var message = arg.Message;

			var body = Encoding.UTF8.GetString(message.Body);

		

			StringBuilder sb = new StringBuilder();
			sb.Append("<h1> Order Proccessed </h1>");
			sb.Append("<p> order  will be shipped tomorrow</p>");

			var Umessage = new UserMessage()
			{
				//Email = rewards.Email,
				Name = "John Doe"
			};

			await _emailService.SendEmail(Umessage, sb.ToString());
			await arg.CompleteMessageAsync(message);
		}

		public async Task Stop()
		{
			//Stop Processing
			await _registrationProcessor.StopProcessingAsync();
			await _registrationProcessor.DisposeAsync();
		}
		private Task ErrorHandler(ProcessErrorEventArgs arg)
		{

			//[Todo] send an email to Admin

			Console.WriteLine(arg.Exception.ToString());
			return Task.CompletedTask;
		}

		private async Task OnRegistartion(ProcessMessageEventArgs arg)
		{
			var message = arg.Message;

			var body = Encoding.UTF8.GetString(message.Body);

			var userMessage = JsonConvert.DeserializeObject<UserMessage>(body);

			//TODO send An Email
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<img src=\"https://scontent.fnbo9-1.fna.fbcdn.net/v/t39.30808-6/395058770_726623192815408_3386073236183771478_n.jpg?stp=cp6_dst-jpg&_nc_cat=100&ccb=1-7&_nc_sid=5f2048&_nc_eui2=AeGTzmybHoFFz2PS6PCKj58N_BqDeiEn_yT8GoN6ISf_JI0urGuBCC7T9NUljKNjHGu2y5lAG4KsExxpQkGJPIqA&_nc_ohc=cZDkqNgS7IcAX_hIVrU&_nc_pt=5&_nc_zt=23&_nc_ht=scontent.fnbo9-1.fna&oh=00_AfCaGFl7Ilg_UbDvm6rlsaGpApfUp3CXq0zXtxJeQygO0w&oe=653ACB66\" width=\"1000\" height=\"600\">");
				stringBuilder.Append("<h1> Hello " + userMessage.Name + "</h1>");
				stringBuilder.AppendLine("<br/>Welcome to Medicure ");

				stringBuilder.Append("<br/>");
				stringBuilder.Append('\n');
				stringBuilder.Append("<p> Start Booking Appointments With Our Doctors</p>");
				var emailLogger = new EmailLoggers()
				{
					Email = userMessage.Email,
					Message = stringBuilder.ToString()

				};
				await _saveToDb.SaveData(emailLogger);
				await _emailService.SendEmail(userMessage, stringBuilder.ToString());
				//you can delete the message from the queue
				await arg.CompleteMessageAsync(message);
			}
			catch (Exception ex) { }
		}


	}
}
