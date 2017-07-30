namespace Common.Extensions
{
	using System;
	using System.Collections.Generic;
	using ArgumentMust;
	using Predicates;

	public static class EnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			ArgumentMust.NotBeNull(() => source);
			ArgumentMust.NotBeNull(() => action);

			foreach (var item in source)
			{
				action(item);
			}
		}

		public static void ForEach<T>(this IEnumerable<T> source, Action action)
		{
			ArgumentMust.NotBeNull(() => source);
			ArgumentMust.NotBeNull(() => action);

			foreach (var item in source)
			{
				action();
			}
		}

		public static bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			ArgumentMust.NotBeNull(() => source);
			ArgumentMust.NotBeNull(() => predicate);

			return EnumerablePredicates.None(source, predicate);
		}
		
		public static bool IsEmpty<T>(this IEnumerable<T> source)
		{
			ArgumentMust.NotBeNull(() => source);

			return EnumerablePredicates.IsEmpty(source);
		}
	}
}
