namespace Email.Messaging
{
	public interface IAzureMessageBusConsumer
	{
		Task Start();


		Task Stop();
	}
}
