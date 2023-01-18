using System;
namespace eBPF_verifier
{
	public class BasicIterator : IIterator
	{
        private int MaxIterations = 1000;
        public Analyzer Analyzer { get; set; }

        public BasicIterator(int maxIterations = 1000)
        {
            MaxIterations = maxIterations;
        }

        public Solution Solve(List<Equation> equations)
        {
            var solutionCandidate = new Solution();
            int i = 0;
            bool fixpointReached = false;
            while(i < MaxIterations && !fixpointReached)
            {
                foreach(var eq in equations)
                {
                    eq.Update();
                }
                var newSolutionCadidate = Analyzer.GetCurrentState();
                i++;
            }
            foreach(var eq in equations)
            {
                solutionCandidate.AddProgramPoint(eq.ProgramPoint);
            }
            return solutionCandidate;
        }
    }
}

