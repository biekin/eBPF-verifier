using System;
namespace eBPF_verifier
{
	public class EdgeExpression : IIntervalEvaluable
    {
		public IProgramVariable ProgramVariable { get; private set; }
		public IIntervalEvaluable IntervalEvaluableExpression { get; private set; }

		public EdgeExpression(IProgramVariable programVariable, IIntervalEvaluable intervalEvaluable)
		{
			ProgramVariable = programVariable;
			IntervalEvaluableExpression = intervalEvaluable;
		}

		public Interval GetInterval(AbstractState abstractState)
		{
			return IntervalEvaluableExpression.GetInterval(abstractState);
		}

        public override string ToString()
        {
			return $"{ProgramVariable}:={IntervalEvaluableExpression}";
        }
    }
}

