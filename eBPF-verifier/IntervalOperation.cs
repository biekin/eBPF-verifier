using System;
namespace eBPF_verifier
{
	public enum IntervalOperation
	{
		Assign, // TODO: is this needed?
		Add,
		Subtract,
		LeastUpperBound,
		GreatestLowerBound
	}
}

