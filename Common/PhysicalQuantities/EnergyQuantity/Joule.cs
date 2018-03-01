namespace Common.PhysicalQuantities.EnergyQuantity
{
	using System;
	using Definitions;

	[Serializable]
	public class Joule : BaseUnit<Energy>
	{
		public static Joule Instance { get; } = new Joule();

		public override string Symbol => "J";
	}
}