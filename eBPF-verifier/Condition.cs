using System;
namespace eBPF_verifier
{
	public class Condition
	{
		public IProgramVariable ProgramVariable { get; private set; }
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

