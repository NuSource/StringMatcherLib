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

public static class CharacterTranspositionMatcherShould
{
    #region CharacterTranspositionMatcher()

    [Fact]
    public static void CtorDefault_ShouldNotThrow()
    {
        Action act = () => new CharacterTranspositionMatcher();
        act.Should().NotThrow();
    }
    
    #endregion
    
    #region CharacterTranspositionMatcher(Dictionary<string, string>? matchOptions)

    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    public static void Ctor_ShouldNotThrow_ForValidOptions(int chars)
    {
        Action act = () => new CharacterTranspositionMatcher(new ()
        {
            { MatchOptionsKeys.NumberOfTranspositions, chars.ToString() }
        });
        
        act.Should().NotThrow();
    }
    
    [Fact]
    public static void Ctor_ShouldThrow_ForNullMatchOptions()
    {
        Action act = () => new CharacterTranspositionMatcher(null);
        act.Should().Throw<InvalidMatcherOptionsException>();
    }
    
    [Fact]
    public static void Ctor_ShouldThrow_ForKeyMissing()
    {
        Action act = () => new CharacterTranspositionMatcher(new());
        act.Should().Throw<InvalidMatcherOptionsException>();
    }
    
    [Fact]
    public static void Ctor_ShouldThrow_ForNullNumberOfTranspositions()
    {
        Action act = () => new CharacterTranspositionMatcher(new ()
        {
            { MatchOptionsKeys.NumberOfTranspositions, null! }
        });
        act.Should().Throw<InvalidMatcherOptionsException>();
    }
    
    [Fact]
    public static void Ctor_ShouldThrow_ForInvalidNumberOfTranspositions()
    {
        Action act = () => new CharacterTranspositionMatcher(new ()
        {
            { MatchOptionsKeys.NumberOfTranspositions, "Unit McTester" }
        });
        act.Should().Throw<InvalidMatcherOptionsException>();
    }
    
    #endregion
    
    #region Match(string? str1, string? str2)
    
    [Theory]
    [InlineData("Strong", "Stronk")]
    [InlineData("Strong", "String")]
    [InlineData("Rupert", "Rpuert")]
    public static void Match_ShouldMatch_1Transposed(string str1, string str2)
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
    public static void Match_ShouldMatch_3Transposed(string str1, string str2)
    {
        Dictionary<string, string> matchOptions = new()
        {
            { MatchOptionsKeys.NumberOfTranspositions, "3" }
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
    public static void Match_ShouldHandle_Nulls(string str1, string str2)
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
    public static void Match_ShouldNotMatch_1Transposed(string str1, string str2)
    {
        IMatcher matcher = new CharacterTranspositionMatcher();
        MatchResult actual = matcher.Match(str1, str2);

        actual.HasWarnings.Should().BeFalse();
        actual.IsMatch.Should().BeFalse();
        actual.MatchTypeUsed.Should().Be(MatchType.CharacterTransposition);
    }
    
    #endregion
    
}