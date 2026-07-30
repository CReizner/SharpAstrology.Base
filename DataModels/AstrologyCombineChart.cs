using System;
using System.Collections.Generic;
using SharpAstrology.DataModels;
using SharpAstrology.Enums;
using SharpAstrology.Interfaces;
using SharpAstrology.Utility;

namespace SharpAstrology.Base;

/// <summary>
/// A pair of charts, for transits or for two people.
/// </summary>
/// <remarks>
/// Both charts have to stand in the same zodiac. <see cref="AngleOf"/> compares two longitudes,
/// and longitudes from two different zodiacs are about an ayanamsa apart, which is not a real
/// angle between the two objects.
/// </remarks>
public sealed class AstrologyCombineChart
{
    public AstrologyChart Chart { get; }
    public AstrologyChart ComparatorChart { get; }

    /// <summary>
    /// The zodiac both charts stand in.
    /// </summary>
    public EphCalculationMode CalculationMode => Chart.CalculationMode;

    #region

    /// <summary>
    /// Combines two charts that are already built.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// The two charts stand in different zodiacs, so their longitudes are not comparable.
    /// </exception>
    public AstrologyCombineChart(AstrologyChart chart, AstrologyChart comparatorChart)
    {
        if (chart.CalculationMode != comparatorChart.CalculationMode)
        {
            throw new ArgumentException(
                $"The chart was calculated as {chart.CalculationMode} and the comparator chart as "
                + $"{comparatorChart.CalculationMode}. Both have to stand in the same zodiac, otherwise "
                + "their longitudes are about an ayanamsa apart and AngleOf is meaningless. Build "
                + "both charts with the same EphCalculationMode.",
                nameof(comparatorChart));
        }

        Chart = chart;
        ComparatorChart = comparatorChart;
    }

    /// <summary>
    /// Builds the comparator chart for an existing chart. It inherits the zodiac of
    /// <paramref name="chart"/>, so the pair can never end up mixed.
    /// </summary>
    public AstrologyCombineChart(AstrologyChart chart, DateTime comparatorPointInTime, IEphemerides eph, IEnumerable<Planets>? includeComparatorPlanets=null)
    {
        Chart = chart;
        ComparatorChart = new AstrologyChart(comparatorPointInTime, eph, includeComparatorPlanets,
            chart.CalculationMode);
    }

    /// <summary>
    /// Builds both charts.
    /// </summary>
    /// <param name="mode">The zodiac both charts are calculated in.</param>
    public AstrologyCombineChart(DateTime pointInTime, DateTime comparatorPointInTime, IEphemerides eph,
        IEnumerable<Planets>? includePlanets=null, IEnumerable<Planets>? includePlanetsForComparator=null,
        EphCalculationMode mode = EphCalculationMode.Tropic)
    {
        Chart = new AstrologyChart(pointInTime, eph, includePlanets, mode);
        ComparatorChart = new AstrologyChart(comparatorPointInTime, eph, includePlanetsForComparator, mode);
    }
    #endregion

    public double AngleOf(Planets planet, Planets comparatorPlanet)
    {
        return AstrologyUtility.AngleDifference(Chart.PositionOf(planet).Longitude, ComparatorChart.PositionOf(comparatorPlanet).Longitude);
    }
}
