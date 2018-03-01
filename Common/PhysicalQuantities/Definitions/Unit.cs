namespace Common.PhysicalQuantities.Definitions
{
	using System;

	[Serializable]
	public abstract class Unit<TPhysicalQuantity> : IUnitBase
		where TPhysicalQuantity : IPhysicalQuantity<TPhysicalQuantity>
	{
		public abstract string Symbol { get; }

		public abstract double FactorToBaseUnit { get; }
	}
}