using System;
using System.Collections.Generic;
using System.Linq;
using SharpAstrology.DataModels;
using SharpAstrology.Enums;

namespace SharpAstrology.Utility;

public static partial class AstrologyUtility
{
    /// <summary>
    /// Converts a Julian date to a DateTime object.
    /// </summary>
    /// <param name="julianDate">The Julian date to convert.</param>
    /// <returns>A DateTime object representing the converted Julian date.</returns>
    public static DateTime DateTimeFromJulianDate(double julianDate)
    {
        return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds((julianDate - 2440587.5) * 86400000);
    }
        
    /// <summary>
    /// Calculates the angles between the longitudes of planets.
    /// </summary>
    /// <param name="self">Planet positions for the first set of planets.</param>
    /// <param name="other">Planet positions for the second set of planets.</param>
    /// <returns>
    /// A nested dictionary containing the angles (in degrees) between the longitudes of planets.
    /// The outer dictionary key represents the planet from the first set,
    /// and the inner dictionary key represents the planet from the second set.
    /// The corresponding values are the angles between their longitudes.
    /// </returns>
    public static Dictionary<Enum, Dictionary<Enum, double>> AnglesBetween(
        Dictionary<Enum, double> self,
        Dictionary<Enum, double> other) 
    {
        return self.SelectMany(_ => other,
                (p1, p2)
                    => (p1.Key, p2.Key, AngleDifference(p1.Value, p2.Value)))
            .GroupBy(x => x.Item1)
            .ToDictionary(
                x => x.Key,
                x => x.ToDictionary(y => y.Item2, y => y.Item3));
    }
        
    /// <summary>
    /// Determines the astrological house of a celestial body based on its longitude
    /// and a dictionary of house positions.
    /// </summary>
    /// <param name="planetsLongitude">The longitude of the celestial body.</param>
    /// <param name="housePositions">
    /// Dictionary containing the positions of the houses cusps, where the key represents the house,
    /// and the value represents its longitude cusp.
    /// </param>
    /// <returns>The house in which the celestial body resides.</returns>
    public static Houses HouseOf(double planetsLongitude, Dictionary<Houses, double> housePositions)
    {
        var lowerHouseCusps = housePositions
            .Where(x => x.Value <= planetsLongitude)
            .ToDictionary(p=>p.Key, p=>p.Value);
        return lowerHouseCusps.Any() 
            ? lowerHouseCusps.MaxBy(x => x.Value).Key 
            : housePositions.MaxBy(x => x.Value).Key;
    }
        
    /// <summary>
    /// Subtracts one degree value from another, considering the circular nature of degrees.
    /// </summary>
    /// <param name="deg1">The first degree value.</param>
    /// <param name="deg2">The second degree value to subtract.</param>
    /// <returns>The result of subtracting deg2 from deg1, considering the circular nature of degrees.</returns>
    public static double SubtractDegree(double deg1, double deg2) => deg1 - deg2 < 0 ? deg1 + 360 - deg2 : deg1 - deg2;
        
    /// <summary>
    /// Adds two degree values, considering the circular nature of degrees.
    /// </summary>
    /// <param name="deg1">The first degree value.</param>
    /// <param name="deg2">The second degree value to add.</param>
    /// <returns>The result of adding deg1 and deg2, considering the circular nature of degrees.</returns>
    public static double AddDegree(double deg1, double deg2) => deg1 + deg2 >= 360 ? deg1 - 360 + deg2 : deg1 + deg2;
        
    /// <summary>
    /// Calculates the angular difference between two degrees, considering the circular nature of degrees.
    /// </summary>
    /// <param name="p1">The first degree value.</param>
    /// <param name="p2">The second degree value.</param>
    /// <returns>
    /// The angular difference between p1 and p2, considering the circular nature of degrees.
    /// The result is in the range [-180, 180].
    /// </returns>
    public static double AngleDifference(double p1, double p2)
    {
        var num = _normalizeDegree(p1 - p2);
        return num >= 180.0 ? num - 360.0 : num;
    }
        
    /// <summary>
    /// Calculates the house position of the planet based on the given planets longitude and the house cusp positions.
    /// </summary>
    /// <param name="planetsLongitude">The longitude of the planets.</param>
    /// <param name="housePositions">The dictionary containing the longitudes of the house cusps.</param>
    /// <returns>
    /// The house, where the celestial object is situated in.
    /// </returns>
    public static Houses PlanetsHousePosition(double planetsLongitude, Dictionary<Houses, double> housePositions)
    {
        var lowerHouseCusps = housePositions
            .Where(x => x.Value < planetsLongitude)
            .ToDictionary(p=>p.Key, p=>p.Value);
        return lowerHouseCusps.Any() 
            ? lowerHouseCusps.MaxBy(x => x.Value).Key 
            : housePositions.MaxBy(x => x.Value).Key;
    }
    
    /// <summary>
    /// Determines the zodiac sign based on a given longitude.
    /// </summary>
    /// <param name="longitude">The celestial objects longitude value.</param>
    /// <returns>The zodiac sign corresponding to the given longitude.</returns>
    public static Zodiac ZodiacSignOf(double longitude) => (Zodiac)(longitude / 30);
    
    /// <summary>
    /// Determines the house positions for each planet based on their longitudes and house cusp positions.
    /// </summary>
    /// <param name="planetPosition">Dictionary containing planet positions.</param>
    /// <param name="housePositions">Dictionary containing house cusp positions.</param>
    /// <returns>
    /// Dictionary where the keys represent planets, and the values represent the house positions
    /// for the corresponding planets based on their longitudes.
    /// </returns>
    public static Dictionary<Planets, Houses> PlanetsHousePosition(Dictionary<Planets, PlanetPosition> planetPosition, Dictionary<Houses, double> housePositions)
    {
        return planetPosition.ToDictionary(p => p.Key,
            p => PlanetsHousePosition(p.Value.Longitude, housePositions));
    }
            
    private static double _normalizeDegree(double x)
    {
        var num = x % 360.0;
        if (Math.Abs(num) < 1E-13)
            num = 0.0;
        if (num < 0.0)
            num += 360.0;
        return num;
    }
}
