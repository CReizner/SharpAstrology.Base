namespace SharpAstrology.DataModels;

/// <summary>
/// Represents the position of a planet in space.
/// </summary>
public sealed class PlanetPosition
{
    /// <summary>
    /// Longitude of the celestial object.
    /// </summary>
    public double Longitude { get; init; }

    /// <summary>
    /// Latitude of the celestial object.
    /// </summary>
    public double Latitude { get; init; }

    /// <summary>
    /// Distance of the celestial object from a reference point.
    /// </summary>	
    public double Distance { get; init; }

    /// <summary>
    /// Speed of the celestial object along the longitude.
    /// </summary>
    public double SpeedLongitude { get; init; }

    /// <summary>
    /// Speed of the celestial object along the latitude.
    /// </summary>
    public double SpeedLatitude { get; init; }

    /// <summary>
    /// Speed of the celestial object along the distance from the reference point.
    /// </summary>
    public double SpeedDistance { get; init; }
}