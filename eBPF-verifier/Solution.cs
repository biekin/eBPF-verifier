using System;
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
	}
}

