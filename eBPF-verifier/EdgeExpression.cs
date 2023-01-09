using System;
namespace eBPF_verifier
{
	public class EdgeExpression
	{
		private IProgramVariable ProgramVariable;
		private IIntervalEvaluable IntervalEvaluableExpression;

		public EdgeExpression(IProgramVariable programVariable, IIntervalEvaluable intervalEvaluable)
		{
			ProgramVariable = programVariable;
			IntervalEvaluableExpression = intervalEvaluable;
		}
	}
}

