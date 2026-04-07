using Calculator_machine.Models;
using Calculator_machine.Models;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_machine.Service
{
    public class Tokenizer
    {
        public List<Token> Tokenize(string inputExpression)
        {
            List<Token> tokens = new List<Token>();
            StringBuilder numberBuffer = new StringBuilder();

            for (int i = 0; i < inputExpression.Length; i++)
            {
                char currentChar = inputExpression[i];

                // 숫자이거나 소수점이면 버퍼에 보관
                if (char.IsDigit(currentChar) || currentChar == '.')
                {
                    numberBuffer.Append(currentChar);
                }
                else
                {
                    // 기호를 만났는데 버퍼에 숫자가 있다면 먼저 토큰화
                    if (numberBuffer.Length > 0)
                    {
                        tokens.Add(new Token(TokenType.Number, numberBuffer.ToString(), i - numberBuffer.Length));
                        numberBuffer.Clear();
                    }

                    // 공백이 아닌 기호 토큰화
                    if (!char.IsWhiteSpace(currentChar))
                    {
                        TokenType type = TokenType.Operator;
                        if (currentChar == '(') type = TokenType.OpenBracket;
                        else if (currentChar == ')') type = TokenType.CloseBracket;

                        tokens.Add(new Token(type, currentChar.ToString(), i));
                    }
                }
            }

            // 마지막에 남은 숫자 찌꺼기 처리
            if (numberBuffer.Length > 0)
            {
                tokens.Add(new Token(TokenType.Number, numberBuffer.ToString(), inputExpression.Length - numberBuffer.Length));
            }

            return tokens;
        }
    }
}
