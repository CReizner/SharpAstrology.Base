# SharpAstrology.Base - The base library for SharpAstrology

This package provides enums and data types that are fundamental to all astrological systems. 
In addition, there are some utility functions that are often used in the SharpAstrology packages. 
The most important part in this module is the IEphemerides interface, 
which must be implemented before any other SharpAstrology package can be used.

## The IEphemerides interface
The IEphemerides interface is used for all astrological modules of SharpAstrology. 
SharpAstrology currently only provides one implementation of IEphemerides. 
This implementation uses SwissEphNet (a C port of the swisseph C library). 
As swisseph has a dual license system, which is in the free version not compatible with SharpAstrology, 
the implementation is not an integral part of the base module. There may be other implementations of 
IEphemerides in the future.

## The AstrologyChart class
The AstrologyChart class provides essential functions that are used in both Western and Vedic astrology.
If you want to try the examples below, you have to install **SharpAstrology.SwissEph**.
```C#
using SharpAstrology.DataModels;
using SharpAstrology.Enums;
using SharpAstrology.Ephemerides;


var date = new DateTime(1988, 9, 4, 1, 15, 0, DateTimeKind.Utc);
using var eph = new SwissEphemeridesService(ephType:EphType.Moshier).CreateContext();

// Create chart
var chart = new AstrologyChart(date, eph);

// Houses
Console.WriteLine(chart.HousePositions is null ? "No houses available" : "Houses available");
// Output: No houses available

// Planet positions
foreach (var planet in chart.SupportedObjects)
{
    Console.WriteLine($"{planet} - {Math.Round(chart.PositionOf(planet).Longitude,2)}° in sign {chart.SignOf(planet)} and constellation {chart.ConstellationOf(planet)} is {chart.MotionOf(planet)} moving.");
}
// Output:
// Sun - 161,73° in sign Virgo and constellation Leo is Forward moving.
// Moon - 82,41° in sign Gemini and constellation Taurus is Forward moving.
// Mercury - 185,87° in sign Libra and constellation Virgo is Forward moving.
// Venus - 116,41° in sign Cancer and constellation Cancer is Forward moving.
// Mars - 10,96° in sign Aries and constellation Pisces is Retrograde moving.
// Jupiter - 65,44° in sign Gemini and constellation Taurus is Forward moving.
// Saturn - 265,95° in sign Sagittarius and constellation Sagittarius is Forward moving.
// NorthNode - 344,1° in sign Pisces and constellation Aquarius is Retrograde moving.
// SouthNode - 164,1° in sign Virgo and constellation Leo is Retrograde moving.
// Uranus - 267,04° in sign Sagittarius and constellation Sagittarius is Retrograde moving.
// Neptune - 277,48° in sign Capricorn and constellation Sagittarius is Retrograde moving.
// Pluto - 220,35° in sign Scorpio and constellation Libra is Forward moving.

// Angles between planets (-180°,180°].
Console.WriteLine(chart.AngleOf(Planets.Sun, Planets.Mars));
Console.WriteLine(chart.AngleOf(Planets.Mars, Planets.Sun));
// Output:
// 150,76609634460928
// -150,76609634460928
```
The longitudes are based on the vernal equinox (as in Western astrology).
The chart is divided into signs, houses and constellations.
The constellations are based on the positions of the constellations in the sky, and the chart stores the offset between the vernal equinox and the beginning of the
and the beginning of the constellation of Aries.
Based on the Vedic system, this offset is called Ayanamsa. The offset is given by the IEphemerides implementation.

