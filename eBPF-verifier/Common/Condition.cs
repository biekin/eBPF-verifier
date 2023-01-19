using System;
namespace eBPF_verifier
{
	public class Condition : IIntervalEvaluable
	{
		public IProgramVariable ProgramVariable { get; private set; }
		private string Inequality;
		private Literal Arg;

		public Condition(IProgramVariable programVariable, string inequality, Literal arg)
		{
			ProgramVariable = programVariable;
			Inequality = inequality;
			Arg = arg;
		}

        public override string ToString()
        {
			return $"{ProgramVariable} {Inequality} {Arg}";
        }

        public Interval GetInterval(AbstractState abstractState)
        {
			var i = Arg.GetInequalityInterval(Inequality);
			return Interval.GreatestLowerBound(i, abstractState.GetIntervalOfRegister(ProgramVariable));
        }
    }
}

