using eBPF_verifier;
namespace eBPF_verifierTests;

[TestClass]
public class BasicIteratorTests
{
    [TestMethod]
    public void BasicIteratorSimpleTest()
    {
        CFG ex1 = new CFG();

        Register x = new Register("x");

        ProgramPoint p0 = new ProgramPoint("0");
        ProgramPoint p1 = new ProgramPoint("1");
        ProgramPoint p2 = new ProgramPoint("2");
        ProgramPoint p3 = new ProgramPoint("3");

        var e1 = new AssignmentEdge(p0, p1, x, new ArithmeticExpresion(new Literal(100), new Literal(0), ArithmeticOperation.Add)); // workaround
        var e2 = new BranchEdge(p1, p2, new Condition(x, ">", new Literal(0)));
        var e3 = new AssignmentEdge(p2, p1, x, new ArithmeticExpresion(x, new Literal(1), ArithmeticOperation.Subtract));
        var e4 = new BranchEdge(p1, p3, new Condition(x, "<=", new Literal(0)));

        ex1.AddNode(p0);
        ex1.AddNode(p1);
        ex1.AddNode(p2);
        ex1.AddNode(p3);
        ex1.AddEdge(e1);
        ex1.AddEdge(e2);
        ex1.AddEdge(e3);
        ex1.AddEdge(e4);

        var analyzer = new Analyzer(ex1, new BasicIterator());
        analyzer.GenerateEquations();
        var solution = analyzer.Solve();

        var expectedSolution = new Solution();
        var pp0ExpectedAS = new AbstractState();
        pp0ExpectedAS.AddVariableTristate(x, null);
        var pp1ExpectedAS = new AbstractState();
        pp1ExpectedAS.AddVariableInterval(x, new Interval(0, 100));
        var pp2ExpectedAS = new AbstractState();
        pp2ExpectedAS.AddVariableInterval(x, new Interval(1, 100));
        var pp3ExpectedAS = new AbstractState();
        pp3ExpectedAS.AddVariableInterval(x, new Interval(0, 0));
        expectedSolution.AddOrUpdateProgramPoint("0", pp0ExpectedAS);
        expectedSolution.AddOrUpdateProgramPoint("1", pp1ExpectedAS);
        expectedSolution.AddOrUpdateProgramPoint("2", pp2ExpectedAS);
        expectedSolution.AddOrUpdateProgramPoint("3", pp3ExpectedAS);

        Assert.IsTrue(expectedSolution.IsEqualTo(solution));

        //PP#0:
        //x -> 
        //PP#1:
        //x -> Interval: [0, 100]
        //PP#2:
        //x -> Interval: [1, 100]
        //PP#3:
        //x -> Interval: [0, 0]
    }

    [TestMethod]
    [ExpectedException(typeof(AIException))]
    public void BasicIteratorLimitTest()
    {
        CFG ex1 = new CFG();

        Register x = new Register("x");

        ProgramPoint p0 = new ProgramPoint("0");
        ProgramPoint p1 = new ProgramPoint("1");
        ProgramPoint p2 = new ProgramPoint("2");
        ProgramPoint p3 = new ProgramPoint("3");

        var e1 = new AssignmentEdge(p0, p1, x, new ArithmeticExpresion(new Literal(100), new Literal(0), ArithmeticOperation.Add)); // workaround
        var e2 = new BranchEdge(p1, p2, new Condition(x, ">", new Literal(0)));
        var e3 = new AssignmentEdge(p2, p1, x, new ArithmeticExpresion(x, new Literal(1), ArithmeticOperation.Subtract));
        var e4 = new BranchEdge(p1, p3, new Condition(x, "<=", new Literal(0)));

        ex1.AddNode(p0);
        ex1.AddNode(p1);
        ex1.AddNode(p2);
        ex1.AddNode(p3);
        ex1.AddEdge(e1);
        ex1.AddEdge(e2);
        ex1.AddEdge(e3);
        ex1.AddEdge(e4);

        var analyzer = new Analyzer(ex1, new BasicIterator(20));
        analyzer.GenerateEquations();
        var solution = analyzer.Solve();
    }

