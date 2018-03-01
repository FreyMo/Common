namespace Common.PhysicalQuantities.VelocityQuantity
{
	using System;
	using Definitions;

	[Serializable]
	public sealed class MillimeterPerSecond : Unit<Velocity>
	{
		public static MillimeterPerSecond Instance { get; } = new MillimeterPerSecond();

		public override string Symbol => "mm/s";

		public override double FactorToBaseUnit => 1.0e-3;
	}
}