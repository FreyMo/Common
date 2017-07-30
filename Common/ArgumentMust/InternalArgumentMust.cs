namespace Common.ArgumentMust
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using Predicates;

	internal static class InternalArgumentMust
	{
		[DebuggerHidden]
		public static void NotBeNull<T>(Func<T> argumentFunc) where T : class
		{
			if (argumentFunc().IsNull())
			{
				var nullArgument = argumentFunc.GetParameter(GenericTypePredicates.IsNull);
				throw new ArgumentNullException(nullArgument.Name);
			}
		}

		[DebuggerHidden]
		public static void NotBeEmpty<T>(Func<IEnumerable<T>> argumentFunc)
		{
			if (argumentFunc().IsEmpty())
			{
				var nullArgument = argumentFunc.GetParameter(EnumerablePredicates.IsEmpty);
				throw new FailedToEnsureException($"The sequence '{nullArgument.Name}' is empty.");
			}
		}

		[DebuggerHidden]
		public static void BeGreater<T>(Func<T> argumentFunc, T lowerLimit) where T : struct, IComparable
		{
			if (argumentFunc().IsLessOrEqual(lowerLimit))
			{
				var arg = argumentFunc.GetParameter(p => p.IsLessOrEqual(lowerLimit));
				throw new FailedToEnsureException($"The argument '{arg.Name}' is less than or equal to {lowerLimit}.");
			}
		}

		[DebuggerHidden]
		public static void BeGreaterOrEqual<T>(Func<T> argumentFunc, T lowerLimit) where T : struct, IComparable
		{
			if (argumentFunc().IsLessThan(lowerLimit))
			{
				var arg = argumentFunc.GetParameter(p => p.IsLessThan(lowerLimit));
				throw new FailedToEnsureException($"The argument '{arg.Name}' is less than {lowerLimit}.");
			}
		}

		[DebuggerHidden]
		public static void BeEnum(Func<Type> argumentFunc)
		{
			if (!argumentFunc().IsEnum)
			{
				var arg = argumentFunc.GetParameter(p => !p.IsEnum);
				throw new FailedToEnsureException($"The argument '{arg.Name}' was not of the requested Type enum.");
			}
		}
	}
}