using System;
using System.Collections.Generic;
using eBPF_verifier;

enum InstructionType {
    ADD_1 = 0x0f, // add dst, src  | dst += src
    ADD_2 = 0x07, // add dst, imm  | dst += imm
    SUB_1 = 0x17, // sub dst, imm  | dst -= imm
    SUB_2 = 0x1f, // sub dst, src  | dst -= src
    MUL_1 = 0x27, // mul dst, imm  | dst *= imm
    MUL_2 = 0x2f, // mul dst, src  | dst *= src
    DIV_1 = 0x37, // div dst, imm  | dst /= imm
    DIV_2 = 0x3f, // div dst, src  | dst /= src
    OR_1 = 0x47, // or dst, imm   | dst \|= im
    OR_2 = 0x4f, // or dst, src   | dst \|= sr
    AND_1 = 0x57, // and dst, imm  | dst &= imm
    AND_2 = 0x5f, // and dst, src  | dst &= src
    LSH_1 = 0x67, // lsh dst, imm  | dst <<= im
    LSH_2 = 0x6f, // lsh dst, src  | dst <<= sr
    RSH_1 = 0x77, // rsh dst, imm  | dst >>= im
    RSH_2 = 0x7f, // rsh dst, src  | dst >>= sr
    NEG = 0x87, // neg dst       | dst = -dst
    MOD_1 = 0x97, // mod dst, imm  | dst %= imm
    MOD_2 = 0x9f, // mod dst, src  | dst %= src
    XOR_1 = 0xa7, // xor dst, imm  | dst ^= imm
    XOR_2 = 0xaf, // xor dst, src  | dst ^= src
    MOV_1 = 0xb7, // mov dst, imm  | dst = imm
    MOV_2 = 0xbf, // mov dst, src  | dst = src
    ARSH_1 = 0xc7, // arsh dst, imm | dst >>= imm
    ARSH_2 = 0xcf, // arsh dst, src | dst >>= src
    ADD_32_1 = 0x04, // add32 dst, imm  | dst += imm
    ADD_32_2 = 0x0c, // add32 dst, src  | dst += src
    SUB_32_1 = 0x14, // sub32 dst, imm  | dst -= imm
    SUB_32_2 = 0x1c, // sub32 dst, src  | dst -= src
    MUL_32_1 = 0x24, // mul32 dst, imm  | dst *= imm
    MUL_32_2 = 0x2c, // mul32 dst, src  | dst *= src
    DIV_32_1 = 0x34, // div32 dst, imm  | dst /= imm
    DIV_32_2 = 0x3c, // div32 dst, src  | dst /= src
    OR_32_1 = 0x44, // or32 dst, imm   | dst \|= imm
    OR_32_2 = 0x4c, // or32 dst, src   | dst \|= src
    AND_32_1 = 0x54, // and32 dst, imm  | dst &= imm
    AND_32_2 = 0x5c, // and32 dst, src  | dst &= src
    LSH_32_1 = 0x64, // lsh32 dst, imm  | dst <<= imm
    LSH_32_2 = 0x6c, // lsh32 dst, src  | dst <<= src
    RSH_32_1 = 0x74, // rsh32 dst, imm  | dst >>= imm (logical)
    RSH_32_2 = 0x7c, // rsh32 dst, src  | dst >>= src (logical)
    NEG_32 = 0x84, // neg32 dst       | dst = -dst
    MOD_32_1 = 0x94, // mod32 dst, imm  | dst %= imm
    MOD_32_2 = 0x9c, // mod32 dst, src  | dst %= src
    XOR_32_1 = 0xa4, // xor32 dst, imm  | dst ^= imm
    XOR_32_2 = 0xac, // xor32 dst, src  | dst ^= src
    MOV_32_1 = 0xb4, // mov32 dst, imm  | dst = imm
    MOV_32_2 = 0xbc, // mov32 dst, src  | dst = src
    ARSH_32_1 = 0xc4, // arsh32 dst, imm | dst >>= imm (arithmetic)
    ARSH_32_2 = 0xcc, // arsh32 dst, src | dst >>= src (arithmetic)
    JMP = 0x05, // ja +off             | PC += off
    JMP_EQ_1 = 0x15, // jeq dst, imm, +off  | PC += off if dst == imm
    JMP_EQ_2 = 0x1d, // jeq dst, src, +off  | PC += off if dst == src
    JMP_GT_1 = 0x25, // jgt dst, imm, +off  | PC += off if dst > imm
    JMP_GT_2 = 0x2d, // jgt dst, src, +off  | PC += off if dst > src
    JMP_GE_1 = 0x35, // jge dst, imm, +off  | PC += off if dst >= imm
    JMP_GE_2 = 0x3d, // jge dst, src, +off  | PC += off if dst >= src
    JMP_LT_1 = 0xa5, // jlt dst, imm, +off  | PC += off if dst < imm
    JMP_LT_2 = 0xad, // jlt dst, src, +off  | PC += off if dst < src
    JMP_LE_1 = 0xb5, // jle dst, imm, +off  | PC += off if dst <= imm
    JMP_LE_2 = 0xbd, // jle dst, src, +off  | PC += off if dst <= src
    JMP_SET_1 = 0x45, // jset dst, imm, +off | PC += off if dst & imm
    JMP_SET_2 = 0x4d, // jset dst, src, +off | PC += off if dst & src
    JMP_NE_1 = 0x55, // jne dst, imm, +off  | PC += off if dst != imm
    JMP_NE_2 = 0x5d, // jne dst, src, +off  | PC += off if dst != src
    JMP_SGT_1 = 0x65, // jsgt dst, imm, +off | PC += off if dst > imm (signed)
    JMP_SGT_2 = 0x6d, // jsgt dst, src, +off | PC += off if dst > src (signed)
    JMP_SGE_1 = 0x75, // jsge dst, imm, +off | PC += off if dst >= imm (signed)
    JMP_SGE_2 = 0x7d, // jsge dst, src, +off | PC += off if dst >= src (signed)
    JMP_SLT_1 = 0xc5, // jslt dst, imm, +off | PC += off if dst < imm (signed)
    JMP_SLT_2 = 0xcd, // jslt dst, src, +off | PC += off if dst < src (signed)
    JMP_SLE_1 = 0xd5, // jsle dst, imm, +off | PC += off if dst <= imm (signed)
    JMP_SLE_2 = 0xdd, // jsle dst, src, +off | PC += off if dst <= src (signed)
    CALL = 0x85, // call imm            | Function call
    EXIT = 0x95 // exit                | return r0
}

