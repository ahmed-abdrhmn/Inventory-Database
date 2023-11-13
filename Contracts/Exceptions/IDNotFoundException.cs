using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.Exceptions
{
    public class IDNotFoundException : Exception
    {
        public IDNotFoundException() { }
        public IDNotFoundException(string table) : base($"Supplied Id for {table} cannot be found") { }
        public IDNotFoundException(string table, System.Exception inner) : base($"Supplied Id for {table} cannot be found", inner) { }
    }
}