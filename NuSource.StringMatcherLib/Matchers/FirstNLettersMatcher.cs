using System;
using System.Collections.Generic;
using NuSource.StringMatcherLib.Constants;
using NuSource.StringMatcherLib.Enums;
using NuSource.StringMatcherLib.Exceptions;
using NuSource.StringMatcherLib.Interface;
using NuSource.StringMatcherLib.Model;

namespace NuSource.StringMatcherLib.Matchers;

public class FirstNLettersMatcher : IMatcher
{
    public MatchType Type => MatchType.FirstNLetters;

    private readonly int _numberOfLetters;
    
    /// <summary>
    ///   Defaults the number of characters checked to 1.
    /// </summary>
    public FirstNLettersMatcher()
    {
        _numberOfLetters = 1;
    }

    /// <summary>
    ///   Allows you to set the number of characters that will be checked.
    /// </summary>
    /// <param name="matchOptions">Set key [MatchOptionsKeys.NumberOfCharacters] with the number of characters to check.</param>
    public FirstNLettersMatcher(Dictionary<string, string>? matchOptions)
    {
        if (matchOptions == null || !matchOptions.ContainsKey(MatchOptionsKeys.NumberOfCharacters))
        {
            throw new InvalidMatcherOptionsException($"Missing key '{MatchOptionsKeys.NumberOfCharacters}' in {nameof(matchOptions)}");
        }

        bool success = int.TryParse(matchOptions[MatchOptionsKeys.NumberOfCharacters], out _numberOfLetters);

        if (!success)
        {
            throw new InvalidMatcherOptionsException($"Invalid value given for '{MatchOptionsKeys.NumberOfCharacters}' in {nameof(matchOptions)}");
        }
    }
    
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
        
        char[] str1chars = str1.ToCharArray();
        char[] str2chars = str2.ToCharArray();

        for (int i = 0; i < _numberOfLetters; i++)
        {
            if (i >= str1chars.Length || i >= str2chars.Length)
            {
                return new MatchResult()
                {
                    MatchTypeUsed = this.Type,
                    IsMatch = false
                };
            }

            if (str1chars[i] != str2chars[i])
            {
                return new MatchResult()
                {
                    MatchTypeUsed = this.Type,
                    IsMatch = false
                };
            }
        }
        
        // Otherwise return true, assume good if we get this far.
        return new MatchResult()
        {
            MatchTypeUsed = Type,
            IsMatch = true
        };
    }
}