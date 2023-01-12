using System;
namespace eBPF_verifier
{
	public class BasicIterator : IIterator
	{
		public BasicIterator() { }

        public Solution Solve(List<Equation> equations)
        {
            var fixpoint = new Solution();
            foreach(var eq in equations)
            {
                fixpoint.AddProgramPoint(eq.ProgramPoint);
            }
            return fixpoint;
        }
    }
}

