using System;

namespace SharpAstrology.Enums;

public enum HouseSystems
{   
    Alcabitus,
    ApcHouses,
        
    /// <summary>
    /// Axial rotation system / Meridian system / Zariel
    /// </summary>
    AxialRotationSystem,
        
    /// <summary>
    /// Azimuthal or horizontal system
    /// </summary>
    AzimutalSystem,
        
    /// <summary>
    /// Carter "Poli-Equatorial"
    /// </summary>
    Campanus,
    Carter,              
    Equal,
    EqualMc,
    Equal1Aries,
    SunshineTreindl,
    SunshineMakransky,
    Koch,
    KrusinskiPisaGoelzer,
    Morinus,
    Placidus,
        
    /// <summary>
    /// Topocentric system
    /// </summary>
    PolichPage,       
    Porphyrius,
    PullenSd,
    PullenSr, 
    Regiomontanus,
    Sripati,
    VehlowEqual,
    WholeSign
}

public static class HouseSystemsExtensionMethods
{
    public static string ToName(this HouseSystems houseSystem)
    {
        return houseSystem switch
        {
            HouseSystems.Alcabitus => "Alcabitus",
            HouseSystems.ApcHouses => "APC houses",
            HouseSystems.AxialRotationSystem => "Axial rotation system",
            HouseSystems.AzimutalSystem => "Azimutal system",
            HouseSystems.Campanus => "Campanus",
            HouseSystems.Carter => "Carter",
            HouseSystems.Equal => "Equal Ascendant",
            HouseSystems.EqualMc => "Equal MC",
            HouseSystems.Equal1Aries => "Equal 0 Aries",
            HouseSystems.SunshineTreindl => "Sunshine (Treindl solution)",
            HouseSystems.SunshineMakransky => "Sunshine (Makransky solution)",
            HouseSystems.Koch => "Koch",
            HouseSystems.KrusinskiPisaGoelzer => "Krusinski-Pisa-Goelzer",
            HouseSystems.Morinus => "Morinus",
            HouseSystems.Placidus => "Placidus",
            HouseSystems.PolichPage => "Polich/Page",
            HouseSystems.Porphyrius => "Porphyrius",
            HouseSystems.PullenSd => "Pullen SD",
            HouseSystems.PullenSr => "Pullen SR",
            HouseSystems.Regiomontanus => "Regiomontanus",
            HouseSystems.Sripati => "Sripati",
            HouseSystems.VehlowEqual => "Vehlow equal",
            HouseSystems.WholeSign => "Whole sign",
            _ => throw new ArgumentOutOfRangeException(nameof(houseSystem), houseSystem, $"Missing implementation of {nameof(houseSystem)}")
        };
    }
}