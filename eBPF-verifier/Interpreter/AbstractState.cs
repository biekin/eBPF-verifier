using System;
using System.Text;
using eBPF_verifier.Common;

namespace eBPF_verifier
{
	public class AbstractState
	{
		public Dictionary<IProgramVariable, Interval> VariablesIntervals = new Dictionary<IProgramVariable, Interval>();

		public Dictionary<IProgramVariable, TristateNumber> VariablesTristates = new Dictionary<IProgramVariable, TristateNumber>();

		public AbstractState() { }

		public AbstractState(AbstractState another)
		{
			foreach(var v in another.VariablesIntervals)
			{
				VariablesIntervals.Add(v.Key, v.Value);
			}
            foreach (var v in another.VariablesTristates)
            {
                VariablesTristates.Add(v.Key, v.Value);
            }
        }

		public AbstractState(List<IProgramVariable> variables)
		{
			foreach(var v in variables)
			{
				Add(v);
			}
		}

		public void Add(IProgramVariable v, Interval interval = null)
		{
			if (!VariablesIntervals.ContainsKey(v))
			{
				VariablesIntervals.Add(v, interval);
			}
		}

        public void Add(IProgramVariable v, TristateNumber tristate)
        {
            if (!VariablesTristates.ContainsKey(v))
            {
                VariablesTristates.Add(v, tristate);
            }
        }

        public Interval GetIntervalOfRegister(IProgramVariable r)
		{
			if (VariablesIntervals.ContainsKey(r)) return VariablesIntervals[r];
			return null;
		}

		public TristateNumber GetTristateNumberOfRegister(IProgramVariable r)
		{
			return VariablesTristates[r];
		}

        public override string ToString()
        {
			var sb = new StringBuilder();
			foreach(var varInterval in VariablesIntervals)
			{
				sb.Append($"{varInterval.Key} -> {varInterval.Value}\n");
			}
			foreach(var varTristate in VariablesTristates)
			{
				sb.Append($"{varTristate.Key} -> {varTristate.Value}\n");
			}
			return sb.ToString();
        }

		public void UpdateVariableInterval(IProgramVariable variable, Interval interval)
		{
			if (VariablesIntervals.ContainsKey(variable))
			{
				VariablesIntervals[variable] = interval;
			}
			else Add(variable, interval);
		}

		public static AbstractState LeastUpperBound(AbstractState a, AbstractState b)
		{
			var newState = new AbstractState();
			foreach(var v in a.VariablesIntervals.Keys)
			{
				var interval = Interval.LeastUpperBound(a.VariablesIntervals[v], b.VariablesIntervals[v]);
				newState.Add(v, interval);
			}
			return newState;
		}

		public void ExtendIntervalOfVariable(IProgramVariable variable, Interval interval)
		{
			if (!VariablesIntervals.ContainsKey(variable))
			{
				Add(variable, interval);
			} else
			{
				VariablesIntervals[variable] = Interval.LeastUpperBound(VariablesIntervals[variable], interval);
			}
		}
    }
}

