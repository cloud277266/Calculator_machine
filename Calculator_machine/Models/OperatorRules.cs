using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Calculator_machine.Models
{
    public static class OperatorRules
    {
        public static readonly Dictionary<string, int> PriorityMap = new Dictionary<string, int>
        {
            { "+", 1 },
            { "-", 1 },
            { "*", 2 },
            { "/", 2 }
        };
    }
}
