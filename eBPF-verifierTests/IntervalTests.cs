using eBPF_verifier;
namespace eBPF_verifierTests;

[TestClass]
public class IntervalTests
{
	[TestMethod]
	public void IntervalAddTest1()
	{
		var interval1 = new Interval(0, 100);
		var interval2 = new Interval(5, 10);
		var actual = Interval.Add(interval1, interval2);
		var expected = new Interval(5, 110);
		Assert.IsTrue(actual.IsEqualTo(expected));
	}

    [TestMethod]
    public void IntervalAddTest2()
    {
        var interval1 = new Interval(0, 0);
        var interval2 = new Interval(0, 0);
        var actual = Interval.Add(interval1, interval2);
        var expected = new Interval(0, 0);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }

    [TestMethod]
    public void IntervalAddTest3()
    {
        var interval1 = new Interval(-1, 1);
        var interval2 = new Interval(-5, 0);
        var actual = Interval.Add(interval1, interval2);
        var expected = new Interval(-6, 1);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }

    [TestMethod]
    public void IntervalAddTest4()
    {
        var interval1 = new Interval(-100, -10);
        var interval2 = new Interval(10, 100);
        var actual = Interval.Add(interval1, interval2);
        var expected = new Interval(-90, 90);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }

    [TestMethod]
    public void IntervalSubTest1()
    {
        var interval1 = new Interval(0, 100);
        var interval2 = new Interval(5, 10);
        var actual = Interval.Subtract(interval1, interval2);
        var expected = new Interval(-10, 95);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }

    [TestMethod]
    public void IntervalSubTest2()
    {
        var interval1 = new Interval(0, 0);
        var interval2 = new Interval(0, 0);
        var actual = Interval.Subtract(interval1, interval2);
        var expected = new Interval(0, 0);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }

    [TestMethod]
    public void IntervalSubTest3()
    {
        var interval1 = new Interval(-1, 1);
        var interval2 = new Interval(-5, 0);
        var actual = Interval.Subtract(interval1, interval2);
        var expected = new Interval(-1, 6);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }

    [TestMethod]
    public void IntervalSubTest4()
    {
        var interval1 = new Interval(-100, -10);
        var interval2 = new Interval(10, 100);
        var actual = Interval.Subtract(interval1, interval2);
        var expected = new Interval(-200, -20);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }

    [TestMethod]
    public void IntervalMultiplyTest1()
    {
        var interval1 = new Interval(0, 100);
        var interval2 = new Interval(5, 10);
        var actual = Interval.Multiply(interval1, interval2);
        var expected = new Interval(0, 1000);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }

    [TestMethod]
    public void IntervalMultiplyTest2()
    {
        var interval1 = new Interval(0, 0);
        var interval2 = new Interval(0, 0);
        var actual = Interval.Multiply(interval1, interval2);
        var expected = new Interval(0, 0);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }

    [TestMethod]
    public void IntervalMultiplyTest3()
    {
        var interval1 = new Interval(-1, 1);
        var interval2 = new Interval(-5, 0);
        var actual = Interval.Multiply(interval1, interval2);
        var expected = new Interval(-5, 5);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }

    [TestMethod]
    public void IntervalMultiplyTest4()
    {
        var interval1 = new Interval(-100, -10);
        var interval2 = new Interval(10, 100);
        var actual = Interval.Multiply(interval1, interval2);
        var expected = new Interval(-10000, -100);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }

    [TestMethod]
    public void IntervalDivideTest1()
    {
        var interval1 = new Interval(0, 100);
        var interval2 = new Interval(5, 10);
        var actual = Interval.Divide(interval1, interval2);
        var expected = new Interval(0, 20);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }

    [TestMethod]
    public void IntervalDivideTest2()
    {
        var interval1 = new Interval(0, 0);
        var interval2 = new Interval(0, 2);
        var actual = Interval.Divide(interval1, interval2);
        var expected = new Interval(0, 0);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }

    [TestMethod]
    public void IntervalDivideTest3()
    {
        var interval1 = new Interval(-1, 1);
        var interval2 = new Interval(-5, 0);
        var actual = Interval.Divide(interval1, interval2);
        var expected = new Interval(-1, 1);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }

    [TestMethod]
    public void IntervalDivideTest4()
    {
        var interval1 = new Interval(-100, -10);
        var interval2 = new Interval(10, 100);
        var actual = Interval.Divide(interval1, interval2);
        var expected = new Interval(-10, 0);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }

    [TestMethod]
    public void IntervalModuloTest1()
    {
        var interval1 = new Interval(0, 100);
        var interval2 = new Interval(5, 10);
        var actual = Interval.Modulo(interval1, interval2);
        var expected = new Interval(-9, 9);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }

    [TestMethod]
    public void IntervalModuloTest2()
    {
        var interval1 = new Interval(0, 0);
        var interval2 = new Interval(0, 0);
        var actual = Interval.Modulo(interval1, interval2);
        var expected = new Interval(0, 0);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }

    [TestMethod]
    public void IntervalModuloTest3()
    {
        var interval1 = new Interval(-1, 1);
        var interval2 = new Interval(-5, 0);
        var actual = Interval.Modulo(interval1, interval2);
        var expected = new Interval(-4, 4);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }

    [TestMethod]
    public void IntervalModuloTest4()
    {
        var interval1 = new Interval(-100, -10);
        var interval2 = new Interval(10, 100);
        var actual = Interval.Modulo(interval1, interval2);
        var expected = new Interval(-99, 99);
        Assert.IsTrue(actual.IsEqualTo(expected));
    }
}


