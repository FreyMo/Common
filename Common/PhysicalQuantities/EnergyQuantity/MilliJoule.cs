﻿namespace Common.PhysicalQuantities.EnergyQuantity
{
	using System;
	using Definitions;

	[Serializable]
	public class MilliJoule : Unit<Energy>
	{
		public static MilliJoule Instance { get; } = new MilliJoule();

		public override string Symbol => "mJ";

		public override double FactorToBaseUnit => 1.0e-3;
	}
}