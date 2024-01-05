using System;
using System.Collections.Generic;
using System.Linq;
using SharpAstrology.DataModels;
using SharpAstrology.Enums;
using SharpAstrology.Interfaces;

namespace SharpAstrology.ExtensionMethods;

public static class IEphemeridesContextExtensionMethods
{
    /// <summary>
    /// Retrieves the positions of multiple planets at a specific point in time.
    /// </summary>
    /// <param name="planets">The collection of planets for which positions are requested.</param>
    /// <param name="pointInTime">The specific point in time for which positions are requested.</param>
    /// <returns>
    /// A dictionary where the key is the planet and the value is its corresponding position
    /// at the specified point in time.
    /// </returns>
    public static Dictionary<Planets, PlanetPosition> PlanetsPosition(this IEphemerides eph,
        IEnumerable<Planets> planets, DateTime pointInTime)
    {
        return planets.ToDictionary(o => o, o => eph.PlanetsPosition(o, pointInTime));
    }
}