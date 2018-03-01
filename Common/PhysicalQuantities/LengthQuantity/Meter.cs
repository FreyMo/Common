namespace Common.PhysicalQuantities.LengthQuantity
{
	using System;
	using Definitions;

	[Serializable]
	public sealed class Meter : BaseUnit<Length>
	{
		public static Meter Instance { get; } = new Meter();

		public override string Symbol => "m";
	}
}