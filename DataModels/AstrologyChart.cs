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

/// <summary>
/// A chart of the celestial objects at one point in time, optionally with house cusps for one
/// place.
/// </summary>
/// <remarks>
/// A chart stands in exactly one zodiac. It is chosen when the chart is built and it holds for
/// the planets, the house cusps and the axes alike. <see cref="CalculationMode"/> says which one
/// it is.
/// </remarks>
public sealed class AstrologyChart : IAstrologyChart
{
    public DateTime PointInTime { get; }
    public HousePosition? HousePositions { get; }
    public Planets[] SupportedObjects { get; }

    /// <summary>
    /// The offset between the tropical and the sidereal zodiac at <see cref="PointInTime"/>.
    /// </summary>
    /// <remarks>
    /// This is an information about the point in time, not an instruction. It is the same number
    /// for both values of <see cref="CalculationMode"/>. Subtracting it from a longitude of this
    /// chart is only ever right when the chart stands in <see cref="EphCalculationMode.Tropic"/>.
    /// </remarks>
    public double Ayanamsa { get; }

    /// <inheritdoc />
    public EphCalculationMode CalculationMode { get; }

    private static readonly Planets[] DefaultObjects =
    [
        Planets.Sun, Planets.Moon, Planets.Mercury, Planets.Venus, Planets.Mars,
        Planets.Jupiter, Planets.Saturn, Planets.NorthNode, Planets.SouthNode,
        Planets.Uranus, Planets.Neptune, Planets.Pluto
    ];

    private readonly Dictionary<Planets, PlanetPosition> _planetsPositions;

    #region Constructor
    /// <summary>
    /// A chart without a place, so without house cusps and axes.
    /// </summary>
    /// <param name="pointInTime">The moment the chart is calculated for.</param>
    /// <param name="eph">The ephemerides the positions come from.</param>
    /// <param name="includePlanets">
    /// The objects the chart carries. Defaults to the ten classical planets plus both lunar nodes.
    /// </param>
    /// <param name="mode">
    /// The zodiac of the chart. A chart without a place still needs the choice, because a transit
    /// chart belongs to a radix chart and the two have to stand in the same zodiac.
    /// </param>
    public AstrologyChart(DateTime pointInTime, IEphemerides eph, IEnumerable<Planets>? includePlanets=null,
        EphCalculationMode mode = EphCalculationMode.Tropic)
    {
        PointInTime = pointInTime;
        SupportedObjects = includePlanets?.ToArray() ?? DefaultObjects;
        CalculationMode = mode;
        _planetsPositions = eph.PlanetsPosition(SupportedObjects, pointInTime, mode);
        Ayanamsa = eph.Ayanamsa(pointInTime);
    }

    /// <summary>
    /// A chart with house cusps and axes for one place.
    /// </summary>
    /// <param name="pointInTime">The moment the chart is calculated for.</param>
    /// <param name="eph">The ephemerides the positions come from.</param>
    /// <param name="latitude">The geographic latitude of the place, in degrees.</param>
    /// <param name="longitude">The geographic longitude of the place, in degrees.</param>
    /// <param name="includePlanets">
    /// The objects the chart carries. Defaults to the ten classical planets plus both lunar nodes.
    /// </param>
    /// <param name="houseSystem">The house system the cusps are calculated in.</param>
    /// <param name="mode">
    /// The zodiac of the chart. It reaches the planets, the house cusps and the axes alike.
    /// </param>
    public AstrologyChart(DateTime pointInTime, IEphemerides eph, double latitude, double longitude,
        IEnumerable<Planets>? includePlanets = null,
        HouseSystems houseSystem = HouseSystems.Placidus,
        EphCalculationMode mode = EphCalculationMode.Tropic)
    {
        PointInTime = pointInTime;
        SupportedObjects = includePlanets?.ToArray() ?? DefaultObjects;
        CalculationMode = mode;
        _planetsPositions = eph.PlanetsPosition(SupportedObjects, pointInTime, mode);
        Ayanamsa = eph.Ayanamsa(pointInTime);
        HousePositions = eph.HouseCuspPositions(pointInTime, latitude, longitude, houseSystem, mode);
    }

