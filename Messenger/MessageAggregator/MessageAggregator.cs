namespace Messenger.MessageAggregator
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using Common.ArgumentMust;
	using Common.Extensions;

	public class MessageAggregator : IMessageAggregator
	{
		private readonly IList<Delegate> _subscribedHandlers = new List<Delegate>();

		private readonly SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

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

			_subscribedHandlers.AddOnce(messageHandler);
		}

		public void UnsubscribeFrom<TMessage>(Action<TMessage> messageHandler) where TMessage : class, IMessage
		{
			ArgumentMust.NotBeNull(() => messageHandler);

			_subscribedHandlers.RemoveAll(messageHandler);
		}

		private void DispatchAction<TMessage>(TMessage message)
		{
			_subscribedHandlers.OfType<Action<TMessage>>().ToList().ForEach(handler => handler(message));
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