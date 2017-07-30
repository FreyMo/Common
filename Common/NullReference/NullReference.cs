namespace Common.NullReference
{
	public static class NullReference
	{
		public static T Of<T>() where T : class
		{
			return null;
		}
	}
}