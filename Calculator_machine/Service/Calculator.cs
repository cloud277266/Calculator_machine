using Calculating_machine.Models;
using Calculator_machine.Models;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_machine.Service
{
    public class Calculator
    {
        public double Evaluate(List<Token> workingExpression)
        {
            if (workingExpression == null || workingExpression.Count == 0)
                throw new ArgumentException("계산할 수식이 없습니다.");

            int currentIndex = 0;
            return ParseRecursive(workingExpression, ref currentIndex);
        }

        private double ParseRecursive(List<Token> tokens, ref int currentIndex)
        {
            List<Token> localExpression = new List<Token>();

            while (currentIndex < tokens.Count)
            {
                Token currentToken = tokens[currentIndex];

                if (currentToken.Type == TokenType.Number || currentToken.Type == TokenType.Operator)
                {
                    localExpression.Add(currentToken);
                    currentIndex++;
                }
                else if (currentToken.Type == TokenType.OpenBracket)
                {
                    currentIndex++;
                    double innerResult = ParseRecursive(tokens, ref currentIndex);
                    localExpression.Add(new Token(TokenType.Number, innerResult.ToString()));
                }
                else if (currentToken.Type == TokenType.CloseBracket)
                {
                    currentIndex++;
                    break;
                }
            }
            return CalculateFlatExpression(localExpression);
        }

        private double CalculateFlatExpression(List<Token> flatExpression)
        {
            if (flatExpression.Count == 0) return 0;

            while (flatExpression.Count > 1)
            {
                int targetOpIndex = -1;
                int maxPriority = -1;

                for (int tokenIndex = 0; tokenIndex < flatExpression.Count; tokenIndex++)
                {
                    Token currentToken = flatExpression[tokenIndex];

                    if (currentToken.Type == TokenType.Operator)
                    {
                        int currentPriority = OperatorRules.PriorityMap[currentToken.Value];

                        if (currentPriority > maxPriority)
                        {
                            maxPriority = currentPriority;
                            targetOpIndex = tokenIndex;
                        }
                    }
                }

                if (targetOpIndex == -1) throw new InvalidOperationException("형식이 잘못되었습니다.");
                if (targetOpIndex == 0 || targetOpIndex == flatExpression.Count - 1)
                    throw new InvalidOperationException("수식의 양 끝에는 연산자가 올 수 없습니다.");

                int startIndex = targetOpIndex - 1;
                int endIndex = targetOpIndex + 1;

                OperationUnit currentOperation = new OperationUnit(
                     leftNumber: double.Parse(flatExpression[startIndex].Value),
                     rightNumber: double.Parse(flatExpression[endIndex].Value),
                     operatorSymbol: flatExpression[targetOpIndex].Value,
                     startIndex: startIndex,
                     endIndex: endIndex
                 );

                double operationResult = PerformOperation(currentOperation);

                int removeCount = currentOperation.EndIndex - currentOperation.StartIndex + 1;
                flatExpression.RemoveRange(currentOperation.StartIndex, removeCount);
                flatExpression.Insert(currentOperation.StartIndex, new Token(TokenType.Number, operationResult.ToString()));
            }

            return double.Parse(flatExpression[0].Value);
        }

        private double PerformOperation(OperationUnit unit)
        {
            switch (unit.OperatorSymbol)
            {
                case "+": return unit.LeftNumber + unit.RightNumber;
                case "-": return unit.LeftNumber - unit.RightNumber;
                case "*": return unit.LeftNumber * unit.RightNumber;
                case "/":
                    if (unit.RightNumber == 0) throw new DivideByZeroException("0으로 나눌 수 없습니다.");
                    return unit.LeftNumber / unit.RightNumber;
                default:
                    throw new InvalidOperationException($"지원하지 않는 연산자입니다: {unit.OperatorSymbol}");
            }
        }
    }
}
