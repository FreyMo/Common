﻿namespace Common.PhysicalQuantities.AreaQuantity.Extensions
{
	using System;

	public static class IntegerExtensions
	{
		public static Area SquareMeters(this int value)
		{
			return Convert.ToDouble(value).SquareMeters();
		}

		public static Area SquareCentiMeters(this int value)
		{
			return Convert.ToDouble(value).SquareCentiMeters();
		}

		public static Area SquareMilliMeters(this int value)
		{
			return Convert.ToDouble(value).SquareMilliMeters();
		}
	}
}