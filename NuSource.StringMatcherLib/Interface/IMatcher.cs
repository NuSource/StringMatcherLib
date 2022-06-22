using System.Collections.Generic;
using NuSource.StringMatcherLib.Enums;
using NuSource.StringMatcherLib.Model;

namespace NuSource.StringMatcherLib.Interface;

public interface IMatcher
{
    public MatchType Type { get; }

    public MatchResult Match(string? str1, string? str2);
}