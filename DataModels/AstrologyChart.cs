using System;
using System.Collections.Generic;
using System.Linq;
using SharpAstrology.Definitions;
using SharpAstrology.Enums;
using SharpAstrology.Exceptions;
using SharpAstrology.Interfaces;
using SharpAstrology.ExtensionMethods;
using SharpAstrology.Utility;

namespace SharpAstrology.DataModels;

public sealed class AstrologyChart
{
    public HousePosition? HousePositions { get; }
    public Planets[] SupportedObjects { get; }
    public double Ayanamsa { get; }
    private readonly Dictionary<Planets, PlanetPosition> _planetsPositions;

    
    #region Constructor
    public AstrologyChart(DateTime pointInTime, IEphemerides eph, IEnumerable<Planets>? includePlanets=null)
    {
        SupportedObjects = includePlanets?.ToArray() ??
        [
            Planets.Sun, Planets.Moon, Planets.Mercury, Planets.Venus, Planets.Mars,
            Planets.Jupiter, Planets.Saturn, Planets.NorthNode, Planets.SouthNode,
            Planets.Uranus, Planets.Neptune, Planets.Pluto
        ];
        _planetsPositions = eph.PlanetsPosition(SupportedObjects, pointInTime);
        Ayanamsa = eph.Ayanamsa(pointInTime);
    }

    public AstrologyChart(DateTime pointInTime, IEphemerides eph, double latitude, double longitude, IEnumerable<Planets>? includePlanets=null)
    {
        SupportedObjects = includePlanets?.ToArray() ??
        [
            Planets.Sun, Planets.Moon, Planets.Mercury, Planets.Venus, Planets.Mars,
            Planets.Jupiter, Planets.Saturn, Planets.NorthNode, Planets.SouthNode,
            Planets.Uranus, Planets.Neptune, Planets.Pluto
        ];
        _planetsPositions = eph.PlanetsPosition(SupportedObjects, pointInTime);
        Ayanamsa = eph.Ayanamsa(pointInTime);
        HousePositions = eph.HouseCuspPositions(pointInTime, latitude, longitude);
    }
    #endregion

    /// <summary>
    /// Retrieves the position of the specified planet.
    /// </summary>
    /// <param name="planet">The planet for which to retrieve the position.</param>
    /// <param name="regardingConstellation">Set to true, if you want the longitude relative to the start of the constellation Aries.</param>
    /// <returns>
    /// A <see cref="PlanetPosition"/> object representing the position of the specified planet.
    /// </returns>
    /// <exception cref="CelestialObjectNotSupportedException">
    /// Thrown if the specified planet is not supported in this chart.
    /// </exception>
    public PlanetPosition PositionOf(Planets planet, bool regardingConstellation=false)
    {
        if (!_planetsPositions.TryGetValue(planet, out var position)) throw new CelestialObjectNotSupportedException($"{planet} not supported in this chart.");
        if (regardingConstellation)
        {
            position.Longitude -= Ayanamsa;
        }

        return position;
    }

    /// <summary>
    /// Calculates the angle between the longitudes of two specified planets.
    /// </summary>
    /// <param name="planet1">The first planet.</param>
    /// <param name="planet2">The second planet.</param>
    /// <returns>The angle in degrees between the longitudes of the two specified planets.</returns>
    public double AngleOf(Planets planet1, Planets planet2)
    {
        return AstrologyUtility.AngleDifference(PositionOf(planet1).Longitude, PositionOf(planet2).Longitude);
    }

    /// <summary>
    /// Gets the zodiac sign of the specified planet.
    /// </summary>
    /// <param name="planet">The planet for which to determine the zodiac sign.</param>
    /// <returns>The zodiac sign of the specified planet.</returns>
    public Zodiac SignOf(Planets planet)
    {
        return AstrologyUtility.ZodiacSignOf(PositionOf(planet).Longitude);
    }

    /// <summary>
    /// Gets the zodiac sign of a specified direction of the cross.
    /// </summary>
    /// <param name="direction">The direction of the cross (e.g., Ascendant, Midheaven).</param>
    /// <returns>The zodiac sign corresponding to the specified direction.</returns>
    /// <exception cref="HousesNotAvailableException">Thrown if the house positions are not available.</exception>
    public Zodiac SignOf(Cross direction)
    {
        if (HousePositions is null) throw new HousesNotAvailableException();
        return AstrologyUtility.ZodiacSignOf(HousePositions.Cross[direction]);
    }

