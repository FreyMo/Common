namespace Messenger.DisposableMessenger
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using Common.ArgumentMust;
	using Common.Dispose;
	using Common.Extensions;

	public class DisposableMessenger : DisposableBase, IDisposableMessenger
	{
		private readonly IList<Delegate> _subscribedHandlers = new List<Delegate>();

		private readonly SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

		private readonly object _lockObject = new object();

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

		public void SubscribeTo<TMessage>(Action<TMessage> messageHandler) where TMessage : class, IMessage
		{
			ArgumentMust.NotBeNull(() => messageHandler);

			lock (_lockObject)
			{
				_subscribedHandlers.AddOnce(messageHandler);
			}
		}

		public void UnsubscribeFrom<TMessage>(Action<TMessage> messageHandler) where TMessage : class, IMessage
		{
			ArgumentMust.NotBeNull(() => messageHandler);

			lock (_lockObject)
			{
				_subscribedHandlers.RemoveAll(messageHandler);
			}
		}

		private void DispatchAction<TMessage>(TMessage message)
		{
			lock (_lockObject)
			{
				_subscribedHandlers.OfType<Action<TMessage>>().ToList().ForEach(handler => handler(message));
			}
		}

		private void PublishMessage<TMessage>(Action<SendOrPostCallback, object> sendOrPostAction, TMessage message)
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