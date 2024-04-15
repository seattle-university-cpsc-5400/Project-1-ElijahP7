using System;
using System.Runtime.InteropServices;
using Project1;

namespace Project1

{
	using static Project1.PToken;  // defined in Token.cs

	/// <summary>
	/// Recursive-descent parser based on the grammar given
	///   in Figure 2.1
	/// 
	/// </summary>
	public class Parser
	{

		private TokenStream ts;
        private const Boolean parseTrace = true;
        private PToken lastToken;
        private SymbolTable symbolTable;
        private float fvalue;


        public Parser(string f)
		{
			ts = new TokenStream(f);
            symbolTable = new SymbolTable();
		}


		public virtual void Prog()
		{
			if (ts.peek() == FLTDCL || ts.peek() == INTDCL || ts.peek() == ID || ts.peek() == PRINT || ts.peek() == EOF)
			{
                Things();
                Expect(EOF);
			}
			else
			{
				Error("expected floatdcl, intdcl, id, print, or eof");
			}
            if (parseTrace) Console.WriteLine("Recognized Prog");
        }

		public virtual void Things()
        /***** This is not a complete procedure for matching Things -- it only handles the production with an empty RHS.
         * Add the logic needed to match any other productions for Things.
         */
		{
            if (ts.peek() == FLTDCL || ts.peek() == INTDCL)
            {
                Dcl();
                Things();
            }
            else if (ts.peek() == ID || ts.peek() == PRINT)
            {
                Stmt();
                Things();
            }
            else if (ts.peek() == EOF)
            {
                // Do nothing for lambda-production
            }
            else
			{
				Error("expected floatdcl, intdcl, id, print, or eof");
			}
		}

        /***** functions to match all of the other rules of the grammar go here */
        public virtual void Dcl()
        {
            string temp;
            if (ts.peek() == FLTDCL)
            {
                Expect(FLTDCL);
                temp = TokenTypeToStringType(lastToken.type);
                Expect(ID);
                symbolTable.EnterSymbol(lastToken.val, temp);
            }
            else if (ts.peek() == INTDCL)
            {
                Expect(INTDCL);
                temp = TokenTypeToStringType(lastToken.type);
                Expect(ID);
                symbolTable.EnterSymbol(lastToken.val, temp);
            }
            else
            {
                Error("expected float or int declaration");
            }
        }

        public virtual void Stmt()
        {
            PToken temp;
            if (ts.peek() == ID)
            {
                Expect(ID);
                temp = lastToken;
                Expect(ASSIGN);
                Val();
                Expr();
                symbolTable.AssignSymbol(temp.val, fvalue);
            }
            else if (ts.peek() == PRINT)
            {
                Expect(PRINT);
                Expect(ID);
                Console.WriteLine(symbolTable.LookupValue(lastToken.val));
            }
            else
            {
                Error("expected id or print");
            }

        }

        public virtual void Expr()
        {
            float tempFloat = fvalue;
            if (ts.peek() == PLUS)
            {
                Expect(PLUS);
                Val();
                fvalue = tempFloat + fvalue;
                Expr();
            }
            else if (ts.peek() == MINUS)
            {
                Expect(MINUS);
                Val();
                fvalue = tempFloat - fvalue;
                Expr();
            }
            else if (ts.peek() == ID || ts.peek() == PRINT || ts.peek() == EOF)
            {
                // Do nothing for lambda-production
            }
            else
            {
                Error("expected plus, minus, id, print, or eof");
            }

        }

        public virtual void Val()
        {
            if (ts.peek() == ID)
            {
                Expect(ID);
                fvalue = float.Parse(symbolTable.LookupValue(lastToken.val));
            }
            else if (ts.peek() == INUM)
            {
                Expect(INUM);
                fvalue = float.Parse(lastToken.val);
            }
            else if (ts.peek() == FNUM)
            {
                Expect(FNUM);
                fvalue = float.Parse(lastToken.val);
            }
            else
            {
                Error("expected id, inum, or fnum");
            }

        }

        private void Expect(int type)
		{
			PToken t = ts.advance();
			if (t.type != type)
			{
				throw new Exception("Expected type " + token2str[type] + " but received type " + token2str[t.type]);

			}
            lastToken = t;
		}

		private void Error(string message)
		{
			throw new Exception(message);
		}

        private string TokenTypeToStringType(int type)
        {
            switch(type)
            {
                case 1:
                    return "float";
                case 2:
                    return "int";
                default:
                    return "";
            }
        }

	}

}