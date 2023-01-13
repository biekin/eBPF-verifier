using System;
namespace eBPF_verifier
{
	public class BasicIterator : IIterator
	{
		public BasicIterator() { }
        private int MAX_ITERATIONS = 1000;

        public Solution Solve(List<Equation> equations)
        {
            var fixpoint = new Solution();
            int i = 0;
            bool fixpointReached = false;
            while(i < MAX_ITERATIONS && !fixpointReached)
            {
                foreach(var eq in equations)
                {
                    eq.Update();
                }
                i++;
            }
            foreach(var eq in equations)
            {
                fixpoint.AddProgramPoint(eq.ProgramPoint);
            }
            return fixpoint;
        }
    }
}