class EBPFInstruction {
  public InstructionType instruction;
  public int src;
  public int dst;
  public int offset;
  public int immediate;

  public EBPFInstruction(InstructionType instruction, int src, int dst, int offset, int immediate)
  {
    this.instruction = instruction;
    this.src = src;
    this.dst = dst;
    this.offset = offset;
    this.immediate = immediate;
  }

  public string toString() {
    return instruction + " " + src + " " + dst + " " + offset + " " + immediate;
  }
}

class ParserAsm {
    public static Dictionary<string, InstructionType> instructionsRev = new Dictionary<string, InstructionType>() {
                {"add1" , InstructionType.ADD_1},
                {"add2" , InstructionType.ADD_2},
                {"sub1" , InstructionType.SUB_1},
                {"sub2" , InstructionType.SUB_2},
                {"mul1" , InstructionType.MUL_1},
                {"mul2" , InstructionType.MUL_2},
                {"div1" , InstructionType.DIV_1},
                {"div2" , InstructionType.DIV_2},
                {"neg" , InstructionType.NEG},
                {"mod1" , InstructionType.MOD_1},
                {"mod2" , InstructionType.MOD_2},
                {"mov1" , InstructionType.MOV_1},
                {"mov2" , InstructionType.MOV_2},
                {"jmp" , InstructionType.JMP},
                {"jgt1" , InstructionType.JMP_GT_1},
                {"jgt2" , InstructionType.JMP_GT_2},
                {"jge1" , InstructionType.JMP_GE_1},
                {"jge2" , InstructionType.JMP_GE_2},
                {"jlt1" , InstructionType.JMP_LT_1},
                {"jlt2" , InstructionType.JMP_LT_2},
                {"jle1" , InstructionType.JMP_LE_1},
                {"jle2" , InstructionType.JMP_LE_2},
                {"exit" , InstructionType.EXIT}
  };

