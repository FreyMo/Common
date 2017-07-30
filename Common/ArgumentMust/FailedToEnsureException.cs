namespace Common.ArgumentMust
{
	using System;

	[Serializable]
	public class FailedToEnsureException : ArgumentException
	{
		public FailedToEnsureException(string message) : base(message)
		{
		}
	}
}