### Houses and the cross are given if longitude and latitude are given.
```C#
using SharpAstrology.DataModels;
using SharpAstrology.Ephemerides;


var date = new DateTime(1988, 9, 4, 1, 15, 0, DateTimeKind.Utc);
using var eph = new SwissEphemeridesService(ephType:EphType.Moshier).CreateContext();

// Create chart
var chart = new AstrologyChart(date, eph, 51.0, 12.0);

// Houses
Console.WriteLine(chart.HousePositions is null ? "No houses available" : "Houses available");
// Output: Houses available

// Planet positions
foreach (var planet in chart.SupportedObjects)
{
    Console.WriteLine($"{planet} - {Math.Round(chart.PositionOf(planet).Longitude,2)}° in sign {chart.SignOf(planet)} and constellation {chart.ConstellationOf(planet)} is {chart.MotionOf(planet)} moving.");
}
// Output:
// Sun - 161,73° in sign Virgo and constellation Leo is Forward moving.
// Moon - 82,41° in sign Gemini and constellation Taurus is Forward moving.
// Mercury - 185,87° in sign Libra and constellation Virgo is Forward moving.
// Venus - 116,41° in sign Cancer and constellation Cancer is Forward moving.
// Mars - 10,96° in sign Aries and constellation Pisces is Retrograde moving.
// Jupiter - 65,44° in sign Gemini and constellation Taurus is Forward moving.
// Saturn - 265,95° in sign Sagittarius and constellation Sagittarius is Forward moving.
// NorthNode - 344,1° in sign Pisces and constellation Aquarius is Retrograde moving.
// SouthNode - 164,1° in sign Virgo and constellation Leo is Retrograde moving.
// Uranus - 267,04° in sign Sagittarius and constellation Sagittarius is Retrograde moving.
// Neptune - 277,48° in sign Capricorn and constellation Sagittarius is Retrograde moving.
// Pluto - 220,35° in sign Scorpio and constellation Libra is Forward moving.

foreach (var (direction, longitude) in chart.HousePositions!.Cross)
{
    Console.WriteLine($"{direction} - {Math.Round(longitude,2)}° in sign {chart.SignOf(direction)} and constellation {chart.ConstellationOf(direction)}.");
}
// Output:
// Asc - 126,41° in sign Leo and constellation Cancer.
// Mc - 15,35° in sign Aries and constellation Pisces.
// Ic - 195,35° in sign Libra and constellation Virgo.
// Dc - 306,41° in sign Aquarius and constellation Capricorn.
// Vertex - 264,22° in sign Sagittarius and constellation Scorpio.

foreach (var (cusp, longitude) in chart.HousePositions!.HouseCusps)
{
    Console.WriteLine($"{cusp} - {Math.Round(longitude,2)}° in sign {chart.SignOf(cusp)} and constellation {chart.ConstellationOf(cusp)}.");
}
// Output:
// House1 - 126,41° in sign Leo and constellation Cancer.
// House2 - 143,48° in sign Leo and constellation Cancer.
// House3 - 165,36° in sign Virgo and constellation Leo.
// House4 - 195,35° in sign Libra and constellation Virgo.
// House5 - 234,87° in sign Scorpio and constellation Scorpio.
// House6 - 275,14° in sign Capricorn and constellation Sagittarius.
// House7 - 306,41° in sign Aquarius and constellation Capricorn.
// House8 - 323,48° in sign Aquarius and constellation Capricorn.
// House9 - 345,36° in sign Pisces and constellation Aquarius.
// House10 - 15,35° in sign Aries and constellation Pisces.
// House11 - 54,87° in sign Taurus and constellation Taurus.
// House12 - 95,14° in sign Cancer and constellation Gemini.
```

### Specify the planets that are important for your chart
```C#
using SharpAstrology.DataModels;
using SharpAstrology.Enums;
using SharpAstrology.Ephemerides;


var date = new DateTime(1988, 9, 4, 1, 15, 0, DateTimeKind.Utc);
using var eph = new SwissEphemeridesService(ephType:EphType.Moshier).CreateContext();

// Create chart
var chart = new AstrologyChart(date, eph, [Planets.Sun, Planets.Moon, Planets.Mars]);

// Houses
Console.WriteLine(chart.HousePositions is null ? "No houses available" : "Houses available");
// Output: Houses available

// Planet positions
foreach (var planet in chart.SupportedObjects)
{
    Console.WriteLine($"{planet} - {Math.Round(chart.PositionOf(planet).Longitude,2)}° in sign {chart.SignOf(planet)} and constellation {chart.ConstellationOf(planet)} is {chart.MotionOf(planet)} moving.");
}
// Output:
// Sun - 161,73° in sign Virgo and constellation Leo is Forward moving.
// Moon - 82,41° in sign Gemini and constellation Taurus is Forward moving.
// Mars - 10,96° in sign Aries and constellation Pisces is Retrograde moving.
```



