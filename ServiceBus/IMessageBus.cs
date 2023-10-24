using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus
{
	internal interface IMessageBus
	{
		Task PublishMessage(object message, string topicName);
	}
}
