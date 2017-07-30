namespace Messenger.MessageAggregator
{
	using System;

	public interface IMessageAggregator
	{
		void Send<TMessage>(TMessage message) where TMessage : class, IMessage;

		void Post<TMessage>(TMessage message) where TMessage : class, IMessage;

		void SubscribeTo<TMessage>(Action<TMessage> messageHandler) where TMessage : class, IMessage;
		
		void UnsubscribeFrom<TMessage>(Action<TMessage> messageHandler) where TMessage : class, IMessage;
	}
}