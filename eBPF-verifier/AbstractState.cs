using System;
using System.Text;
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

        public override string ToString()
        {
			var sb = new StringBuilder();
			foreach(var varInterval in VariablesIntervals)
			{
				sb.Append($"{varInterval.Key} -> {varInterval.Value}\n");
			}
			return sb.ToString();
        }
    }
}

