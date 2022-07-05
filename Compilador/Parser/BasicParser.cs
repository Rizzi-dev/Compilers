using VerySimpleInterpreter.Lexer;

namespace VerySimpleInterpreter.Parser
{
    public class BasicParser
    {
        private BasicLexer _lexer;
        private Token _lookAhead;
        public BasicParser(BasicLexer lexer)
        {
            _lexer = lexer;
        }

        public void Match(ETokenType type)
        {
            if (_lookAhead.Type == type)
                _lookAhead = _lexer.GetNextToken();
            else
                Error();
        }

        public void Error()
        {
            Console.WriteLine("Lascou...");
        }

        public void Input() //INPUT VAR
        {
            Match(ETokenType.INPUT);
            Match(ETokenType.VAR);
        }
        public void Output() //OUTPUT VAR
        {
            Match(ETokenType.OUTPUT);
            Match(ETokenType.VAR);
        }
        public void Atrib() //VAR AT expr
        {
            Match(ETokenType.VAR);
            Match(ETokenType.AT);
            Expr();
        }

        public void Fact() //NUM | VAR | OE expr CE
        {
            if (_lookAhead.Type == ETokenType.NUM){
                Match(ETokenType.NUM);
            }
            else if (_lookAhead.Type == ETokenType.VAR){
                Match(ETokenType.VAR);
            }
            else if (_lookAhead.Type == ETokenType.OE){
                Match(ETokenType.OE);
                Expr();
                Match(ETokenType.CE);
            }
            else {
                Error();
            }
        }

        public void Term() //factZ
        {
            Fact();
            Z();
        }

        public void Z() //vazio | * term | / term
        {
            // Log("R " + _lookAhead.Type);
            if (_lookAhead.Type == ETokenType.MULT)  //FIRST
            {
                Match(ETokenType.MULT);
                Term();
                // return t + Expr;
            } 
            else if (_lookAhead.Type == ETokenType.DIV) //FIRST
            {
                Match(ETokenType.DIV);
                Term();
                //return t - res;
            } 
            else if ((_lookAhead.Type != ETokenType.EOF))//FOLLOW
            {
                Error();
               //Error("Símbolo inesperado em R");
            }
            //return t;
        }

        public void Expr() //termY
        {
            Term();
            Y();
        }

        public void Y() //vazio | + expr | - expr
        {
            // Log("R " + _lookAhead.Type);
            if (_lookAhead.Type == ETokenType.SUM)  //FIRST
            {
                Match(ETokenType.SUM);
                Expr();
                // return t + Expr;
            } 
            else if (_lookAhead.Type == ETokenType.SUB) //FIRST
            {
                Match(ETokenType.SUB);
                Expr();
                //return t - res;
            } 
            else if ((_lookAhead.Type != ETokenType.EOF))//FOLLOW
            {
                Error();
               //Error("Símbolo inesperado em R");
            }
            // return t;
        }



        public void Prog() // prog   : lineX
        {
            Line();
            X();
        }

        public void X() //X : EOF | prog
        {
            if (_lookAhead.Type == ETokenType.EOF)
                Match(ETokenType.EOF);
            else
                Prog();
        }

        public void Line() // line   : stmt EOL
        {
            Stmt();
            Match(ETokenType.EOL);
        }
    
        public void Stmt() //stmt   : in | out | atrib  
        {
            if (_lookAhead.Type == ETokenType.INPUT)
                Input();
            else if (_lookAhead.Type == ETokenType.OUTPUT)
                Output();
            else if (_lookAhead.Type == ETokenType.VAR)
                Atrib();
            else
                Error();
        }


    }
}