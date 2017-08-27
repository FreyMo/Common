namespace Messenger.DisposableMessenger
{
	using System;

	public interface IDisposableMessenger : IDisposable
	{
		void Send<TMessage>(TMessage message) where TMessage : class, IMessage;

		void Post<TMessage>(TMessage message) where TMessage : class, IMessage;

		void SubscribeTo<TMessage>(Action<TMessage> messageHandler) where TMessage : class, IMessage;
		
		void UnsubscribeFrom<TMessage>(Action<TMessage> messageHandler) where TMessage : class, IMessage;
	}
}