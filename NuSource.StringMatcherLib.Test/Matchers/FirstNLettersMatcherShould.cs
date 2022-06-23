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

public static class FirstNLettersMatcherShould
{
    #region FirstNLettersMatcher()

    [Fact]
    public static void CtorDefault_ShouldNotThrow()
    {
        Action act = () => new FirstNLettersMatcher();
        act.Should().NotThrow();
    }
    
    #endregion
    
    #region FirstNLettersMatcher(Dictionary<string, string>? matchOptions)

    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    public static void Ctor_ShouldNotThrow_ForValidOptions(int chars)
    {
        Action act = () => new FirstNLettersMatcher(new ()
        {
            { MatchOptionsKeys.NumberOfCharacters, chars.ToString() }
        });
        
        act.Should().NotThrow();
    }
    
    [Fact]
    public static void Ctor_ShouldThrow_ForNullMatchOptions()
    {
        Action act = () => new FirstNLettersMatcher(null);
        act.Should().Throw<InvalidMatcherOptionsException>();
    }
    
    [Fact]
    public static void Ctor_ShouldThrow_ForKeyMissing()
    {
        Action act = () => new FirstNLettersMatcher(new());
        act.Should().Throw<InvalidMatcherOptionsException>();
    }
    
    [Fact]
    public static void Ctor_ShouldThrow_ForNullNumberOfCharacters()
    {
        Action act = () => new FirstNLettersMatcher(new ()
        {
            { MatchOptionsKeys.NumberOfCharacters, null! }
        });
        act.Should().Throw<InvalidMatcherOptionsException>();
    }
    
    [Fact]
    public static void Ctor_ShouldThrow_ForInvalidNumberOfCharacters()
    {
        Action act = () => new FirstNLettersMatcher(new ()
        {
            { MatchOptionsKeys.NumberOfCharacters, "Unit McTester" }
        });
        act.Should().Throw<InvalidMatcherOptionsException>();
    }
    
    #endregion
    
    #region Match(string? str1, string? str2)
    
    [Theory]
    [InlineData("Strong", "Stronk")]
    [InlineData("Strong", "String")]
    [InlineData("Rupert", "Rpuert")]
    public static void Match_ShouldMatch_1Chars(string str1, string str2)
    {
        IMatcher matcher = new FirstNLettersMatcher();
        MatchResult actual = matcher.Match(str1, str2);

        actual.HasWarnings.Should().BeFalse();
        actual.IsMatch.Should().BeTrue();
        actual.MatchTypeUsed.Should().Be(MatchType.FirstNLetters);
    }
    
    [Theory]
    [InlineData("Strong", "Strink")]
    [InlineData("Strong", "String")]
    [InlineData("Robert", "Roburt")]
    public static void Match_ShouldMatch_3Chars(string str1, string str2)
    {
        Dictionary<string, string> matchOptions = new()
        {
            { MatchOptionsKeys.NumberOfCharacters, "3" }
        };
        
        IMatcher matcher = new FirstNLettersMatcher(matchOptions);
        MatchResult actual = matcher.Match(str1, str2);

        actual.HasWarnings.Should().BeFalse();
        actual.IsMatch.Should().BeTrue();
        actual.MatchTypeUsed.Should().Be(MatchType.FirstNLetters);
    }
    
    [Theory]
    [InlineData(null, "Strink")]
    [InlineData("Strong", null)]
    [InlineData(null, null)]
    public static void Match_ShouldHandle_Nulls(string str1, string str2)
    {
        IMatcher matcher = new FirstNLettersMatcher();
        MatchResult actual = matcher.Match(str1, str2);

        actual.HasWarnings.Should().BeTrue();
        actual.IsMatch.Should().BeFalse();
        actual.MatchTypeUsed.Should().Be(MatchType.FirstNLetters);
    }
    
    [Theory]
    [InlineData("Strong", "Wrong")]
    [InlineData("Wing", "Bling")]
    [InlineData("Who", "You")]
    public static void Match_ShouldNotMatch_1Chars(string str1, string str2)
    {
        IMatcher matcher = new FirstNLettersMatcher();
        MatchResult actual = matcher.Match(str1, str2);

        actual.HasWarnings.Should().BeFalse();
        actual.IsMatch.Should().BeFalse();
        actual.MatchTypeUsed.Should().Be(MatchType.FirstNLetters);
    }
    
    #endregion
}