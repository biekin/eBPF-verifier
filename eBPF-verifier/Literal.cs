using System;
namespace eBPF_verifier
{
	public class Literal : IArgument
	{
		private int Value;

		public Literal(int value)
		{
			Value = value;
		}

		public Interval GetInterval()
		{
			return new Interval(Value, Value);
		}

        public override string ToString()
        {
			return Value.ToString();
        }
    }
}

