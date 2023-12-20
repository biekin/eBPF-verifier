using System.Text;
namespace eBPF_verifier
{
	public class Solution
	{
		public Dictionary<string, AbstractState> FixpointState { get; private set; }

		public Solution()
		{
			FixpointState = new Dictionary<string, AbstractState>();
		}

		public Solution(Dictionary<string, AbstractState> fixpoint)
		{
			FixpointState = fixpoint;
		}

        public void AddOrUpdateProgramPoint(string ppLabel, AbstractState abstractState)
        {
            if (FixpointState.ContainsKey(ppLabel))
            {
                FixpointState[ppLabel] = new AbstractState(abstractState);
            }
            else
            {
                FixpointState.Add(ppLabel, new AbstractState(abstractState));
            }
        }

        public override string ToString()
        {
			var sb = new StringBuilder();
			sb.Append("SOLUTION:\n");
			foreach(var pp in FixpointState)
			{
				sb.Append($"PP#{pp.Key}:\n{pp.Value}");
			}
			return sb.ToString();
        }

		public bool IsEqualTo(Solution another)
		{
			if (another == null) return false;
			var isEqual = true;
			foreach(var programPoint in FixpointState.Keys)
			{
				var anotherAbstractState = another.FixpointState[programPoint];
				foreach(var variableInterval in FixpointState[programPoint].VariablesIntervals)
				{
					var v = variableInterval.Key;
					var i = variableInterval.Value;
                    var anotherVariableInterval = anotherAbstractState.GetIntervalOfRegister(v);
					if (anotherVariableInterval != null)
					{
						if (!anotherVariableInterval.IsEqualTo(i))
						{
							isEqual = false;
						}
					}
					else if (i != null) isEqual = false;
                    
				}
                foreach (var variableTristate in FixpointState[programPoint].VariablesTristates)
                {
                    var v = variableTristate.Key;
                    var i = variableTristate.Value;
                    var anotherVariableTristate = anotherAbstractState.GetTristateNumberOfRegister(v);
                    if (anotherVariableTristate != null)
                    {
                        if (!anotherVariableTristate.isEqualTo(i))
                        {
                            isEqual = false;
                        }
                    }
                    else if (i != null) isEqual = false;

                }
            }
			return isEqual;
		}
    }
}

