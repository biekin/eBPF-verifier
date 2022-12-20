using System;
namespace eBPF_verifier
{
	public class Solution
	{
		private Dictionary<ProgramPoint, AbstractState> FixpointState = new Dictionary<ProgramPoint, AbstractState>();

		public Solution() { }
	}
}

