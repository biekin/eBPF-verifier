using System;
namespace eBPF_verifier
{
	public class AbstractState
	{
		private List<Register> VariablesIntervals = new List<Register>();

		public AbstractState() { }
	}
}

