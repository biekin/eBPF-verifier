using System;
namespace eBPF_verifier
{
	public class Condition
	{
		private IProgramVariable ProgramVariable;
		private string Ineqaulity;
		private int Value;

		public Condition(IProgramVariable programVariable, string inequality, int value)
		{
			ProgramVariable = programVariable;
			Ineqaulity = inequality;
			Value = value;
		}
	}
}

