using System.Collections.Generic;
using SharpAstrology.Enums;

namespace SharpAstrology.DataModels;

/// <summary>
/// Representing the house positions in a natal chart.
/// </summary>
public sealed class HousePosition
{
    /// <summary>
    /// Representing the cusps longitude of the houses.
    /// </summary>
    public required Dictionary<Houses, double> HouseCusps { get; init; }
        
    /// <summary>
    /// Representing the longitude of the parts of the cross (asc, mc, ...).
    /// </summary>
    public required Dictionary<Cross, double> Cross { get; init; }
}