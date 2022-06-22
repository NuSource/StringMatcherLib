using System.Collections.Generic;
using NuSource.StringMatcherLib.Enums;

namespace NuSource.StringMatcherLib.Model;

public class MatchResult
{
    // Match Results
    public bool IsMatch { get; internal set; }
    
    public MatchType MatchTypeUsed { get; internal set; }
    
    // Warnings
    public bool HasWarnings { get; internal set; }

    public List<string> Warnings { get; internal set; } = new();
}