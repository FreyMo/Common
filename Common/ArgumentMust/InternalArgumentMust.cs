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
				var argument = argumentFunc.GetParameter(EnumerablePredicates.IsEmpty);
				throw new ArgumentException($"The sequence '{argument.Name}' must not be empty.", argument.Name);
			}
		}

		[DebuggerHidden]
		public static void BeGreater<T>(Func<T> argumentFunc, T lowerLimit) where T : struct, IComparable
		{
			if (argumentFunc().IsLessOrEqual(lowerLimit))
			{
				var argument = argumentFunc.GetParameter(p => p.IsLessOrEqual(lowerLimit));
				throw new ArgumentException($"The argument '{argument.Name}' must not be less than or equal to {lowerLimit}.", argument.Name);
			}
		}

		[DebuggerHidden]
		public static void BeGreaterOrEqual<T>(Func<T> argumentFunc, T lowerLimit) where T : struct, IComparable
		{
			if (argumentFunc().IsLessThan(lowerLimit))
			{
				var argument = argumentFunc.GetParameter(p => p.IsLessThan(lowerLimit));
				throw new ArgumentException($"The argument '{argument.Name}' must not be less than {lowerLimit}.", argument.Name);
			}
		}
		
		[DebuggerHidden]
		public static void BeLess<T>(Func<T> argumentFunc, T upperLimit) where T : struct, IComparable
		{
			if (argumentFunc().IsGreaterOrEqual(upperLimit))
			{
				var argument = argumentFunc.GetParameter(p => p.IsGreaterOrEqual(upperLimit));
				throw new ArgumentException($"The argument '{argument.Name}' must not be greater than or equal to {upperLimit}.", argument.Name);
			}
		}

		[DebuggerHidden]
		public static void BeLessOrEqual<T>(Func<T> argumentFunc, T upperLimit) where T : struct, IComparable
		{
			if (argumentFunc().IsGreaterThan(upperLimit))
			{
				var argument = argumentFunc.GetParameter(p => p.IsGreaterThan(upperLimit));
				throw new ArgumentException($"The argument '{argument.Name}' must not be greater than {upperLimit}.", argument.Name);
			}
		}

		[DebuggerHidden]
		public static void BeEnum(Func<Type> argumentFunc)
		{
			if (!argumentFunc().IsEnum)
			{
				var argument = argumentFunc.GetParameter(p => !p.IsEnum);
				throw new ArgumentException($"The argument '{argument.Name}' was not of the requested Type enum.", argument.Name);
			}
		}

		[DebuggerHidden]
		public static void NotBeNullOrWhitespace(Func<string> argumentFunc)
		{
			if (string.IsNullOrWhiteSpace(argumentFunc()))
			{
				var argument = argumentFunc.GetParameter(string.IsNullOrWhiteSpace);
				throw new ArgumentException($"The argument '{argument.Name}' must not be null or whitespace.", argument.Name);
			}
		}
	}
}