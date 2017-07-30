namespace Common.Predicates
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	internal static class EnumerablePredicates
	{
		public static bool None<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
		{
			return !sequence.Any(predicate);
		}

		public static bool IsEmpty<T>(this IEnumerable<T> sequence)
		{
			return !sequence.Any();
		}
	}
}