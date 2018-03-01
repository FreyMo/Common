﻿namespace Messenger.Messenger
{
	public interface ICachedMessenger : IMessenger
	{
		TMessage RequestLast<TMessage>() where TMessage : class;
	}
}