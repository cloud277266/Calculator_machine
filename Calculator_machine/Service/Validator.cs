using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Calculator_machine.Service
{
    public class Validator
    {
        public bool IsValidInput(string currentExpression, string newInput)
        {
            if (string.IsNullOrEmpty(currentExpression)) return true;

            char lastChar = currentExpression.Last();

            // 연산자 연속 입력 방지 로직 (+, -, *, /)
            string operators = "+-*/";
            if (operators.Contains(lastChar) && operators.Contains(newInput))
            {
                return false;
            }

            return true; // 문제없으면 통과
        }
    }
}
