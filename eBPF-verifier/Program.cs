using eBPF_verifier;

// EXAMPLE 1
//    x := 100
//    while x < 0 {
//        x := x-1
//    }

CFG ex1cfg = new CFG();

Register x = new Register("x");

ProgramPoint p0 = new ProgramPoint("0");
ProgramPoint p1 = new ProgramPoint("1");
ProgramPoint p2 = new ProgramPoint("2");
ProgramPoint p3 = new ProgramPoint("3");

var e1 = new AssignmentEdge(p0, p1, x, new ArithmeticExpresion(new Literal(100), new Literal(0), "+"));
var e2 = new BranchEdge(p1, p2, new Condition(x, ">", new Literal(0)));
var e3 = new AssignmentEdge(p2, p1, x, new ArithmeticExpresion(x, new Literal(1), "-"));