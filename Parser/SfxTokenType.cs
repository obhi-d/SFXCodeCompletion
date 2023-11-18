using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFXCodeCompletion.Parser
{
    public enum SfxTokenType
    {
        Command,
        Section,
        Function,
        Identifier,
        Keyword,
        Number,
        Operator,
        Preprocessor,
        Variable,
        QuotedString,
        Comment
    }
}
