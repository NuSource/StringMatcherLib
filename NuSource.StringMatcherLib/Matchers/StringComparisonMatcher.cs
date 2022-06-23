using System;
using System.Collections.Generic;
using NuSource.StringMatcherLib.Enums;
using NuSource.StringMatcherLib.Interface;
using NuSource.StringMatcherLib.Model;

namespace NuSource.StringMatcherLib.Matchers;

public class StringComparisonMatcher : IMatcher
{
    public MatchType Type => MatchType.StringComparison;
    
    public MatchResult Match(string? str1, string? str2)
    {
        if (str1 == null || str2 == null)
        {
            return new MatchResult()
            {
                MatchTypeUsed = this.Type,
                IsMatch = false,
                HasWarnings = true,
                Warnings = new()
                {
                    WarningFlags.NullInput
                }
            };
        }
        
        return new MatchResult()
        {
            MatchTypeUsed = Type,
            IsMatch = (string.CompareOrdinal(str1, str2) == 0)
        };
    }
}