using System;
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


        public Parser(string f)
		{
			ts = new TokenStream(f);
		}


		public virtual void Prog()
		{
			if (ts.peek() == FLTDCL || ts.peek() == INTDCL || ts.peek() == ID || ts.peek() == PRINT || ts.peek() == EOF)
			{
				Things();
				expect(EOF);
			}
			else
			{
				error("expected floatdcl, intdcl, id, print, or eof");
			}
            if (parseTrace) Console.WriteLine("Recognized Prog");
        }

		public virtual void Things()
        /***** This is not a complete procedure for matching Things -- it only handles the production with an empty RHS.
         * Add the logic needed to match any other productions for Things.
         */

		{
			if (ts.peek() == EOF)
            {
                // Do nothing for lambda-production
            }
            else
			{
				error("expected floatdcl, intdcl, id, print, or eof");
			}
		}

		/***** functions to match all of the other rules of the grammar go here */

		private void expect(int type)
		{
			PToken t = ts.advance();
			if (t.type != type)
			{
				throw new Exception("Expected type " + token2str[type] + " but received type " + token2str[t.type]);

			}
		}

		private void error(string message)
		{
			throw new Exception(message);
		}

	}

}