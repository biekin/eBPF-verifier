using System;
namespace eBPF_verifier
{
	public class AIException : Exception
	{
		public AIException(string message) : base (message) { }
	}
}

