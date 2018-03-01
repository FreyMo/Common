namespace Common.PhysicalQuantities.Definitions
{
	public interface IPhysicalQuantityBase
	{
		double Value { get; }

		string UnitSymbol { get; }
	}
}