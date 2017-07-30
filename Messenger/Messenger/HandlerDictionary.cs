namespace Messenger.Messenger
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Common.ArgumentMust;
	using Common.Extensions;

	internal class HandlerDictionary : Dictionary<Type, Handlers>
	{
		private readonly object _thisLock = new object();

		public void AddHandler<TMessage>(ISubscriber<TMessage> subscriber) where TMessage : class, IMessage
		{
			ArgumentMust.NotBeNull(() => subscriber);

			lock (_thisLock)
			{
				AddHandlerPrivate(subscriber);
			}
		}

		public void RemoveHandler<TMessage>(ISubscriber<TMessage> subscriber) where TMessage : class, IMessage
		{
			ArgumentMust.NotBeNull(() => subscriber);

			lock (_thisLock)
			{
				RemoveHandlerPrivate(subscriber);
			}
		}
		
		public void InvalidateHandlers()
		{
			var deadReferences = this.SelectMany(kvp => kvp.Value.Where(wr => !wr.IsAlive));

			deadReferences.ForEach(Delete);
		}

		private void AddHandlerPrivate<TMessage>(ISubscriber<TMessage> subscriber) where TMessage : class, IMessage
		{
			if (ContainsKey(typeof(TMessage)))
			{
				if (this[typeof(TMessage)].All(wr => wr.Target.As<ISubscriber<TMessage>>() != subscriber))
				{
					this[typeof(TMessage)].Add(new WeakReference(subscriber));
				}
			}
			else
			{
				Add(typeof(TMessage), new Handlers(new WeakReference(subscriber)));
			}

			InvalidateHandlers();
		}

		private void RemoveHandlerPrivate<TMessage>(ISubscriber<TMessage> subscriber) where TMessage : class, IMessage
		{
			if (ContainsKey(typeof(TMessage)))
			{
				this[typeof(TMessage)].RemoveAll(wr => wr.Target.As<ISubscriber<TMessage>>() == subscriber);
			}

			InvalidateHandlers();
		}

		private void Delete(WeakReference deadReference)
		{
			var allReferences = this.Select(kvp => kvp.Value).Where(wr => wr.Contains(deadReference));

			allReferences.ForEach(handlers => handlers.RemoveAll(reference => reference.Equals(deadReference)));
		}
	}
}