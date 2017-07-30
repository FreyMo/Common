namespace Messenger.ConcreteMessenger
{
	using System.Threading;
	using Messenger;

	public sealed class ConcreteMessenger : Messenger, IConcreteMessenger
	{
		public ConcreteMessenger() : this(SynchronizationContext.Current)
		{
		}

		public ConcreteMessenger(SynchronizationContext synchonizationContext) : base(synchonizationContext)
		{
		}
	}
}