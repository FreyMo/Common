namespace Common.PhysicalQuantities.VelocityQuantity
{
	using System;
	using Definitions;

	[Serializable]
	public sealed class MeterPerSecond : BaseUnit<Velocity>
	{
		public static MeterPerSecond Instance { get; } = new MeterPerSecond();

		public override string Symbol => "m/s";
	}
}