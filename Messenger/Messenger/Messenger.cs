namespace Messenger.Messenger
{
	using System;
	using System.Threading;
	using Common.ArgumentMust;
	using Common.Extensions;

	public abstract class Messenger : IMessenger
	{
		private readonly SynchronizationContext _synchronizationContext;

		private readonly HandlerDictionary _handlers = new HandlerDictionary();

		protected Messenger(SynchronizationContext synchonizationContext)
		{
			_synchronizationContext = synchonizationContext;
		}

		public void Send<TMessage>(TMessage message) where TMessage : class, IMessage
		{
			ArgumentMust.NotBeNull(() => message);

			PublishMessage((x, y) => _synchronizationContext.Send(x, y), message);
		}

		public void Post<TMessage>(TMessage message) where TMessage : class, IMessage
		{
			ArgumentMust.NotBeNull(() => message);

			PublishMessage((x, y) => _synchronizationContext.Post(x, y), message);
		}

		public void SubscribeTo<TMessage>(ISubscriber<TMessage> subscriber) where TMessage : class, IMessage
		{
			ArgumentMust.NotBeNull(() => subscriber);

			_handlers.AddHandler(subscriber);
		}

		public void UnsubscribeFrom<TMessage>(ISubscriber<TMessage> subscriber) where TMessage : class, IMessage
		{
			ArgumentMust.NotBeNull(() => subscriber);

			_handlers.RemoveHandler(subscriber);
		}

		private void DispatchAction<TMessage>(TMessage message) where TMessage : class, IMessage
		{
			if (_handlers.ContainsKey(typeof(TMessage)))
			{
				_handlers.InvalidateHandlers();
				_handlers[typeof(TMessage)].ForEach(wr => wr.Target.As<ISubscriber<TMessage>>()?.OnMessageReceived(message));
			}
		}

		private void PublishMessage<TMessage>(Action<SendOrPostCallback, object> sendOrPostAction, TMessage message)
			where TMessage : class, IMessage
		{
			if (_synchronizationContext != null)
			{
				sendOrPostAction(m => DispatchAction(m.Cast<TMessage>()), message);
			}
			else
			{
				DispatchAction(message);
			}
		}
	}
}