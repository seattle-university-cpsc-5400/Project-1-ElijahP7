using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class SymbolTable
    {
        private Dictionary<string, Tuple<string, object>> symbolTable;
        public SymbolTable() 
        {
            symbolTable = new Dictionary<string, Tuple<string, object>>();
        }

        public void EnterSymbol(string name, string type)
        {
            if (symbolTable.ContainsKey(name))
                throw new Exception($"Duplicate declaration of {name}");
            symbolTable[name] = new Tuple<string, object>(type, null);
        }

        public void AssignSymbol(string name, object value)
        {
            if (!symbolTable.ContainsKey(name))
                throw new Exception($"{name} is not declared");
            string type = symbolTable[name].Item1;
            if (!TypeChecking(type, value))
                throw new Exception($"Can't assign {type} to {value}");
            symbolTable[name] = new Tuple<string, object>(type, value);
        }

        public string LookupType(string name)
        {
            if (!symbolTable.ContainsKey(name))
                throw new Exception($"{name} is not declared");
            return symbolTable[name].Item1;
        }

        public string LookupValue(string name)
        {
            if (!symbolTable.ContainsKey(name))
                throw new Exception($"{name} is not declared");
            return symbolTable[name].Item2.ToString();
        }

        private bool TypeChecking(string type, object value)
        {
            if (type == "int")
                return value is int || (value is float && Math.Floor((float)value) == (float)value);
            else if (type == "float")
                return value is float;
            else
                return false;
        }
    }
}
