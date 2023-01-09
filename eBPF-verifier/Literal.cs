using System;
namespace eBPF_verifier
{
	public class Literal : IArgument
	{
		public int Value { get; set; }

        public Literal(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
			return Value.ToString();
        }

        public Interval GetInterval(AbstractState _)
        {
            return new Interval(Value, Value);
        }
    }
}