    /// <summary>
    /// Gets the zodiac sign of a specified house cusp.
    /// </summary>
    /// <param name="house">The house cusp for which to determine the zodiac sign.</param>
    /// <returns>The zodiac sign corresponding to the specified house cusp.</returns>
    /// <exception cref="HousesNotAvailableException">Thrown if the house positions are not available.</exception>
    public Zodiac SignOf(Houses house)
    {
        if (HousePositions is null) throw new HousesNotAvailableException();
        return AstrologyUtility.ZodiacSignOf(HousePositions.HouseCusps[house]);
    }
    
    /// <summary>
    /// Gets the zodiac constellation of the specified planet.
    /// </summary>
    /// <param name="planet">The planet for which to determine the zodiac constellation.</param>
    /// <returns>The zodiac constellation of the specified planet.</returns>
    public Zodiac ConstellationOf(Planets planet)
    {
        return AstrologyUtility.ZodiacSignOf(AstrologyUtility.SubtractDegree(PositionOf(planet).Longitude, Ayanamsa));
    }
    
    /// <summary>
    /// Gets the zodiac constellation of a specified direction of the cross.
    /// </summary>
    /// <param name="direction">The direction if the cross (e.g., Ascendant, Midheaven).</param>
    /// <returns>The zodiac constellation corresponding to the specified direction.</returns>
    /// <exception cref="HousesNotAvailableException">Thrown if the house positions are not available.</exception>
    public Zodiac ConstellationOf(Cross direction)
    {
        if (HousePositions is null) throw new HousesNotAvailableException();
        return AstrologyUtility.ZodiacSignOf(AstrologyUtility.SubtractDegree(HousePositions.Cross[direction], Ayanamsa));
    }
    
    /// <summary>
    /// Gets the zodiac constellation of a specified house cusp.
    /// </summary>
    /// <param name="house">The house cusp for which to determine the zodiac constellation.</param>
    /// <returns>The zodiac constellation corresponding to the specified house cusp.</returns>
    /// <exception cref="HousesNotAvailableException">Thrown if the house positions are not available.</exception>
    public Zodiac ConstellationOf(Houses house)
    {
        if (HousePositions is null) throw new HousesNotAvailableException();
        return AstrologyUtility.ZodiacSignOf(AstrologyUtility.SubtractDegree(HousePositions.HouseCusps[house], Ayanamsa));
    }
    
    /// <summary>
    /// Determines the motion of the specified planet, indicating whether it is in retrograde or forward motion.
    /// </summary>
    /// <param name="planet">The planet for which to determine the motion.</param>
    /// <returns>
    /// A <see cref="Motion"/> value indicating the motion of the planet: 
    /// <c>Motion.Retrograde</c> if the planet's longitudinal speed is negative, 
    /// otherwise <c>Motion.Forward</c>.
    /// </returns>
    public Motion MotionOf(Planets planet)
    {
        return PositionOf(planet).SpeedLongitude < 0 ? Motion.Retrograde : Motion.Forward;
    }

    /// <summary>
    /// Determines the state of the specified planet. You can choose the state evaluation regarding sign or constellation.
    /// </summary>
    /// <param name="planet">The planet for which to determine the state.</param>
    /// <param name="regardingConstellation">
    /// A boolean value indicating whether to consider the planet's constellation (true) or its sign (false).
    /// </param>
    /// <returns>
    /// A <see cref="PlanetStates"/> value representing the state of the planet based on default astrological rules.
    /// </returns>
    /// <exception cref="CelestialObjectNotSupportedException">Thrown if the planet is not supported in this chart.</exception>
    public PlanetStates StateOf(Planets planet, bool regardingConstellation=false)
    {
        return AstrologyDefaults.DefaultStates(planet, regardingConstellation ? ConstellationOf(planet) : SignOf(planet));
    }
    
    /// <summary>
    /// Determines the state of the specified planet using a custom mapping function.
    /// You can choose the state evaluation regarding sign or constellation.
    /// </summary>
    /// <param name="planet">The planet for which to determine the state.</param>
    /// <param name="statesMap">
    /// A function that maps a planet and a zodiac (constellation or sign) to a <see cref="PlanetStates"/> value.
    /// </param>
    /// <param name="regardingConstellation">
    /// A boolean value indicating whether to consider the planet's constellation (true) or its sign (false).
    /// </param>
    /// <returns>
    /// A <see cref="PlanetStates"/> value representing the state of the planet based on the provided mapping function.
    /// </returns>
    /// <exception cref="CelestialObjectNotSupportedException">Thrown if the planet is not supported in this chart.</exception>
    public PlanetStates StateOf(Planets planet, Func<Planets, Zodiac, PlanetStates> statesMap, bool regardingConstellation=false)
    {
        return statesMap(planet, regardingConstellation ? ConstellationOf(planet) : SignOf(planet));
    }
}