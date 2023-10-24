using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace ServiceBus
{
	public class MessageBus : IMessageBus
	{
		public string ConnectionString = "Endpoint=sb://hmsservicebus12.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=OEmf30A2r81cniuY9+jcQiKPXheG5qMJS+ASbBfaWk8=";
		public async Task PublishMessage(object message, string topicName)
		{
			var serviceBus = new ServiceBusClient(ConnectionString);

			var sender = serviceBus.CreateSender(topicName);

			var messageJson = JsonConvert.SerializeObject(message);

			var theMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageJson))
			{

				CorrelationId = Guid.NewGuid().ToString(),
			};

			await sender.SendMessageAsync(theMessage);
			await serviceBus.DisposeAsync();

		}
	}
}