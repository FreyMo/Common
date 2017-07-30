namespace Commands.AsyncCommand
{
	using System;
	using System.Threading.Tasks;
	using Common.Notification;

	public abstract class AsyncCommandBase : BindableBase, IAsyncCommand
	{
		private bool _isRunning;

		public event EventHandler CanExecuteChanged;

		public bool IsRunning
		{
			get => _isRunning;

			private set
			{
				if (_isRunning != value)
				{
					_isRunning = value;
					OnPropertyChanged();
				}
			}
		}

		public async void Execute(object parameter)
		{
			IsRunning = true;
			RaiseCanExecuteChanged();

			await ExecuteAsync(parameter);

			IsRunning = false;
			RaiseCanExecuteChanged();
		}

		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}

		public abstract Task ExecuteAsync(object parameter);

		public virtual bool CanExecute(object parameter)
		{
			return !IsRunning;
		}
	}
}