using System;
namespace eBPF_verifier
{
	public class Register : IProgramVariable
	{
		public string Name { get; private set; }

		public Register(string name)
		{
			Name = name;
		}

        public override string ToString()
        {
			return Name;
        }
    }
}

