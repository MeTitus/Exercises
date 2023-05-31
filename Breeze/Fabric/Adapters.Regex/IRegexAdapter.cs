using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Dywham.Breeze.Fabric.Adapters.Regex
{
    public interface IRegexAdapter : IAdapter
    {
        IEnumerable<string> MatchesAsStrings(string input, [StringSyntax(StringSyntaxAttribute.Regex)] string pattern);
    }
}