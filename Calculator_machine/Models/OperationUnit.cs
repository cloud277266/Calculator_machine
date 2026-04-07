using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_machine.Models
{
    public class OperationUnit
    {
        public double LeftNumber { get; set; }
        public double RightNumber { get; set; }
        public string OperatorSymbol { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }

        public OperationUnit(double leftNumber, double rightNumber, string operatorSymbol, int startIndex, int endIndex)
        {
            LeftNumber = leftNumber;
            RightNumber = rightNumber;
            OperatorSymbol = operatorSymbol;
            StartIndex = startIndex;
            EndIndex = endIndex;
        }
    }
}
