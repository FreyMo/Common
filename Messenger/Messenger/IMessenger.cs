namespace Messenger.Messenger
{
	public interface IMessenger
	{
		void Send<TMessage>(TMessage message) where TMessage : class, IMessage;

		void Post<TMessage>(TMessage message) where TMessage : class, IMessage;

		void SubscribeTo<TMessage>(ISubscriber<TMessage> subscriber) where TMessage : class, IMessage;
		
		void UnsubscribeFrom<TMessage>(ISubscriber<TMessage> subscriber) where TMessage : class, IMessage;
	}
}