using System;
namespace eBPF_verifier
{
	public class Register : IProgramVariable
	{
		public string Name { get; private set; }

		private Interval interval;

		public Register(string name)
		{
			Name = name;
		}

		public Interval GetInterval()
		{
			return interval;
		}
	}
}

