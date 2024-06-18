using SharpAstrology.Enums;

namespace SharpAstrology.Definitions;

public static class AstrologyDefaults
{
    public static PlanetStates DefaultStates(Planets planet, Zodiac sign)
    {
        return planet switch
        {
            Planets.Sun when sign is Zodiac.Leo => PlanetStates.Dominion,
            Planets.Sun when sign is Zodiac.Aries => PlanetStates.Exaltation,
            Planets.Sun when sign is Zodiac.Aquarius => PlanetStates.Detriment,
            Planets.Sun when sign is Zodiac.Libra => PlanetStates.Fall,
            Planets.Moon when sign is Zodiac.Cancer => PlanetStates.Dominion,
            Planets.Moon when sign is Zodiac.Taurus => PlanetStates.Exaltation,
            Planets.Moon when sign is Zodiac.Capricorn => PlanetStates.Detriment,
            Planets.Moon when sign is Zodiac.Scorpio => PlanetStates.Fall,
            Planets.Mercury when sign is Zodiac.Gemini => PlanetStates.Dominion,
            Planets.Mercury when sign is Zodiac.Virgo => PlanetStates.Exaltation,
            Planets.Mercury when sign is Zodiac.Sagittarius => PlanetStates.Detriment,
            Planets.Mercury when sign is Zodiac.Pisces => PlanetStates.Fall,
            Planets.Venus when sign is Zodiac.Taurus => PlanetStates.Dominion,
            Planets.Venus when sign is Zodiac.Libra => PlanetStates.Dominion,
            Planets.Venus when sign is Zodiac.Pisces => PlanetStates.Exaltation,
            Planets.Venus when sign is Zodiac.Aries or Zodiac.Scorpio => PlanetStates.Detriment,
            Planets.Venus when sign is Zodiac.Virgo => PlanetStates.Fall,
            Planets.Mars when sign is Zodiac.Aries => PlanetStates.Dominion,
            Planets.Mars when sign is Zodiac.Capricorn => PlanetStates.Exaltation,
            Planets.Mars when sign is Zodiac.Taurus or Zodiac.Libra => PlanetStates.Detriment,
            Planets.Jupiter when sign is Zodiac.Sagittarius or Zodiac.Pisces => PlanetStates.Dominion,
            Planets.Jupiter when sign is Zodiac.Cancer => PlanetStates.Exaltation,
            Planets.Jupiter when sign is Zodiac.Gemini or Zodiac.Virgo => PlanetStates.Detriment,
            Planets.Jupiter when sign is Zodiac.Capricorn => PlanetStates.Fall,
            Planets.Saturn when sign is Zodiac.Capricorn => PlanetStates.Dominion,
            Planets.Saturn when sign is Zodiac.Libra => PlanetStates.Exaltation,
            Planets.Saturn when sign is Zodiac.Cancer or Zodiac.Leo => PlanetStates.Detriment,
            Planets.Saturn when sign is Zodiac.Aries => PlanetStates.Fall,
            Planets.Uranus when sign is Zodiac.Aquarius => PlanetStates.Dominion,
            Planets.Uranus when sign is Zodiac.Scorpio => PlanetStates.Exaltation,
            Planets.Uranus when sign is Zodiac.Leo => PlanetStates.Detriment,
            Planets.Uranus when sign is Zodiac.Taurus => PlanetStates.Fall,
            Planets.Neptune when sign is Zodiac.Pisces => PlanetStates.Dominion,
            Planets.Neptune when sign is Zodiac.Cancer => PlanetStates.Exaltation,
            Planets.Neptune when sign is Zodiac.Virgo => PlanetStates.Detriment,
            Planets.Neptune when sign is Zodiac.Capricorn => PlanetStates.Fall,
            _ => PlanetStates.None
        };
    }
}