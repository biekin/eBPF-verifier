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

		// TODO proper name
		public Interval SetSum(Interval another)
		{
			return new Interval(Math.Min(From, another.From), Math.Max(To, another.To));
		}

		// TODO proper name
		public Interval SetIntersection(Interval another)
		{
			return Normalize(new Interval(Math.Max(From, another.From), Math.Min(To, another.To)));
		}

		private static Interval Normalize(Interval i)
		{
			return (i.From < i.To) ? i : null; // TODO better indicate empty interval
		}

        public override string ToString()
        {
			return $"Interval: [{From}, {To}]";
        }
    }
}

