using System;

namespace eBPF_verifier
{
	public class WideningIterator : IIterator
	{
        public Analyzer Analyzer { get; set; }

        public WideningIterator() { }

        public Solution Solve(List<Equation> equations)
        {
            throw new NotImplementedException();
        }
    }
}

