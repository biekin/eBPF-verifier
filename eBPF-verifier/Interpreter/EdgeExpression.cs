using System;
using eBPF_verifier.Common;
using eBPF_verifier.Interfaces;

namespace eBPF_verifier
{
	public class EdgeExpression : IIntervalEvaluable, ITristateNumberEvaluable
    {
		public IProgramVariable ProgramVariable { get; private set; }
		public IIntervalEvaluable IntervalEvaluableExpression { get; private set; }
		public ITristateNumberEvaluable TristateNumberEvaluableExpression { get; private set; }

		public EdgeExpression(IProgramVariable programVariable, IIntervalEvaluable intervalEvaluable)
		{
			ProgramVariable = programVariable;
			IntervalEvaluableExpression = intervalEvaluable;
		}

		public Interval GetInterval(AbstractState abstractState)
		{
			return IntervalEvaluableExpression.GetInterval(abstractState);
		}

		public TristateNumber GetTristateNumber(AbstractState abstractState)
		{
			return TristateNumberEvaluableExpression.GetTristateNumber(abstractState);
		}

        public override string ToString()
        {
			return $"{ProgramVariable}:={IntervalEvaluableExpression}";
        }
    }
}

