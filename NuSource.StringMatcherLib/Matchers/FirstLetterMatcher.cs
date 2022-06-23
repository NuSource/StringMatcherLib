using System;
using System.Collections.Generic;
using NuSource.StringMatcherLib.Enums;
using NuSource.StringMatcherLib.Interface;
using NuSource.StringMatcherLib.Model;

namespace NuSource.StringMatcherLib.Matchers;

public class FirstLetterMatcher : IMatcher
{
    public MatchType Type => MatchType.FirstLetter;

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

        if (str1.Length < 1 
            || str2.Length < 1 
            || str1[0] != str2[0])
        {
            return new MatchResult()
            {
                MatchTypeUsed = this.Type,
                IsMatch = false
            };
        }
        
        // Otherwise return true, assume good if we get this far.
        return new MatchResult()
        {
            MatchTypeUsed = Type,
            IsMatch = true
        };
    }
}