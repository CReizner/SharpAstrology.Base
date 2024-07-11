using System.Collections.Generic;
using SharpAstrology.DataModels;
using SharpAstrology.Enums;

namespace SharpAstrology.Interfaces;

public interface IAstrologyChart
{
    public HousePosition? HousePositions { get; }
    public Planets[] SupportedObjects { get; }
    
    public PlanetPosition PositionOf(Planets planet);
    public double AngleOf(Planets planet1, Planets planet2);
    public Zodiac SignOf(Planets planet);
    public Motion MotionOf(Planets planet);
}