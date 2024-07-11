using System;
using System.Collections.Generic;
using SharpAstrology.DataModels;
using SharpAstrology.Enums;
using SharpAstrology.Interfaces;
using SharpAstrology.Utility;

namespace SharpAstrology.Base;

public sealed class AstrologyCombineChart
{
    public AstrologyChart Chart { get; }
    public AstrologyChart ComparatorChart { get; }
    
    #region

    public AstrologyCombineChart(AstrologyChart chart, AstrologyChart comparatorChart)
    {
        Chart = chart;
        ComparatorChart = comparatorChart;
    }

    public AstrologyCombineChart(AstrologyChart chart, DateTime comparatorPointInTime, IEphemerides eph, IEnumerable<Planets>? includeComparatorPlanets=null)
    {
        Chart = chart;
        ComparatorChart = new AstrologyChart(comparatorPointInTime, eph, includeComparatorPlanets);
    }

    public AstrologyCombineChart(DateTime pointInTime, DateTime comparatorPointInTime, IEphemerides eph, IEnumerable<Planets>? includePlanets=null, IEnumerable<Planets>? includePlanetsForComparator=null)
    {
        Chart = new AstrologyChart(pointInTime, eph, includePlanets);
        ComparatorChart = new AstrologyChart(comparatorPointInTime, eph, includePlanetsForComparator);
    }
    #endregion
    
    public double AngleOf(Planets planet, Planets comparatorPlanet)
    {
        return AstrologyUtility.AngleDifference(Chart.PositionOf(planet).Longitude, ComparatorChart.PositionOf(comparatorPlanet).Longitude);
    }
}