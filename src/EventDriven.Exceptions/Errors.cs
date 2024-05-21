namespace UpscaleDown.EventDriven.Exceptions;

public static class Errors
{
    public static readonly string InvalidResourceBuilderCode = "ED0001";
    public static readonly string InvalidResourceBuilderMessage = "Resource builder requires Provider, Origin and Entity to be set before building.";
}