    [TestMethod]
    public void BasicIteratorTwoVariables()
    {
        var CFG = new CFG();

        var x = new Register("x");
        var y = new Register("y");

        var p0 = new ProgramPoint("0");
        var p1 = new ProgramPoint("1");
        var p2 = new ProgramPoint("2");
        var p3 = new ProgramPoint("3");
        var p4 = new ProgramPoint("4");
        var p5 = new ProgramPoint("5");
        var p6 = new ProgramPoint("6");


        var e1 = new AssignmentEdge(p0, p1, x, new ArithmeticExpresion(new Literal(3), new Literal(0), ArithmeticOperation.Add));
        var e2 = new BranchEdge(p1, p2, new Condition(x, ">", new Literal(0)));
        var e3 = new AssignmentEdge(p2, p3, y, new ArithmeticExpresion(new Literal(2), new Literal(0), ArithmeticOperation.Add));
        var e4 = new BranchEdge(p1, p4, new Condition(x, "<=", new Literal(0)));
        var e5 = new AssignmentEdge(p4, p5, y, new ArithmeticExpresion(new Literal(-2), new Literal(0), ArithmeticOperation.Add));
        var e6 = new AssignmentEdge(p3, p6, x, new ArithmeticExpresion(x, new Literal(0), ArithmeticOperation.Add));
        var e7 = new AssignmentEdge(p5, p6, x, new ArithmeticExpresion(x, new Literal(0), ArithmeticOperation.Add));


        CFG.AddNode(p0);
        CFG.AddNode(p1);
        CFG.AddNode(p2);
        CFG.AddNode(p3);
        CFG.AddNode(p4);
        CFG.AddNode(p5);
        CFG.AddNode(p6);

        CFG.AddEdge(e1);
        CFG.AddEdge(e2);
        CFG.AddEdge(e3);
        CFG.AddEdge(e4);
        CFG.AddEdge(e5);
        CFG.AddEdge(e6);
        CFG.AddEdge(e7);

        var analyzer = new Analyzer(CFG, new BasicIterator());
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
        pp2ExpectedAS.AddVariableInterval(x, new Interval(3, 3));
        pp2ExpectedAS.AddVariableTristate(y, null);
        var pp3ExpectedAS = new AbstractState();
        pp3ExpectedAS.AddVariableInterval(x, new Interval(3, 3));
        pp3ExpectedAS.AddVariableInterval(y, new Interval(2, 2));
        var pp4ExpectedAS = new AbstractState();
        pp4ExpectedAS.AddVariableTristate(x, null);
        pp4ExpectedAS.AddVariableTristate(y, null);
        var pp5ExpectedAS = new AbstractState();
        pp5ExpectedAS.AddVariableTristate(x, null);
        pp5ExpectedAS.AddVariableInterval(y, new Interval(-2, -2));
        var pp6ExpectedAS = new AbstractState();
        pp6ExpectedAS.AddVariableInterval(x, new Interval(3, 3));
        pp6ExpectedAS.AddVariableInterval(y, new Interval(-2, 2));

        expectedSolution.AddOrUpdateProgramPoint("0", pp0ExpectedAS);
        expectedSolution.AddOrUpdateProgramPoint("1", pp1ExpectedAS);
        expectedSolution.AddOrUpdateProgramPoint("2", pp2ExpectedAS);
        expectedSolution.AddOrUpdateProgramPoint("3", pp3ExpectedAS);
        expectedSolution.AddOrUpdateProgramPoint("4", pp4ExpectedAS);
        expectedSolution.AddOrUpdateProgramPoint("5", pp5ExpectedAS);
        expectedSolution.AddOrUpdateProgramPoint("6", pp6ExpectedAS);

        Assert.IsTrue(expectedSolution.IsEqualTo(solution));

        //PP#0:
        //x->
        //y->

        //PP#1:
        //x->Interval: [3, 3]
        //y->

        //PP#2:
        //x->Interval: [3, 3]
        //y->

        //PP#3:
        //x->Interval: [3, 3]
        //y->Interval: [2, 2]

        //PP#4:
        //x -> 
        //y->

        //PP#5:
        //x->
        //y->Interval: [-2, -2]

        //PP#6:
        //x -> Interval: [3, 3]
        //y->Interval: [-2, 2]
    }
}