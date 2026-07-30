namespace SharpAstrology.Enums;

/// <summary>
/// The zodiac a position is measured in.
/// </summary>
/// <remarks>
/// This is both the instruction to an <c>IEphemerides</c> implementation and the property of the
/// result, so a chart reports the mode it was calculated with. Whoever needs both zodiacs
/// calculates twice.
/// </remarks>
public enum EphCalculationMode
{
    /// <summary>
    /// The tropical zodiac. Aries begins at the vernal equinox, and the twelve divisions are the
    /// signs. They carry the names of the constellations but no longer stand in them.
    /// </summary>
    Tropic,

    /// <summary>
    /// The sidereal zodiac of Vedic astrology. Aries begins where the ayanamsa says it does, so the
    /// twelve divisions follow the constellations.
    /// </summary>
    Sidereal
}
