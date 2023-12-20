using eBPF_verifier;
namespace eBPF_verifierTests;

[TestClass]
public class WideningIteratorTests
{
	[TestMethod]
	public void WideningIteratorSimpleTest()
	{
        CFG ex2 = new CFG();

        Register x = new Register("x");
        Register y = new Register("y");

        var p0 = new ProgramPoint("0");
        var p1 = new ProgramPoint("1");
        var p2 = new ProgramPoint("2");
        var p3 = new ProgramPoint("3");
        var p4 = new ProgramPoint("4");
        var p5 = new ProgramPoint("5");

        var e1 = new AssignmentEdge(p0, p1, x, new ArithmeticExpresion(new Literal(3), new Literal(0), ArithmeticOperation.Add));
        var e2 = new AssignmentEdge(p1, p2, y, new ArithmeticExpresion(new Literal(0), new Literal(0), ArithmeticOperation.Add));
        var e3 = new BranchEdge(p2, p3, new Condition(x, ">", new Literal(0)));
        var e4 = new AssignmentEdge(p3, p4, y, new ArithmeticExpresion(y, new Literal(1), ArithmeticOperation.Add));
        var e5 = new AssignmentEdge(p4, p2, x, new ArithmeticExpresion(x, new Literal(1), ArithmeticOperation.Subtract));
        var e6 = new BranchEdge(p2, p5, new Condition(x, "<=", new Literal(0)));

        ex2.AddNode(p0);
        ex2.AddNode(p1);
        ex2.AddNode(p2);
        ex2.AddNode(p3);
        ex2.AddNode(p4);
        ex2.AddNode(p5);
        ex2.AddEdge(e1);
        ex2.AddEdge(e2);
        ex2.AddEdge(e3);
        ex2.AddEdge(e4);
        ex2.AddEdge(e5);
        ex2.AddEdge(e6);

        var analyzer = new Analyzer(ex2, new WideningIterator(5));
        analyzer.GenerateEquations();
        var solution = analyzer.Solve();

        var expectedSolution = new Solution();
        var pp0ExpectedAS = new AbstractState();
        pp0ExpectedAS.AddVariableTristate(x, null);
        pp0ExpectedAS.AddVariableTristate(y, null);
        var pp1ExpectedAS = new AbstractState();
        pp1ExpectedAS.AddVariableInterval(x, new Interval(3, 3));
        pp1ExpectedAS.AddVariableTristate(y, null);
        var pp2ExpectedAS = new AbstractState();
        pp2ExpectedAS.AddVariableInterval(x, new Interval(0, 3));
        pp2ExpectedAS.AddVariableInterval(y, new Interval(0, int.MaxValue));
        var pp3ExpectedAS = new AbstractState();
        pp3ExpectedAS.AddVariableInterval(x, new Interval(1, 3));
        pp3ExpectedAS.AddVariableInterval(y, new Interval(0, int.MaxValue));
        var pp4ExpectedAS = new AbstractState();
        pp4ExpectedAS.AddVariableInterval(x, new Interval(1, 3));
        pp4ExpectedAS.AddVariableInterval(y, new Interval(1, int.MaxValue));
        var pp5ExpectedAS = new AbstractState();
        pp5ExpectedAS.AddVariableInterval(x, new Interval(0, 0));
        pp5ExpectedAS.AddVariableInterval(y, new Interval(0, int.MaxValue));

        expectedSolution.AddOrUpdateProgramPoint("0", pp0ExpectedAS);
        expectedSolution.AddOrUpdateProgramPoint("1", pp1ExpectedAS);
        expectedSolution.AddOrUpdateProgramPoint("2", pp2ExpectedAS);
        expectedSolution.AddOrUpdateProgramPoint("3", pp3ExpectedAS);
        expectedSolution.AddOrUpdateProgramPoint("4", pp4ExpectedAS);
        expectedSolution.AddOrUpdateProgramPoint("5", pp5ExpectedAS);

        Assert.IsTrue(expectedSolution.IsEqualTo(solution));

        //SOLUTION:
        //    PP#0:
        //    x -> 
        //    y->
        //    PP#1:
        //    x->Interval: [3, 3]
        //    y->
        //    PP#2:
        //    x->Interval: [0, 3]
        //    y->Interval: [0, 2147483647]
        //    PP#3:
        //    x -> Interval: [1, 3]
        //    y->Interval: [0, 2147483647]
        //    PP#4:
        //    x -> Interval: [1, 3]
        //    y->Interval: [1, 2147483647]
        //    PP#5:
        //    x -> Interval: [0, 0]
        //    y->Interval: [0, 2147483647]
    }
}


