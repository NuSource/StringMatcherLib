using System.Collections.Generic;
using FluentAssertions;
using NuSource.StringMatcherLib.Enums;
using NuSource.StringMatcherLib.Interface;
using NuSource.StringMatcherLib.Matchers;
using NuSource.StringMatcherLib.Model;
using Xunit;

namespace NuSource.StringMatcherLib.Test.Matchers;

public static class CharacterTranspositionMatcherShould
{
    #region Match(string? str1, string? str2)
    
    [Theory]
    [InlineData("Strong", "Stronk")]
    [InlineData("Strong", "String")]
    [InlineData("Rupert", "Rpuert")]
    public static void Match_ShouldMatch1Transposed(string str1, string str2)
    {
        IMatcher matcher = new CharacterTranspositionMatcher();
        MatchResult actual = matcher.Match(str1, str2);

        actual.HasWarnings.Should().BeFalse();
        actual.IsMatch.Should().BeTrue();
        actual.MatchTypeUsed.Should().Be(MatchType.CharacterTransposition);
    }
    
    [Theory]
    [InlineData("Strong", "Strink")]
    [InlineData("Strong", "Wrong")]
    [InlineData("Robert", "Rupert")]
    public static void Match_ShouldMatch3Transposed(string str1, string str2)
    {
        Dictionary<string, string> matchOptions = new()
        {
            {"NumberOfTranspositions", "3"}
        };
        
        IMatcher matcher = new CharacterTranspositionMatcher(matchOptions);
        MatchResult actual = matcher.Match(str1, str2);

        actual.HasWarnings.Should().BeFalse();
        actual.IsMatch.Should().BeTrue();
        actual.MatchTypeUsed.Should().Be(MatchType.CharacterTransposition);
    }
    
    [Theory]
    [InlineData(null, "Strink")]
    [InlineData("Strong", null)]
    [InlineData(null, null)]
    public static void Match_ShouldHandleNulls(string str1, string str2)
    {
        IMatcher matcher = new CharacterTranspositionMatcher();
        MatchResult actual = matcher.Match(str1, str2);

        actual.HasWarnings.Should().BeTrue();
        actual.IsMatch.Should().BeFalse();
        actual.MatchTypeUsed.Should().Be(MatchType.CharacterTransposition);
    }
    
    [Theory]
    [InlineData("Strong", "Strink")]
    [InlineData("Strong", "Strings")]
    [InlineData("Who", "Where")]
    public static void Match_ShouldNotMatch1Transposed(string str1, string str2)
    {
        IMatcher matcher = new CharacterTranspositionMatcher();
        MatchResult actual = matcher.Match(str1, str2);

        actual.HasWarnings.Should().BeFalse();
        actual.IsMatch.Should().BeFalse();
        actual.MatchTypeUsed.Should().Be(MatchType.CharacterTransposition);
    }
    
    #endregion
    
}