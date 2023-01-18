using System;
using System.Text;
namespace eBPF_verifier
{
	public class Solution
	{
		public HashSet<ProgramPoint> FixpointState { get; private set; }

		public Solution()
		{
			FixpointState = new HashSet<ProgramPoint>();
		}
		public Solution(HashSet<ProgramPoint> fixpoint)
		{
			FixpointState = fixpoint;
		}

		public void AddProgramPoint(ProgramPoint programPoint)
		{
			FixpointState.Add(programPoint);
		}

        public override string ToString()
        {
			var sb = new StringBuilder();
			sb.Append("SOLUTION:\n");
			foreach(var pp in FixpointState)
			{
				sb.Append($"{pp}:\n{pp.AbstractState}");
			}
			return sb.ToString();
        }

		public bool IsEqualTo(Solution another)
		{
			var isEqual = true;
			foreach(var programPoint in FixpointState)
			{
				var ppAnother = another.FixpointState.FirstOrDefault(pp => pp.Label == programPoint.Label);
				foreach((var v, var i) in programPoint.AbstractState.VariablesIntervals)
				{
					if (!ppAnother.AbstractState.VariablesIntervals[v].IsEqualTo(i))
					{
						isEqual = false;
					}
				}
			}
			return isEqual;
		}
    }
}

