using System;
namespace eBPF_verifier
{
	public class ProgramPoint : ICFGNode
	{
		public string Label { get; private set; }

		public ProgramPoint(string label)
		{
			Label = label;
		}

        public override string ToString()
        {
			return Label;
        }
    }
}

