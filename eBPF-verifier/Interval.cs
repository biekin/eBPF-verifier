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

		public Interval(Interval another)
		{
			From = another.From;
			To = another.To;
		}

		public static int IntervalIntAdd(int a, int b)
		{
			if (a == int.MaxValue || b == int.MaxValue) return int.MaxValue;
			if (a == int.MinValue || b == int.MinValue) return int.MinValue;
            return a + b;
		}

		public static int IntervalIntSubtract(int a, int b)
		{
            if (a == int.MaxValue || b == int.MaxValue) return int.MaxValue;
            if (a == int.MinValue || b == int.MinValue) return int.MinValue;
            return a - b;
        }

		public static Interval Add(Interval a, Interval b)
		{
			if (a == null) return null;
			if (b == null) return null;
			return new Interval(IntervalIntAdd(a.From, b.From), IntervalIntAdd(a.To, b.To));
		}

        public static Interval Subtract(Interval a, Interval b)
        {
            if (a == null) return null;
            if (b == null) return null;
            return new Interval(IntervalIntSubtract(a.From, b.From), IntervalIntSubtract(a.To, b.To));
        }

		public static Interval GreatestLowerBound(Interval a, Interval b)
        {
            if (a == null) return null;
            if (b == null) return null;
            return Normalize(new Interval(Math.Max(a.From, b.From), Math.Min(a.To, b.To)));
        }

		public static Interval LeastUpperBound(Interval a, Interval b)
        {
			if (a == null) return b;
			if (b == null) return a;
            return new Interval(Math.Min(a.From, b.From), Math.Max(a.To, b.To));
        }

        public static Interval PerformIntervalOperation(Interval a, Interval b, IntervalOperation operation)
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
			return (i.From <= i.To) ? i : null;
		}

        public override string ToString()
        {
			return $"Interval: [{From}, {To}]";
        }

        public Interval GetInterval(AbstractState abstractState)
        {
			return new Interval(From, To);
        }

		public bool IsEqualTo(Interval another)
		{
			return From == another.From && To == another.To;
		}
    }
}

