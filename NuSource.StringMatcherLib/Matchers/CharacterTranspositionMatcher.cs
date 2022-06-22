using System;
using System.Collections.Generic;
using NuSource.StringMatcherLib.Enums;
using NuSource.StringMatcherLib.Exceptions;
using NuSource.StringMatcherLib.Interface;
using NuSource.StringMatcherLib.Model;

namespace NuSource.StringMatcherLib.Matchers;

/// <summary>
///   Compares two strings, matching if they are the same or if a set number of characters are transposed.
/// </summary>
public class CharacterTranspositionMatcher : IMatcher
{
    public MatchType Type => MatchType.CharacterTransposition;

    private readonly int _numberOfTranspositions;

    /// <summary>
    ///   Defaults the number of transposed characters to 1.
    /// </summary>
    public CharacterTranspositionMatcher()
    {
        _numberOfTranspositions = 1;
    }

    /// <summary>
    ///   Allows you to set the number of characters that can be transposed.
    /// </summary>
    /// <param name="matchOptions">Set key "NumberOfTranspositions" with the number of transpositions to allow.</param>
    public CharacterTranspositionMatcher(Dictionary<string, string> matchOptions)
    {
        if (!matchOptions.ContainsKey("NumberOfTranspositions"))
        {
            throw new InvalidMatcherOptionsException($"Missing key 'NumberOfTranspositions' in {nameof(matchOptions)}");
        }

        bool success = int.TryParse(matchOptions["NumberOfTranspositions"], out _numberOfTranspositions);

        if (!success)
        {
            throw new InvalidMatcherOptionsException($"Invalid value given for 'NumberOfTranspositions' in {nameof(matchOptions)}");
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
                    "Input is null."
                }
            };
        }

        if (string.CompareOrdinal(str1, str2) == 0)
        {
            return new MatchResult()
            {
                MatchTypeUsed = Type,
                IsMatch = true
            };
        }

        // If the character counts are more different than we allow, just go ahead and return false.
        if (Math.Abs(str1.Length - str2.Length) > _numberOfTranspositions)
        {
            return new MatchResult()
            {
                MatchTypeUsed = Type,
                IsMatch = false
            };
        }
        
        // If the transposed characters are more than we allow, return false.
        if (CountTranspositions(str1, str2) > _numberOfTranspositions)
        {
            return new MatchResult()
            {
                MatchTypeUsed = Type,
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
    
    private int CountTranspositions(string str1, string str2)
    {
        char[] str1chars = str1.ToCharArray();
        char[] str2chars = str2.ToCharArray();

        int transpositions = 0;
        
        // Case 1: Two characters flipped, example "String" and "Stirng".
        // Case 2: One added character, example "String" and "Stroing".
        // Case 3: One characters wrong, example "String" and "Strong".
        // Either way, only count one transposition.
        for (int str1itr = 0, str2itr = 0; str1itr < str1chars.Length && str2itr < str2chars.Length; )
        {
            char str1char = str1chars[str1itr];
            char str2char = str2chars[str2itr];

            if (str1char == str2char)
            {
                str1itr++;
                str2itr++;
                continue;
            }

            // If there is a next character available we need to check it also.
            if (str1itr + 1 < str1chars.Length && str2itr + 1 < str2chars.Length)
            {
                // First check if the char is just swapped with the next one
                if (AreSwapped(str1char, str1chars[str1itr + 1], str2char, str2chars[str2itr + 1]))
                {
                    transpositions++;
                    
                    // Skip next char, since we just checked it.
                    str1itr += 2; 
                    str2itr += 2;
                    continue;
                }
                
                // Char added to str1
                if (IsAddedChar(str2char, str1chars[str1itr + 1]))
                {
                    transpositions++;
                    // Skip the next char in this string to get them back to the same place.
                    str1itr++;
                    continue;
                }
                
                // Char added to str2
                if (IsAddedChar(str1char, str2chars[str2itr + 1]))
                {
                    transpositions++;
                    // Skip the next char in this string to get them back to the same place.
                    str2itr++;
                    continue;
                }
            }
            
            // So it wasn't a swap, we count it as just a typo character then.
            transpositions++;
            str1itr++; 
            str2itr++;
        }


        // Add character count difference
        transpositions += Math.Abs(str1.Length - str2.Length);
        
        return transpositions;
    }

    private bool AreSwapped(char str1char1, char str1char2, char str2char1, char str2char2)
    {
        return 
            str1char1 == str2char2 
            && str1char2 == str2char1;
    }

    private bool IsAddedChar(char str1char1, char str2char2)
    {
        if (str1char1 == str2char2)
        {
            return true;
        }

        return false;
    }
    
}