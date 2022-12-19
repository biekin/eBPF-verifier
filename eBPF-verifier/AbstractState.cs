using System;
namespace eBPF_verifier
{
	public class AbstractState
	{
		private Dictionary<IProgramVariable, Interval> VariablesIntervals;

		public AbstractState()
		{
		}
	}
}

