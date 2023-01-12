using System;
namespace eBPF_verifier
{
	public interface IIterator
	{
		Solution Solve(List<Equation> equations);
	}
}

