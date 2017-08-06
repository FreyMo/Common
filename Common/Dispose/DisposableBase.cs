namespace Common.Dispose
{
	using System;
	
	public abstract class DisposableBase : IDisposable
	{
		~DisposableBase()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected bool Disposed { get; private set; }
		
		protected virtual void Dispose(bool disposing)
		{
			if (!Disposed)
			{
				Disposed = true;
			}
		}
	}
}