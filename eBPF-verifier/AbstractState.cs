using System;
namespace eBPF_verifier
{
	public class AbstractState
	{
		private Dictionary<IProgramVariable, Interval> VariablesIntervals = new Dictionary<IProgramVariable, Interval>();

		public AbstractState() { }

		public void AddVariable(IProgramVariable v)
		{
			if (!VariablesIntervals.ContainsKey(v))
			{
				VariablesIntervals.Add(v, null);
			}
		}

		public Interval GetIntervalOfRegister(IProgramVariable r)
		{
			return VariablesIntervals[r];
		}
	}
}

