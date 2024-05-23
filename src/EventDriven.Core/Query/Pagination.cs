namespace UpscaleDown.EventDriven.Core.Query;

public sealed class Pagination
{
    public int CurrentPage { get; set; }
    public int PageLength { get; set; }
    public Pagination() { }
}