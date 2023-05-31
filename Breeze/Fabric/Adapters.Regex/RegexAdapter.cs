using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Dywham.Breeze.Fabric.Adapters.Regex
{
    public class RegexAdapter : IRegexAdapter
    {
        public IEnumerable<string> MatchesAsStrings(string input, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern)
        {
            return System.Text.RegularExpressions.Regex.Matches(input, pattern)
                .Select(x => x.ToString());
        }
    }
}