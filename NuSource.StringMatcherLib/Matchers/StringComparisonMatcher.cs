using System;
using System.Collections.Generic;
using NuSource.StringMatcherLib.Enums;
using NuSource.StringMatcherLib.Interface;
using NuSource.StringMatcherLib.Model;

namespace NuSource.StringMatcherLib.Matchers;

public class StringComparisonMatcher : IMatcher
{
    public MatchType Type { get; }
    
    public MatchResult Match(string? str1, string? str2)
    {
        throw new NotImplementedException();
    }
}