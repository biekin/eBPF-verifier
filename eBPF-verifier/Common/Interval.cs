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

		public static int IntervalIntMultiply(int a, int b)
		{
            if (a == int.MaxValue && b < 0) return int.MinValue;
            if (a < 0 && b == int.MaxValue) return int.MinValue;
			if (a == int.MinValue && b < 0) return int.MaxValue;
			if (a < 0 && b == int.MinValue) return int.MaxValue;
			if (a == int.MaxValue || b == int.MaxValue) return int.MaxValue;
			if (a == int.MinValue || b == int.MinValue) return int.MinValue;
            if (a == 0 || b == 0) return 0;
            if (Math.Abs(a) >= int.MaxValue / Math.Abs(b)) {
				if (a < 0 && b < 0) return int.MaxValue;
				if (a < 0 || b < 0) return int.MinValue;
				return int.MaxValue;
			}
			return a * b;
        }

        public static int IntervalIntDivide(int a, int b)
        {
			if (a == 0) return 0;
            if (a == int.MaxValue && b < 0) return int.MinValue;
            if (b == int.MaxValue) return 0;
            if (a == int.MinValue && b < 0) return int.MaxValue;
            if (b == int.MinValue) return 0;
			if (a == int.MaxValue) return int.MaxValue;
            if (a == int.MinValue) return int.MinValue;
            return a / b;
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
            return new Interval(IntervalIntSubtract(a.From, b.To), IntervalIntSubtract(a.To, b.From));
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

		public static Interval Multiply(Interval a, Interval b)
        {
			if (a == null) return null;
			if (b == null) return null;
			var ac = IntervalIntMultiply(a.From, b.From);
            var ad = IntervalIntMultiply(a.From, b.To);
            var bc = IntervalIntMultiply(a.To, b.From);
			var bd = IntervalIntMultiply(a.To, b.To);
            return new Interval(Math.Min(Math.Min(ac, ad), Math.Min(bc, bd)), Math.Max(Math.Max(ac, ad), Math.Max(bc, bd)));
        }

		public static Interval Divide(Interval a, Interval b)
        {
			if (a == null) return null;
			if (b == null) return null;
            var ac = IntervalIntDivide(a.From, b.From == 0 ? (b.To > 0 ? 1 : -1)  : b.From);
            var ad = IntervalIntDivide(a.From, b.To == 0 ? (b.From < 0 ? 1 : -1) : b.To);
            var bc = IntervalIntDivide(a.To, b.From == 0 ? (b.To > 0 ? 1 : -1) : b.From);
            var bd = IntervalIntDivide(a.To, b.To == 0 ? (b.From < 0 ? 1 : -1) : b.To);
            return new Interval(Math.Min(Math.Min(ac, ad), Math.Min(bc, bd)), Math.Max(Math.Max(ac, ad), Math.Max(bc, bd)));
        }

		public static Interval Modulo(Interval a, Interval b)
        {
			if (a == null) return null;
			if (b == null) return null;
			if (b.From == 0 && b.To == 0) return new Interval(0, 0);
			var maxAbs = Math.Max(Math.Abs(b.From), Math.Abs(b.To));
			var maxAbsInf = maxAbs == int.MaxValue ? int.MaxValue : maxAbs - 1;
			return new Interval(-maxAbsInf, maxAbsInf);
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
				case IntervalOperation.Multiply:
					return Multiply(a, b);
				case IntervalOperation.Divide:
					return Divide(a, b);
				case IntervalOperation.Modulo:
					return Modulo(a, b);
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

