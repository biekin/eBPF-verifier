using System;
namespace eBPF_verifier
{
	public class Example3
	{
		public Example3() { }

		public void Execute()
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
            analyzer.PrintEquations();
            analyzer.Solve();
            analyzer.PrintCurrentStates();
        }
	}
}

