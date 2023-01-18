using System;
namespace eBPF_verifier
{
	public interface IIterator
	{
		Analyzer Analyzer { get; set; }
		Solution Solve(List<Equation> equations);
	}
}

