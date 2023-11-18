using Newtonsoft.Json.Linq;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFXCodeCompletion.Parser
{
    public class SfxToken : IPositionAware<SfxToken>
    {
        public SfxToken(SfxTokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public int Length { get; private set; }
        public int Start { get; private set; }
        public SfxTokenType Type { get; private set; }
        public string Value { get; private set; }

        public SfxToken SetPos(Position startPos, int length)
        {
            Start = startPos.Pos;
            Length = length;
            return this;
        }
    }
}
