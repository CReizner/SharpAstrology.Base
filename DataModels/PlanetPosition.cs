namespace SharpAstrology.DataModels;

/// <summary>
/// Represents the position of a planet in space.
/// </summary>
public sealed class PlanetPosition
{
    /// <summary>
    /// Longitude of the celestial object.
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Latitude of the celestial object.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Distance of the celestial object from a reference point.
    /// </summary>	
    public double Distance { get; set; }

    /// <summary>
    /// Speed of the celestial object along the longitude.
    /// </summary>
    public double SpeedLongitude { get; set; }

    /// <summary>
    /// Speed of the celestial object along the latitude.
    /// </summary>
    public double SpeedLatitude { get; set; }

    /// <summary>
    /// Speed of the celestial object along the distance from the reference point.
    /// </summary>
    public double SpeedDistance { get; set; }
}