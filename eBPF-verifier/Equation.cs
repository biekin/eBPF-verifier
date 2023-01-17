using System;
namespace eBPF_verifier
{
	public class Equation
	{
		public ProgramPoint ProgramPoint { get; private set; }
        private AbstractExpression AbstractExpression;

		public Equation(ProgramPoint programPoint, AbstractExpression abstractExpression)
		{
			ProgramPoint = programPoint;
            AbstractExpression = abstractExpression;
		}

		public void Update()
		{
			var argStates = new List<AbstractState>();
			foreach (var arg in AbstractExpression.Arguments)
			{
				var variable = arg.EdgeExpression.ProgramVariable;
				var abstractState = new AbstractState(arg.ProgramPointFrom.AbstractState);
				var interval = arg.EdgeExpression.IntervalEvaluableExpression.GetInterval(abstractState);
				abstractState.Update(variable, interval);
				argStates.Add(abstractState);
			}
			var newState = new AbstractState(ProgramPoint.AbstractState);
			foreach(var s in argStates)
			{
				newState = AbstractState.LeastUpperBound(newState, s);
			}
			ProgramPoint.AbstractState = newState;
		}

		public override string ToString()
        {
            return $"{ProgramPoint} := {AbstractExpression}\n";
        }
    }
}

