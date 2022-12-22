using System;
namespace eBPF_verifier
{
	public class AbstractState
	{
		private List<Interval> VariablesIntervals = new List<Interval>();

		public AbstractState() { }
	}
}

