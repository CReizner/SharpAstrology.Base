using System;
using SharpAstrology.Enums;
using SharpAstrology.DataModels;

namespace SharpAstrology.Interfaces;


public interface IPlanetPositionProvider
{
    /// <summary>
    /// Get latitude, longitude and distance of a planets position and additionally the speed in each direction.
    /// </summary>
    /// <param name="planet">Enum of the planet from which the position is to be determined.</param>
    /// <param name="pointInTime">A NodaTime Instant that represents the point in time in which the planets position
    /// should be determent.</param>
    /// <returns>An instance of <see cref="PlanetPosition"/>.</returns>
    /// <exception cref="ArgumentException">Thrown if the pointInTime is not of kind UTC.</exception>
    /// <exception cref="Exception">Thrown if an error occurs during the calculation.</exception>
    public PlanetPosition PlanetsPosition(Planets planet, DateTime pointInTime, 
        EphCalculationMode mode = EphCalculationMode.Tropic);
}