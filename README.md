# SharpAstrology.Base - The base library for SharpAstrology

This package provides enums and data types that are fundamental to all astrological systems. 
In addition, there are some utility functions that are often used in the SharpAstrology packages. 
The most important part in this module is the IEphemerides interface, 
which must be implemented before any other SharpAstrology package can be used.

## SharpAstrology Packages
| Package                                                                                                                | Description                                            | Licence  |
|:-----------------------------------------------------------------------------------------------------------------------|:-------------------------------------------------------|:--------:|
| [SharpAstrology.Base](https://github.com/CReizner/SharpAstrology.Base)                                                 | Base library                                           |   MIT    |
| [SharpAstrology.SwissEph](https://github.com/CReizner/SharpAstrology.SwissEph)                                         | Ephemerides package based on SwissEphNet               | AGPL-3.0 |
| [SharpAstrology.Symbols.BlazorComponents](https://github.com/CReizner/SharpAstrology.Symbols.BlazorComponents)         | Astrological symbols as Blazor components              |   MIT    |
| [SharpAstrology.HumanDesign](https://github.com/CReizner/SharpAstrology.HumanDesign)                                   | Extensions for the Human Design system                 |   MIT    |
| [SharpAstrology.HumanDesign.BlazorComponents](https://github.com/CReizner/SharpAstrology.HumanDesign.BlazorComponents) | Human Design charts as Blazor components               |   MIT    |
| [SharpAstrology.Vedic](https://github.com/CReizner/SharpAstrology.Vedic)                                               | Extensions for Vedic astrology systems                 |   MIT    |
| [SharpAstrology.Vedic.BlazorComponents](https://github.com/CReizner/SharpAstrology.Vedic.BlazorComponents)             | Vedic astrology charts as Blazor components            |   MIT    |
| [SharpAstrology.West](https://github.com/CReizner/SharpAstrology.West)                                                 | Extensions for western astrology systems               |   MIT    |
| [SharpAstrology.West.BlazorComponents](https://github.com/CReizner/SharpAstrology.West.BlazorComponents)               | Western astrology charts as Blazor components          |   MIT    |
| [SharpAstrology.WebApp](https://github.com/CReizner/SharpAstrology.WebApp)                                             | Blazor Server app built on the SharpAstrology packages | AGPL-3.0 |

## The IEphemerides interface
The IEphemerides interface is used for all astrological modules of SharpAstrology. 
SharpAstrology currently only provides one implementation of IEphemerides: SharpAstrology.SwissEph a C# rewrite of the swisseph C library.
As swisseph has a dual license system, which is in the free version not compatible with SharpAstrology, 
the implementation is not an integral part of the base module.

## The AstrologyChart class
The AstrologyChart class provides essential functions that are used in both Western and Vedic astrology.
If you want to try the examples below, you have to install **SharpAstrology.SwissEph**.

A chart stands in exactly one zodiac. It is chosen with the `mode` parameter of the constructor and
it holds for the planets, the house cusps and the axes alike. `CalculationMode` says which one it
is, `EphCalculationMode.Tropic` for the signs (western astrology) and `EphCalculationMode.Sidereal` for the
constellations (vedic astrology). `SignOf` then names the divisions of that zodiac. Whoever needs both zodiacs builds
two charts, and the section [Charts in the sidereal zodiac](#charts-in-the-sidereal-zodiac) shows
the second one.
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
    Console.WriteLine($"{planet} - {Math.Round(chart.PositionOf(planet).Longitude,2)}° in sign {chart.SignOf(planet)} is {chart.MotionOf(planet)} moving.");
}
// Output:
// Sun - 161,73° in sign Virgo is Forward moving.
// Moon - 82,41° in sign Gemini is Forward moving.
// Mercury - 185,87° in sign Libra is Forward moving.
// Venus - 116,41° in sign Cancer is Forward moving.
// Mars - 10,96° in sign Aries is Retrograde moving.
// Jupiter - 65,44° in sign Gemini is Forward moving.
// Saturn - 265,95° in sign Sagittarius is Forward moving.
// NorthNode - 344,1° in sign Pisces is Retrograde moving.
// SouthNode - 164,1° in sign Virgo is Retrograde moving.
// Uranus - 267,04° in sign Sagittarius is Retrograde moving.
// Neptune - 277,48° in sign Capricorn is Retrograde moving.
// Pluto - 220,35° in sign Scorpio is Forward moving.

// Angles between planets (-180°,180°].
Console.WriteLine(chart.AngleOf(Planets.Sun, Planets.Mars));
Console.WriteLine(chart.AngleOf(Planets.Mars, Planets.Sun));
// Output:
// 150,76609634460928
// -150,76609634460928
```
The default is `EphCalculationMode.Tropic`, so these longitudes are measured from the vernal equinox
and the twelve divisions are the signs of western astrology. The chart is divided into signs and,
if a place of birth is given, into houses.

The chart also carries the `Ayanamsa` of its point in time. That is the offset between the vernal
equinox and the beginning of the constellation of Aries, and the name comes from the Vedic system.
The value is the same for both zodiacs, because it describes the sky and not the chart. It comes
from the IEphemerides implementation, which also decides which of the many ayanamsas is meant.

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
    Console.WriteLine($"{planet} - {Math.Round(chart.PositionOf(planet).Longitude,2)}° in sign {chart.SignOf(planet)} is {chart.MotionOf(planet)} moving.");
}
// Output:
// Sun - 161,73° in sign Virgo is Forward moving.
// Moon - 82,41° in sign Gemini is Forward moving.
// Mercury - 185,87° in sign Libra is Forward moving.
// Venus - 116,41° in sign Cancer is Forward moving.
// Mars - 10,96° in sign Aries is Retrograde moving.
// Jupiter - 65,44° in sign Gemini is Forward moving.
// Saturn - 265,95° in sign Sagittarius is Forward moving.
// NorthNode - 344,1° in sign Pisces is Retrograde moving.
// SouthNode - 164,1° in sign Virgo is Retrograde moving.
// Uranus - 267,04° in sign Sagittarius is Retrograde moving.
// Neptune - 277,48° in sign Capricorn is Retrograde moving.
// Pluto - 220,35° in sign Scorpio is Forward moving.

foreach (var (direction, longitude) in chart.HousePositions!.Cross)
{
    Console.WriteLine($"{direction} - {Math.Round(longitude,2)}° in sign {chart.SignOf(direction)}.");
}
// Output:
// Asc - 126,41° in sign Leo.
// Dc - 306,41° in sign Aquarius.
// Mc - 15,35° in sign Aries.
// Ic - 195,35° in sign Libra.
// Vertex - 264,22° in sign Sagittarius.

foreach (var (cusp, longitude) in chart.HousePositions!.HouseCusps)
{
    Console.WriteLine($"{cusp} - {Math.Round(longitude,2)}° in sign {chart.SignOf(cusp)}.");
}
// Output:
// House1 - 126,41° in sign Leo.
// House2 - 143,48° in sign Leo.
// House3 - 165,36° in sign Virgo.
// House4 - 195,35° in sign Libra.
// House5 - 234,87° in sign Scorpio.
// House6 - 275,14° in sign Capricorn.
// House7 - 306,41° in sign Aquarius.
// House8 - 323,48° in sign Aquarius.
// House9 - 345,36° in sign Pisces.
// House10 - 15,35° in sign Aries.
// House11 - 54,87° in sign Taurus.
// House12 - 95,14° in sign Cancer.
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
// Output: No houses available

// Planet positions
foreach (var planet in chart.SupportedObjects)
{
    Console.WriteLine($"{planet} - {Math.Round(chart.PositionOf(planet).Longitude,2)}° in sign {chart.SignOf(planet)} is {chart.MotionOf(planet)} moving.");
}
// Output:
// Sun - 161,73° in sign Virgo is Forward moving.
// Moon - 82,41° in sign Gemini is Forward moving.
// Mars - 10,96° in sign Aries is Retrograde moving.
```

### Charts in the sidereal zodiac
`EphCalculationMode.Sidereal` moves the whole chart into the constellations. The planets, the house
cusps and the axes all follow, and `SignOf` then names constellations. Which ayanamsa the offset
comes from is decided by the IEphemerides context, here the default of **SharpAstrology.SwissEph**.
```C#
using SharpAstrology.DataModels;
using SharpAstrology.Enums;
using SharpAstrology.Ephemerides;


var date = new DateTime(1988, 9, 4, 1, 15, 0, DateTimeKind.Utc);
using var eph = new SwissEphemeridesService(ephType:EphType.Moshier).CreateContext();

// Create chart
var chart = new AstrologyChart(date, eph, mode: EphCalculationMode.Sidereal);

Console.WriteLine(Math.Round(chart.Ayanamsa, 2));
// Output: 24,58

// Planet positions
foreach (var planet in chart.SupportedObjects)
{
    Console.WriteLine($"{planet} - {Math.Round(chart.PositionOf(planet).Longitude,2)}° in constellation {chart.SignOf(planet)} is {chart.MotionOf(planet)} moving.");
}
// Output:
// Sun - 137,14° in constellation Leo is Forward moving.
// Moon - 57,82° in constellation Taurus is Forward moving.
// Mercury - 161,29° in constellation Virgo is Forward moving.
// Venus - 91,82° in constellation Cancer is Forward moving.
// Mars - 346,38° in constellation Pisces is Retrograde moving.
// Jupiter - 40,86° in constellation Taurus is Forward moving.
// Saturn - 241,36° in constellation Sagittarius is Forward moving.
// NorthNode - 319,52° in constellation Aquarius is Retrograde moving.
// SouthNode - 139,52° in constellation Leo is Retrograde moving.
// Uranus - 242,46° in constellation Sagittarius is Retrograde moving.
// Neptune - 252,9° in constellation Sagittarius is Retrograde moving.
// Pluto - 195,76° in constellation Libra is Forward moving.
```
Every longitude here is the tropical one minus the ayanamsa.

The house cusps are the reason to calculate sidereally instead of subtracting afterwards. With
`HouseSystems.WholeSign` a sidereal chart puts them exactly on the boundaries of the constellations,
which is what a rashi chart of Vedic astrology needs.
```C#
var rashi = new AstrologyChart(date, eph, 51.0, 12.0,
    houseSystem: HouseSystems.WholeSign, mode: EphCalculationMode.Sidereal);

foreach (var (cusp, longitude) in rashi.HousePositions!.HouseCusps)
{
    Console.WriteLine($"{cusp} - {Math.Round(longitude,2)}° in constellation {rashi.SignOf(cusp)}.");
}
// Output:
// House1 - 90° in constellation Cancer.
// House2 - 120° in constellation Leo.
// House3 - 150° in constellation Virgo.
// House4 - 180° in constellation Libra.
// House5 - 210° in constellation Scorpio.
// House6 - 240° in constellation Sagittarius.
// House7 - 270° in constellation Capricorn.
// House8 - 300° in constellation Aquarius.
// House9 - 330° in constellation Pisces.
// House10 - 0° in constellation Aries.
// House11 - 30° in constellation Taurus.
// House12 - 60° in constellation Gemini.

// The ascendant keeps its degree and decides which constellation is the first house.
Console.WriteLine($"{Math.Round(rashi.HousePositions.Cross[Cross.Asc], 2)}° in {rashi.SignOf(Cross.Asc)}");
// Output: 101,83° in Cancer
```



