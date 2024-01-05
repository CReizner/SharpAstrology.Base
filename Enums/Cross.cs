using System;

namespace SharpAstrology.Enums;

public enum Cross
{
    Asc,
    Ic,
    Dc,
    Mc,
    // Armc,
    Vertex
}

public static class CrossExtensionMethods
{
    public static string ToName(this Cross direction)
    {
        return direction switch
        {
            Cross.Asc => "Ascendant",
            Cross.Mc => "Medium coeli",
            Cross.Ic => "Imum coeli",
            Cross.Dc => "Descendant",
            // Cross.Armc => throw new NotImplementedException("Armc is not implemented yet for ToName()"),
            Cross.Vertex => "Vertex",
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, $"Missing implementation of {nameof(direction)}")
        };
    }
}