  private static EBPFInstruction buildInstruction(string line)
   {
      var parts = line.Split(' ');
      InstructionType instruction = instructionsRev[parts[0]];
      int dst = int.Parse(parts[1]);
      int src = int.Parse(parts[2]);
      int offset = int.Parse(parts[3]);
      int immediate = int.Parse(parts[4]);
      return new EBPFInstruction(instruction, dst, src, offset, immediate);
   }

  public static List<EBPFInstruction> parse(string path) {
    List<EBPFInstruction> instructions = new List<EBPFInstruction>();
    using (StreamReader sr = new StreamReader(path))
    {
      string line;
      while ((line = sr.ReadLine()) != null)
      {
        instructions.Add(buildInstruction(line));
      }
    }
    return instructions;
  }
}

class ParserBin {
    public static Dictionary<string, InstructionType> instructionsRev = new Dictionary<string, InstructionType>() {
                {"0f" , InstructionType.ADD_1},
                {"07" , InstructionType.ADD_2},
                {"17" , InstructionType.SUB_1},
                {"1f" , InstructionType.SUB_2},
                {"27" , InstructionType.MUL_1},
                {"2f" , InstructionType.MUL_2},
                {"37" , InstructionType.DIV_1},
                {"3f" , InstructionType.DIV_2},
                {"47" , InstructionType.OR_1},
                {"4f" , InstructionType.OR_2},
                {"57" , InstructionType.AND_1},
                {"5f" , InstructionType.AND_2},
                {"67" , InstructionType.LSH_1},
                {"6f" , InstructionType.LSH_2},
                {"77" , InstructionType.RSH_1},
                {"7f" , InstructionType.RSH_2},
                {"87" , InstructionType.NEG},
                {"97" , InstructionType.MOD_1},
                {"9f" , InstructionType.MOD_2},
                {"a7" , InstructionType.XOR_1},
                {"af" , InstructionType.XOR_2},
                {"b7" , InstructionType.MOV_1},
                {"bf" , InstructionType.MOV_2},
                {"c7" , InstructionType.ARSH_1},
                {"cf" , InstructionType.ARSH_2},
                {"04" , InstructionType.ADD_32_1},
                {"0c" , InstructionType.ADD_32_2},
                {"14" , InstructionType.SUB_32_1},
                {"1c" , InstructionType.SUB_32_2},
                {"24" , InstructionType.MUL_32_1},
                {"2c" , InstructionType.MUL_32_2},
                {"34" , InstructionType.DIV_32_1},
                {"3c" , InstructionType.DIV_32_2},
                {"44" , InstructionType.OR_32_1 },
                {"4c" , InstructionType.OR_32_2 },
                {"54" , InstructionType.AND_32_1},
                {"5c" , InstructionType.AND_32_2},
                {"64" , InstructionType.LSH_32_1},
                {"6c" , InstructionType.LSH_32_2},
                {"74" , InstructionType.RSH_32_1},
                {"7c" , InstructionType.RSH_32_2},
                {"84" , InstructionType.NEG_32 },
                {"94" , InstructionType.MOD_32_1},
                {"9c" , InstructionType.MOD_32_2},
                {"a4" , InstructionType.XOR_32_1},
                {"ac" , InstructionType.XOR_32_2},
                {"b4" , InstructionType.MOV_32_1},
                {"bc" , InstructionType.MOV_32_2},
                {"c4" , InstructionType.ARSH_32_1},
                {"cc" , InstructionType.ARSH_32_2},
                {"05" , InstructionType.JMP},
                {"15" , InstructionType.JMP_EQ_1},
                {"1d" , InstructionType.JMP_EQ_2},
                {"25" , InstructionType.JMP_GT_1},
                {"2d" , InstructionType.JMP_GT_2},
                {"35" , InstructionType.JMP_GE_1},
                {"3d" , InstructionType.JMP_GE_2},
                {"a5" , InstructionType.JMP_LT_1},
                {"ad" , InstructionType.JMP_LT_2},
                {"b5" , InstructionType.JMP_LE_1},
                {"bd" , InstructionType.JMP_LE_2},
                {"45" , InstructionType.JMP_SET_1},
                {"4d" , InstructionType.JMP_SET_2},
                {"55" , InstructionType.JMP_NE_1},
                {"5d" , InstructionType.JMP_NE_2},
                {"65" , InstructionType.JMP_SGT_1},
                {"6d" , InstructionType.JMP_SGT_2},
                {"75" , InstructionType.JMP_SGE_1},
                {"7d" , InstructionType.JMP_SGE_2},
                {"c5" , InstructionType.JMP_SLT_1},
                {"cd" , InstructionType.JMP_SLT_2},
                {"d5" , InstructionType.JMP_SLE_1},
                {"dd" , InstructionType.JMP_SLE_2},
                {"85" , InstructionType.CALL},
                {"95" , InstructionType.EXIT}
  };

