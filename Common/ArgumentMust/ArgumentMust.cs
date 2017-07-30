﻿namespace Common.ArgumentMust
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;

	public static class ArgumentMust
	{
		[DebuggerHidden]
		public static void NotBeNull<T>(Func<T> argumentFunc) where T : class
		{
			if (argumentFunc == null)
			{
				throw new ArgumentNullException(nameof(argumentFunc));
			}

			InternalArgumentMust.NotBeNull(argumentFunc);
		}

		[DebuggerHidden]
		public static void NotBeNullOrEmpty<T>(Func<IEnumerable<T>> argumentFunc)
		{
			ArgumentMust.NotBeNull(argumentFunc);

			InternalArgumentMust.NotBeEmpty(argumentFunc);
		}

		[DebuggerHidden]
		public static void BeGreater<T>(Func<T> argumentFunc, T lowerLimit) where T : struct, IComparable
		{
			InternalArgumentMust.BeGreater(argumentFunc, lowerLimit);
		}

		[DebuggerHidden]
		public static void BeGreaterOrEqual<T>(Func<T> argumentFunc, T lowerLimit) where T : struct, IComparable
		{
			InternalArgumentMust.BeGreaterOrEqual(argumentFunc, lowerLimit);
		}

		[DebuggerHidden]
		public static void BeEnum(Func<Type> argumentFunc)
		{
			InternalArgumentMust.BeEnum(argumentFunc);
		}
	}
}