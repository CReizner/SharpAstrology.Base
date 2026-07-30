using System.Collections.Generic;
using SharpAstrology.DataModels;
using SharpAstrology.Enums;

namespace SharpAstrology.Interfaces;

public interface IAstrologyChart
{
    public HousePosition? HousePositions { get; }
    public Planets[] SupportedObjects { get; }

    /// <summary>
    /// The zodiac this chart stands in. It is fixed when the chart is built and holds for the
    /// planets, the house cusps and the axes alike.
    /// </summary>
    /// <remarks>
    /// Without this property the result of <see cref="SignOf"/> cannot be interpreted, and any
    /// consumer subtracting an ayanamsa itself cannot tell whether that is needed. It is the same
    /// value the chart was built with, so a second chart in the same zodiac is one pass-through
    /// away.
    /// </remarks>
    public EphCalculationMode CalculationMode { get; }

    public PlanetPosition PositionOf(Planets planet);
    public double AngleOf(Planets planet1, Planets planet2);

    /// <summary>
    /// The division of the zodiac the planet stands in. <see cref="CalculationMode"/> says whether
    /// that is a sign of the tropical zodiac or a constellation of the sidereal one.
    /// </summary>
    public Zodiac SignOf(Planets planet);

    public Motion MotionOf(Planets planet);
}
