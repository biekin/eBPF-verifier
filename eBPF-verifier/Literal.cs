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

        public Interval GetInequalityInterval(string inequality)
        {
            switch (inequality)
            {
                case "<":
                    return new Interval(int.MinValue, Value - 1);
                case ">":
                    return new Interval(Value + 1, int.MaxValue);
                case "<=":
                    return new Interval(int.MinValue, Value);
                default:
                    return new Interval(Value, int.MaxValue);

            }
        }
    }
}

