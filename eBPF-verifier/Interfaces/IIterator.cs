using System;
namespace eBPF_verifier
{
	public interface IIterator
	{
		Solution Solve(Analyzer analyzer);
	}
}

