using System;
namespace eBPF_verifier
{
	public class Condition
	{
		private IProgramVariable ProgramVariable;
		private string Ineqaulity;
		private IArgument Arg;

		public Condition(IProgramVariable programVariable, string inequality, IArgument arg)
		{
			ProgramVariable = programVariable;
			Ineqaulity = inequality;
			Arg = arg;
		}

        public override string ToString()
        {
			return $"{ProgramVariable} {Ineqaulity} {Arg}";
        }
    }
}

