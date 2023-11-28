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

                if (previousState != null)
                {
                    if (newSolution.IsEqualTo(previousState))
                    {
                        fixpointReached = true;
                    }
                    else
                    {
                        foreach ((var pp, var abstractState) in newSolution.FixpointState)
                        {
                            foreach ((var v, var i) in abstractState.VariablesIntervals)
                            {
                                var previousInterval = previousState.FixpointState[pp].GetIntervalOfRegister(v);

                                if(i != null && previousInterval != null)
                                {
                                    var fromKey = Tuple.Create(pp, v, Direction.Down);
                                    if (i.From < previousInterval.From) 
                                    {
                                        if (changesCounter.ContainsKey(fromKey))
                                        {
                                            var count = changesCounter[fromKey];
                                            changesCounter[fromKey] = count + 1;
                                        }
                                        else
                                        {
                                            changesCounter.Add(fromKey, 1);
                                        }
                                    }
                                    else if (changesCounter.ContainsKey(fromKey))
                                    {
                                        changesCounter[fromKey] = 0;
                                    }

                                    var toKey = Tuple.Create(pp, v, Direction.Up);
                                    if (i.To > previousInterval.To)
                                    {
                                        if (changesCounter.ContainsKey(toKey))
                                        {
                                            var count = changesCounter[toKey];
                                            changesCounter[toKey] = count + 1;
                                        }
                                        else
                                        {
                                            changesCounter.Add(toKey, 1);
                                        }
                                    }
                                    else if (changesCounter.ContainsKey(toKey))
                                    {
                                        changesCounter[toKey] = 0;
                                    }
                                }
                            }
                        }

                        foreach (((var programPoint, var programVariable, var direction), var count) in changesCounter)
                        {
                            if (count > WideningThreshold)
                            {
                                if (direction == Direction.Up)
                                {
                                    var oldProgramPoint = newSolution.FixpointState[programPoint];
                                    var oldInterval = oldProgramPoint.VariablesIntervals[programVariable];
                                    var newInterval = new Interval(oldInterval.From, int.MaxValue);
                                    var newAbstractState = new AbstractState(oldProgramPoint);
                                    newAbstractState.UpdateVariableInterval(programVariable, newInterval);
                                    newSolution.AddOrUpdateProgramPoint(programPoint, newAbstractState);
                                    var eq = analyzer.Equations.FirstOrDefault(e => e.ProgramPoint.Label == programPoint);
                                    eq.ProgramPoint.AbstractState = new AbstractState(newAbstractState);
                                }
                                else
                                {
                                    var oldProgramPoint = newSolution.FixpointState[programPoint];
                                    var oldInterval = oldProgramPoint.VariablesIntervals[programVariable];
                                    var newInterval = new Interval(int.MinValue, oldInterval.To);
                                    var newAbstractState = new AbstractState(oldProgramPoint);
                                    newAbstractState.UpdateVariableInterval(programVariable, newInterval);
                                    newSolution.AddOrUpdateProgramPoint(programPoint, newAbstractState);
                                    var eq = analyzer.Equations.FirstOrDefault(e => e.ProgramPoint.Label == programPoint);
                                    eq.ProgramPoint.AbstractState = new AbstractState(newAbstractState);
                                }
                            }
                        }
                    }
                }
                
                previousState = newSolution;
            }
            return previousState;
        }
    }
}

