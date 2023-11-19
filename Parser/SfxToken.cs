using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFXCodeCompletion.Parser
{
    public class SfxToken
    {
        public SfxToken(SfxTokenType type, int start, int length)
        {
            Type = type;
            Start = start;
            Length = length;
        }

        public int Length { get; private set; }
        public int Start { get; private set; }
        public SfxTokenType Type { get; private set; }
    }
}
