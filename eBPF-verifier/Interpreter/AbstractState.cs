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
				AddVariableInterval(v);
				AddVariableTristate(v);
			}
		}

		public void AddVariableInterval(IProgramVariable v, Interval interval = null)
		{
			if (!VariablesIntervals.ContainsKey(v))
			{
				VariablesIntervals.Add(v, interval);
			}
		}

        public void AddVariableTristate(IProgramVariable v, TristateNumber tristate = null)
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
			else AddVariableInterval(variable, interval);
		}

		public static AbstractState LeastUpperBound(AbstractState a, AbstractState b)
		{
			var newState = new AbstractState();
			foreach(var v in a.VariablesIntervals.Keys)
			{
				var interval = Interval.LeastUpperBound(a.VariablesIntervals[v], b.VariablesIntervals[v]);
				newState.AddVariableInterval(v, interval);
			}
            foreach (var v in a.VariablesTristates.Keys)
            {
                var tristate = TristateNumber.LeastUpperBound(a.VariablesTristates[v], b.VariablesTristates[v]);
                newState.AddVariableTristate(v, tristate);
            }
            return newState;
		}

		public void ExtendIntervalOfVariable(IProgramVariable variable, Interval interval)
		{
			if (!VariablesIntervals.ContainsKey(variable))
			{
				AddVariableInterval(variable, interval);
			} else
			{
				VariablesIntervals[variable] = Interval.LeastUpperBound(VariablesIntervals[variable], interval);
			}
		}
    }
}

