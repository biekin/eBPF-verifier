using System;
namespace eBPF_verifier
{
	public class Interval : IIntervalEvaluable
	{
		public int From { get; private set; }
		public int To { get; private set; }

		public Interval(int from, int to)
		{
			From = from;
			To = to;
		}

		public static Interval Add(Interval a, Interval b)
		{
			if (a == null) return null;
			if (b == null) return null;
			return new Interval(a.From + b.From, a.To + b.To);
		}

        public static Interval Subtract(Interval a, Interval b)
        {
            if (a == null) return null;
            if (b == null) return null;
            return new Interval(a.From - b.From, a.To - b.To);
        }

		public static Interval GreatestLowerBound(Interval a, Interval b)
        {
            if (a == null) return null;
            if (b == null) return null;
            return new Interval(Math.Min(a.From, b.From), Math.Max(a.To, b.To));
		}

		public static Interval LeastUpperBound(Interval a, Interval b)
        {
			if (a == null) return b;
			if (b == null) return a;
			return Normalize(new Interval(Math.Max(a.From, b.From), Math.Min(a.To, b.To)));
		}

		public static Interval PerformIntervalOperation(Interval a, Interval? b, IntervalOperation operation)
		{
			switch (operation)
			{
				case IntervalOperation.Add:
					return Add(a, b);
				case IntervalOperation.Subtract:
					return Subtract(a, b);
				case IntervalOperation.LeastUpperBound:
					return LeastUpperBound(a, b);
				case IntervalOperation.GreatestLowerBound:
					return GreatestLowerBound(a, b);
				default:
					return a;
			}
		}

		private static Interval Normalize(Interval i)
		{
			return (i.From < i.To) ? i : null; // TODO better indicate empty interval
		}

        public override string ToString()
        {
			return $"Interval: [{From}, {To}]";
        }

        public Interval GetInterval(AbstractState abstractState)
        {
			return this;
        }
    }
}

