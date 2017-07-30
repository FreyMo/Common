namespace Messenger.Messenger
{
	public interface ISubscriber<in TMessage> where TMessage : class, IMessage
	{
		void OnMessageReceived(TMessage message);
	}
}