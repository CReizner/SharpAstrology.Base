using System;
using SharpAstrology.Enums;
using SharpAstrology.DataModels;

namespace SharpAstrology.Interfaces;

public interface IEphemerides : IDisposable
{
    /// <summary>
    /// Calculates the Ayanamsa (precession of equinoxes) for a specific point in time using the Swiss Ephemeris library.
    /// </summary>
    /// <param name="pointInTime">The DateTime representing the moment for which the Ayanamsa is calculated.</param>
    /// <returns>A double value representing the calculated Ayanamsa.</returns>
    /// <exception cref="Exception">Thrown if an error occurs during the calculation.</exception>
    public double Ayanamsa(DateTime pointInTime);
    
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


    /// <summary>
    /// Calculates the house cusp positions for a given point in time, latitude, and longitude.
    /// </summary>
    /// <param name="pointInTime">The point in time for which the house cusp positions should be calculated.</param>
    /// <param name="latitude">The latitude of the location.</param>
    /// <param name="longitude">The longitude of the location.</param>
    /// <returns>An instance of <see cref="PlanetPosition"/>.</returns>
    /// <exception cref="ArgumentException">Thrown if the pointInTime is not of kind UTC.</exception>
    /// /// <exception cref="Exception">Thrown if an error occurs during the calculation.</exception>
    public HousePosition HouseCuspPositions(DateTime pointInTime, double latitude, double longitude, 
        HouseSystems houseSystems = HouseSystems.Placidus, EphCalculationMode mode = EphCalculationMode.Tropic);
        
}