using System;

namespace SharpAstrology.Enums;

public enum RulerOf
{
    First,
    Second,
    Third,
    Fourth,
    Fifth,
    Sixth,
    Seventh,
    Eight,
    Ninth,
    Tenth,
    Eleventh,
    Twelfth,
}

public static class RulerOfExtensionMethods
{
    public static Houses ToHouse(this RulerOf rulerOf) => rulerOf switch
    {
        RulerOf.First => Houses.House1,
        RulerOf.Second => Houses.House2,
        RulerOf.Third => Houses.House3,
        RulerOf.Fourth => Houses.House4,
        RulerOf.Fifth => Houses.House5,
        RulerOf.Sixth => Houses.House6,
        RulerOf.Seventh => Houses.House7,
        RulerOf.Eight => Houses.House8,
        RulerOf.Ninth => Houses.House9,
        RulerOf.Tenth => Houses.House10,
        RulerOf.Eleventh => Houses.House11,
        RulerOf.Twelfth => Houses.House12,
        _ => throw new ArgumentException($"Parameter {rulerOf} not listed in type RulerOf.")
    };
}