    #endregion

    public IDictionary<Planets, PlanetPosition> PlanetsPositions => _planetsPositions.ToDictionary();
    public bool HousesAvailable => HousePositions is not null;

    public PlanetPosition PositionOf(Planets planet)
    {
        if (!_planetsPositions.TryGetValue(planet, out var position)) throw new CelestialObjectNotSupportedException($"{planet} not supported in this chart.");
        
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
    /// Gets the division of the specified planet in the zodiac of this chart.
    /// </summary>
    /// <param name="planet">The planet for which to determine the division.</param>
    /// <returns>
    /// The division the planet stands in. <see cref="CalculationMode"/> says whether that is a sign
    /// of the tropical zodiac or a constellation of the sidereal one.
    /// </returns>
    public Zodiac SignOf(Planets planet)
    {
        return AstrologyUtility.ZodiacSignOf(PositionOf(planet).Longitude);
    }

    /// <summary>
    /// Gets the division of a specified direction of the cross in the zodiac of this chart.
    /// </summary>
    /// <param name="direction">The direction of the cross (e.g., Ascendant, Midheaven).</param>
    /// <returns>
    /// The division the direction stands in. <see cref="CalculationMode"/> says whether that is a
    /// sign of the tropical zodiac or a constellation of the sidereal one.
    /// </returns>
    /// <exception cref="HousesNotAvailableException">Thrown if the house positions are not available.</exception>
    public Zodiac SignOf(Cross direction)
    {
        if (HousePositions is null) throw new HousesNotAvailableException();
        return AstrologyUtility.ZodiacSignOf(HousePositions.Cross[direction]);
    }

    /// <summary>
    /// Gets the division of a specified house cusp in the zodiac of this chart.
    /// </summary>
    /// <param name="house">The house cusp for which to determine the division.</param>
    /// <returns>
    /// The division the cusp stands in. <see cref="CalculationMode"/> says whether that is a sign
    /// of the tropical zodiac or a constellation of the sidereal one.
    /// </returns>
    /// <exception cref="HousesNotAvailableException">Thrown if the house positions are not available.</exception>
    public Zodiac SignOf(Houses house)
    {
        if (HousePositions is null) throw new HousesNotAvailableException();
        return AstrologyUtility.ZodiacSignOf(HousePositions.HouseCusps[house]);
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
    /// Determines the state of the specified planet in the zodiac of this chart.
    /// </summary>
    /// <param name="planet">The planet for which to determine the state.</param>
    /// <returns>
    /// A <see cref="PlanetStates"/> value representing the state of the planet based on default astrological rules.
    /// </returns>
    /// <exception cref="CelestialObjectNotSupportedException">Thrown if the planet is not supported in this chart.</exception>
    public PlanetStates StateOf(Planets planet)
    {
        return AstrologyDefaults.DefaultStates(planet, SignOf(planet));
    }

    /// <summary>
    /// Determines the state of the specified planet in the zodiac of this chart, using a custom
    /// mapping function.
    /// </summary>
    /// <param name="planet">The planet for which to determine the state.</param>
    /// <param name="statesMap">
    /// A function that maps a planet and its division of the zodiac to a <see cref="PlanetStates"/> value.
    /// </param>
    /// <returns>
    /// A <see cref="PlanetStates"/> value representing the state of the planet based on the provided mapping function.
    /// </returns>
    /// <exception cref="CelestialObjectNotSupportedException">Thrown if the planet is not supported in this chart.</exception>
    public PlanetStates StateOf(Planets planet, Func<Planets, Zodiac, PlanetStates> statesMap)
    {
        return statesMap(planet, SignOf(planet));
    }
}