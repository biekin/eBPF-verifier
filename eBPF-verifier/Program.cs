using eBPF_verifier;

// EXAMPLE 1
//    x := 100
//    while x < 0 {
//        x := x-1
//    }

CFG ex1 = new CFG();

Register x = new Register("x");

ProgramPoint p0 = new ProgramPoint("0");
ProgramPoint p1 = new ProgramPoint("1");
ProgramPoint p2 = new ProgramPoint("2");
ProgramPoint p3 = new ProgramPoint("3");

var e1 = new AssignmentEdge(p0, p1, x, new ArithmeticExpresion(new Literal(100), new Literal(0), "+")); // workaround
var e2 = new BranchEdge(p1, p2, new Condition(x, ">", new Literal(0)));
var e3 = new AssignmentEdge(p2, p1, x, new ArithmeticExpresion(x, new Literal(1), "-"));
var e4 = new BranchEdge(p1, p3, new Condition(x, "<=", new Literal(0)));

ex1.AddNode(p0);
ex1.AddNode(p1);
ex1.AddNode(p2);
ex1.AddNode(p3);
ex1.AddEdge(e1);
ex1.AddEdge(e2);
ex1.AddEdge(e3);
ex1.AddEdge(e4);

Console.WriteLine(ex1);

var analyzer = new Analyzer(ex1, new BasicIterator());