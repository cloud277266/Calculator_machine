using Calculator_machine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_machine.Models
{
    public class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }
        public int Position { get; set; }

        public Token(TokenType type, string value, int position = 0)
        {
            Type = type;
            Value = value;
            Position = position;
        }
    }
}
