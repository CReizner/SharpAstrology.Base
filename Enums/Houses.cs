using System;

namespace SharpAstrology.Enums;

public enum Houses
{
    House1 = 1,
    House2 = 2,
    House3 = 3,
    House4 = 4,
    House5 = 5,
    House6 = 6,
    House7 = 7,
    House8 = 8,
    House9 = 9,
    House10 = 10,
    House11 = 11,
    House12 = 12
}
public static class HousesExtensionMethods
{
    public static Houses AsHouse(this int i) => i switch
    {
        1 => Houses.House1,
        2 => Houses.House2,
        3 => Houses.House3,
        4 => Houses.House4,
        5 => Houses.House5,
        6 => Houses.House6,
        7 => Houses.House7,
        8 => Houses.House8,
        9 => Houses.House9,
        10 => Houses.House10,
        11 => Houses.House11,
        12 => Houses.House12,
        _ => throw new ArgumentException($"{i} is not a supported house number.")
    };
}