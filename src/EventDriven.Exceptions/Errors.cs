namespace UpscaleDown.EventDriven.Exceptions;

public static class Errors
{
    #region Resources errors
    public static readonly string InvalidResourceBuilderCode = "RES0001";
    public static readonly string InvalidResourceBuilderMessage = "Resource builder requires Provider, Origin and Entity to be set before building.";
    #endregion

    #region Query errors
    public static readonly string InvalidFilterCode = "QUE0001";
    public static readonly string InvalidFilterMessage = "Filter requires Field, Operation and Value to be set before use.";
    #endregion

}