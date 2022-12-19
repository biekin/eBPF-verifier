using System;
namespace eBPF_verifier
{
	public class Interval
	{
		public int From { get; private set; }
		public int To { get; private set; }

		public Interval(int from, int to)
		{
			From = from;
			To = to;
		}

		public Interval Add(Interval another)
		{
			return new Interval(From + another.From, To + another.To);
		}

        public Interval Subtract(Interval another)
        {
            return new Interval(From - another.From, To - another.To);
        }

        public override string ToString()
        {
			return $"Interval: [{From}, {To}]";
        }
    }
}

