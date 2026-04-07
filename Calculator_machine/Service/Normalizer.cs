using Calculating_machine.Models;
using Calculator_machine.Models;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_machine.Service
{
    public class Normalizer
    {
        public List<Token> Normalize(List<Token> originalTokens)
        {
            if (originalTokens == null || originalTokens.Count == 0) return new List<Token>();
            List<Token> normalizedTokens = new List<Token>();

            for (int tokenIndex = 0; tokenIndex < originalTokens.Count; tokenIndex++)
            {
                Token currentToken = originalTokens[tokenIndex];

                // 단항 음수 처리
                if (currentToken.Type == TokenType.Operator && currentToken.Value == "-")
                {
                    bool isUnarMinus = false;

                    if (tokenIndex == 0)
                    {
                        isUnarMinus = true;
                    }
                    else
                    {
                        Token previousToken = originalTokens[tokenIndex - 1];
                        if (previousToken.Type == TokenType.OpenBracket) isUnarMinus = true;
                    }

                    if (isUnarMinus)
                    {
                        if (tokenIndex + 1 < originalTokens.Count)
                        {
                            Token nextToken = originalTokens[tokenIndex + 1];
                            if (nextToken.Type == TokenType.Number)
                            {
                                string negativeValue = "-" + nextToken.Value;
                                normalizedTokens.Add(new Token(TokenType.Number, negativeValue, currentToken.Position));

                                tokenIndex++;
                                continue;
                            }
                        }
                    }
                }
                normalizedTokens.Add(currentToken);

                // 암묵적 곱셈 패턴 처리
                if (tokenIndex + 1 < originalTokens.Count)
                {
                    Token nextToken = originalTokens[tokenIndex + 1];
                    string implicitMultPattern = $"{currentToken.Type}_{nextToken.Type}";
                    if (implicitMultPattern == "Number_OpenBracket" ||
                        implicitMultPattern == "CloseBracket_OpenBracket" ||
                        implicitMultPattern == "CloseBracket_Number")
                    {
                        normalizedTokens.Add(new Token(TokenType.Operator, "*", currentToken.Position));
                    }
                }
            }
            return normalizedTokens;
        }
    }
}
