using System;
namespace eBPF_verifier
{
	public class Example2
	{
        //EXAMPLE 2
        //	x := 3
        //	y := 0
        //	while (x>0) {
        //		y := y + 1
        //		x := x - 1 
        //	}

        public Example2() { }

        public void Execute()
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

            Console.WriteLine(ex2);

            var analyzer = new Analyzer(ex2, new BasicIterator());
            analyzer.GenerateEquations();
            //analyzer.PrintEquations();
            //analyzer.PrintCurrentStates();
            //analyzer.Solve();
            //analyzer.PrintCurrentStates();
            //analyzer.Solve();
            //analyzer.PrintCurrentStates();
            //analyzer.Solve();
            //analyzer.PrintCurrentStates();
            //analyzer.Solve();
            //analyzer.PrintCurrentStates();
            var solution = analyzer.Solve();
            Console.WriteLine(solution);
        }
    }
}