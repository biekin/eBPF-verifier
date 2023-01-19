using System;
namespace eBPF_verifier
{
	public class BasicIterator : IIterator
	{
        private int MaxIterations;

        public BasicIterator(int maxIterations = 1000)
        {
            MaxIterations = maxIterations;
        }

        public Solution Solve(Analyzer analyzer)
        {
            Solution solutionCandidate = null;
            int i = 1;
            bool fixpointReached = false;
            while(i <= MaxIterations && !fixpointReached)
            {
                if(i == MaxIterations)
                {
                    throw new AIException($"A fixpoint state was not reached within {MaxIterations} iterations.\n" +
                        $"Try increasing the iterations limit or use a Widening Iterator.");
                }
                foreach(var eq in analyzer.Equations)
                {
                    eq.Update();
                }
                var newSolutionCadidate = analyzer.GetCurrentState();
                if (newSolutionCadidate.IsEqualTo(solutionCandidate))
                {
                    fixpointReached = true;
                }
                solutionCandidate = newSolutionCadidate;
                i++;
            }
            return solutionCandidate;
        }
    }
}

