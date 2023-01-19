using System;

namespace eBPF_verifier
{
	public class WideningIterator : IIterator
	{
        public int WideningThreshold { get; private set; }

        public WideningIterator(int wideningThreshold)
        {
            WideningThreshold = wideningThreshold;
        }

        public Solution Solve(Analyzer analyzer)
        {
            Solution previousState = null;
            var fixpointReached = false;
            var changesCounter = new Dictionary<Tuple<string, IProgramVariable, Direction>, int>();
            while (!fixpointReached)
            {
                foreach (var eq in analyzer.Equations)
                {
                    eq.Update();
                }
                var newSolution = analyzer.GetCurrentState();
                if (newSolution.IsEqualTo(previousState))
                {
                    fixpointReached = true;
                }
                else
                {
                    foreach((var pp, var abstractState) in newSolution.FixpointState)
                    {
                        foreach((var v, var i) in abstractState.VariablesIntervals)
                        {
                            var previousInterval = previousState.FixpointState[pp].GetIntervalOfRegister(v);

                            var fromKey = Tuple.Create(pp, v, Direction.Down);
                            var count = changesCounter[fromKey];
                            // TODO
                            var toKey = Tuple.Create(pp, v, Direction.Up);
                            // TODO
                        }
                    }
                    // TODO check if apply infinity somewhere
                }
                previousState = newSolution;
            }
            return previousState;
        }
    }
}

