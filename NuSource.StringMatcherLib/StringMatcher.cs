using System;
using System.Collections.Generic;
using NuSource.StringMatcherLib.Enums;
using NuSource.StringMatcherLib.Exceptions;
using NuSource.StringMatcherLib.Interface;
using NuSource.StringMatcherLib.Matchers;
using NuSource.StringMatcherLib.Model;

namespace NuSource.StringMatcherLib;

public static class StringMatcher
{
    public static MatchResult Match(string str1, string str2, MatchType matchType = MatchType.StringComparison, Dictionary<string, string>? matchOptions = null)
    {
        bool hasMatchOptions = matchOptions == null;

        IMatcher matcher = matchType switch
        {
            MatchType.CharacterTransposition =>
                hasMatchOptions
                    ? new CharacterTranspositionMatcher()
                    : new CharacterTranspositionMatcher(matchOptions),
            MatchType.FirstLetter => new FirstLetterMatcher(),
            MatchType.FirstNLetters => 
                hasMatchOptions
                    ? new FirstNLettersMatcher()
                    : new FirstNLettersMatcher(matchOptions),
            MatchType.PercentSimilar => new PercentSimilarMatcher(),
                //hasMatchOptions
                    //? new PercentSimilarMatcher()
                    //: new PercentSimilarMatcher(matchOptions),
            MatchType.RegEx => new RegExMatcher(),
            MatchType.Soundex => new SoundexMatcher(),
            MatchType.StringComparison => new StringComparisonMatcher(),
            MatchType.Substring => new SubstringMatcher(),
                //hasMatchOptions
                    //? new SubstringMatcher()
                    //: new SubstringMatcher(matchOptions),
            _ => throw new InvalidMatchTypeException($"Invalid match type '{matchType}' specified.")
        };

        return matcher.Match(str1, str2);
    }
}