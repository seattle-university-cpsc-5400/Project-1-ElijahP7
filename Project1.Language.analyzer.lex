%namespace Project1
%scannertype Scanner
%visibility internal
%tokentype Token
%using Project1;

%option stack, minimize, parser, verbose, persistbuffer, noembedbuffers, noparser

LineTerminator	(\r\n?|\n)
WhiteSpace	({LineTerminator}|[ \t\f])

/* The following constants should be used to tell the parser what tokens have been recogized by the scanner.
   The rule associated with each token should simply be  { return(token-mumber) }
   where token-number is provided by using one of the constants.
 */
%{
	public const int EOF = 0, FLTDCL = 1, INTDCL = 2, PRINT = 3, ASSIGN = 4, PLUS = 5, MINUS = 6, ID = 7, INUM = 8, FNUM = 9;
%}

%%
 
/*****  Add your rules for recognizing ac tokens here.  I have given you one for skipping whitespace.  */
Whitespace+		{ /* delete blanks */ }
f	{ return(FLTDCL); }
i	{ return(INTDCL); }
p	{ return(PRINT); }
[a-eghj-oq-z]	{ return(ID); }
([0-9]+)	{ return(INUM) }
([0-9]+"."[0-9]+)	{ return(FNUM); }
"="		{ return(ASSIGN); }
"+"		{ return(PLUS); }
"-"		{ return(MINUS); }


%%