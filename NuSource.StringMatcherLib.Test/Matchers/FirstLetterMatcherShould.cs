using System;
using System.Collections.Generic;
using FluentAssertions;
using NuSource.StringMatcherLib.Constants;
using NuSource.StringMatcherLib.Enums;
using NuSource.StringMatcherLib.Exceptions;
using NuSource.StringMatcherLib.Interface;
using NuSource.StringMatcherLib.Matchers;
using NuSource.StringMatcherLib.Model;
using Xunit;

namespace NuSource.StringMatcherLib.Test.Matchers;

public static class FirstLetterMatcherShould
{
    #region FirstLetterMatcher()

    [Fact]
    public static void CtorDefault_ShouldNotThrow()
    {
        Action act = () => new FirstLetterMatcher();
        act.Should().NotThrow();
    }
    
    #endregion

    #region Match(string? str1, string? str2)
    
    [Theory]
    [InlineData("Strong", "Stronk")]
    [InlineData("Strong", "String")]
    [InlineData("Rupert", "Rpuert")]
    public static void Match_ShouldMatch(string str1, string str2)
    {
        IMatcher matcher = new FirstLetterMatcher();
        MatchResult actual = matcher.Match(str1, str2);

        actual.HasWarnings.Should().BeFalse();
        actual.IsMatch.Should().BeTrue();
        actual.MatchTypeUsed.Should().Be(MatchType.FirstLetter);
    }

    
    [Theory]
    [InlineData(null, "Strink")]
    [InlineData("Strong", null)]
    [InlineData(null, null)]
    public static void Match_ShouldHandle_Nulls(string str1, string str2)
    {
        IMatcher matcher = new FirstLetterMatcher();
        MatchResult actual = matcher.Match(str1, str2);

        actual.HasWarnings.Should().BeTrue();
        actual.IsMatch.Should().BeFalse();
        actual.MatchTypeUsed.Should().Be(MatchType.FirstLetter);
    }
    
    [Theory]
    [InlineData("Strong", "Wrong")]
    [InlineData("Wing", "Bling")]
    [InlineData("Who", "You")]
    public static void Match_ShouldNotMatch(string str1, string str2)
    {
        IMatcher matcher = new FirstLetterMatcher();
        MatchResult actual = matcher.Match(str1, str2);

        actual.HasWarnings.Should().BeFalse();
        actual.IsMatch.Should().BeFalse();
        actual.MatchTypeUsed.Should().Be(MatchType.FirstLetter);
    }
    
    #endregion
}