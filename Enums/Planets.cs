using System;

namespace SharpAstrology.Enums;

public enum Planets
{ 
        Sun = 0,
        Moon = 1,
        Mercury = 2,
        Venus = 3,
        Mars = 4,
        Jupiter = 5,
        Saturn = 6,
        Uranus = 7,
        Neptune = 8,
        Pluto = 9,
        NorthNode = 10,
        SouthNode = 11,
        Chiron = 12,
        Earth = 13,
}

public static class PlanetsExtensionMethods
{
        public static string ToName(this Planets planet)
        {
                return planet switch
                {
                        Planets.Sun => "Sun",
                        Planets.Moon => "Moon",
                        Planets.Mercury => "Mercury",
                        Planets.Venus => "Venus",
                        Planets.Mars => "Mars",
                        Planets.Jupiter => "Jupiter",
                        Planets.Saturn => "Saturn",
                        Planets.Uranus => "Uranus",
                        Planets.Neptune => "Neptune",
                        Planets.Pluto => "Pluto",
                        Planets.NorthNode => "North Node",
                        Planets.SouthNode => "South Node",
                        Planets.Chiron => "Chiron",
                        Planets.Earth => "Earth",
                        _ => throw new ArgumentOutOfRangeException(nameof(planet), planet, $"Missing implementation of {nameof(planet)}")
                };
        }
}