  private static EBPFInstruction buildInstruction(string line)
   {
      InstructionType instruction = instructionsRev[line.Substring(0, 2)];
      string dstS = line.Substring(2, 1);
      string srcS = line.Substring(3, 1);
      string offsetS = line.Substring(4,4);
      string immediateS = line.Substring(8, 8);
      int dst = int.Parse(dstS, System.Globalization.NumberStyles.HexNumber);
      int src = int.Parse(srcS, System.Globalization.NumberStyles.HexNumber);
      int offset = int.Parse(offsetS, System.Globalization.NumberStyles.HexNumber);
      int immediate = int.Parse(immediateS, System.Globalization.NumberStyles.HexNumber);
      return new EBPFInstruction(instruction, dst, src, offset, immediate);
   }

  public static List<EBPFInstruction> parse(string path) {
    List<EBPFInstruction> instructions = new List<EBPFInstruction>();
    using (StreamReader sr = new StreamReader(path))
    {
      string line;
      while ((line = sr.ReadLine()) != null)
      {
        instructions.Add(buildInstruction(line));
      }
    }
    return instructions;
  }
}

class CFGBuilder {
  private static void processInstruction(Register[] registers, ProgramPoint[] points, CFG cfg, int i, EBPFInstruction instruction) {
    AssignmentEdge e = null;
    BranchEdge b1 = null;
    BranchEdge b2 = null;
    switch (instruction.instruction) {
      case InstructionType.ADD_2:
        e = new AssignmentEdge(points[i-1], points[i], registers[instruction.dst], new ArithmeticExpresion(registers[instruction.dst], registers[instruction.src], ArithmeticOperation.Add));
        break;
      case InstructionType.ADD_1:
        e = new AssignmentEdge(points[i-1], points[i], registers[instruction.dst], new ArithmeticExpresion(registers[instruction.dst], new Literal(instruction.immediate), ArithmeticOperation.Add));
        break;
      case InstructionType.SUB_2:
        e = new AssignmentEdge(points[i-1], points[i], registers[instruction.dst], new ArithmeticExpresion(registers[instruction.dst], registers[instruction.src], ArithmeticOperation.Subtract));
        break;
      case InstructionType.SUB_1:
        e = new AssignmentEdge(points[i-1], points[i], registers[instruction.dst], new ArithmeticExpresion(registers[instruction.dst], new Literal(instruction.immediate), ArithmeticOperation.Subtract));
        break;
      case InstructionType.MOV_2:
        e = new AssignmentEdge(points[i-1], points[i], registers[instruction.dst], new ArithmeticExpresion(new Literal(0), registers[instruction.src], ArithmeticOperation.Add));
        break;
      case InstructionType.MOV_1:
        e = new AssignmentEdge(points[i-1], points[i], registers[instruction.dst], new ArithmeticExpresion(new Literal(0), new Literal(instruction.immediate), ArithmeticOperation.Add));
        break;
      case InstructionType.NEG:
        e = new AssignmentEdge(points[i-1], points[i], registers[instruction.dst], new ArithmeticExpresion(new Literal(0), registers[instruction.dst], ArithmeticOperation.Subtract));
        break;
      case InstructionType.JMP:
        e = new AssignmentEdge(points[i-1], points[instruction.offset], registers[0], new ArithmeticExpresion(new Literal(0), registers[0], ArithmeticOperation.Add));
        break;
      case InstructionType.JMP_GE_1:
        b1 = new BranchEdge(points[i-1], points[instruction.offset], new Condition(registers[instruction.dst], ">=", new Literal(instruction.immediate)));
        b2 = new BranchEdge(points[i-1], points[i], new Condition(registers[instruction.dst], "<", new Literal(instruction.immediate)));
        break;
      case InstructionType.JMP_GT_1:
        b1 = new BranchEdge(points[i-1], points[instruction.offset], new Condition(registers[instruction.dst], ">", new Literal(instruction.immediate)));
        b2 = new BranchEdge(points[i-1], points[i], new Condition(registers[instruction.dst], "<=", new Literal(instruction.immediate)));
        break;
      case InstructionType.JMP_LE_1:
        b1 = new BranchEdge(points[i-1], points[instruction.offset], new Condition(registers[instruction.dst], "<=", new Literal(instruction.immediate)));
        b2 = new BranchEdge(points[i-1], points[i], new Condition(registers[instruction.dst], ">", new Literal(instruction.immediate)));
        break;
      case InstructionType.JMP_LT_1:
        b1 = new BranchEdge(points[i-1], points[instruction.offset], new Condition(registers[instruction.dst], "<", new Literal(instruction.immediate)));
        b2 = new BranchEdge(points[i-1], points[i], new Condition(registers[instruction.dst], ">=", new Literal(instruction.immediate)));
        break;
      default: 
        throw new Exception("Not implemented");
    }
    if (e != null) {
      cfg.AddEdge(e);
    }
    if (b1 != null && b2 != null) {
      cfg.AddEdge(b1);
      cfg.AddEdge(b2);
    }
  }

  public static CFG build(List<EBPFInstruction> instructions) {
    CFG cfg = new CFG();
    Register[] registers = Enumerable.Range(0, 10).ToList().Select(i => new Register("r"+i.ToString())).ToArray();
    ProgramPoint[] points = Enumerable.Range(0, instructions.Count+1).ToList().Select(i => new ProgramPoint(i.ToString())).ToArray();
    for (int i = 0; i < instructions.Count; i++) {
      cfg.AddNode(points[i]);
    }
    for (int i = 0; i < instructions.Count; i++) {
      processInstruction(registers, points, cfg, i+1, instructions[i]);
    }
    return cfg;
  }
}

class Programm
{
    public static void Main(string[] args)
    {
      List<EBPFInstruction> inst = ParserAsm.parse("./Parser/Examples/Example1A");
      inst.ForEach(i => Console.WriteLine(i.instruction + " " + i.dst + " " + i.src + " " + i.immediate + " " + i.offset));
      CFG cfg = CFGBuilder.build(inst);
      Console.WriteLine(cfg.ToString());
    }
}