﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFXCodeCompletion.Parser
{
    public static partial class KnownTokens
    {
        private static readonly Dictionary<string, SfxTokenType> knownTokens = GetDictionary();
        public static IEnumerable<KeyValuePair<string, SfxTokenType>> ReservedWords => knownTokens;

        public static SfxTokenType GetKnownTokenType(string word)
        {
            if (knownTokens.TryGetValue(word, out var type)) return type;
            return SfxTokenType.Identifier;
        }

        public static bool IsIdentifierChar(char c) => char.IsDigit(c) || IsIdentifierStartChar(c);

        public static bool IsIdentifierStartChar(char c) => char.IsLetter(c) || '_' == c || '@' == c;

        private static void AddRange(this Dictionary<string, SfxTokenType> result, IEnumerable<string> words, SfxTokenType type)
        {
            foreach (var word in words)
            {
                result[word] = type;
            }
        }
    }
}
