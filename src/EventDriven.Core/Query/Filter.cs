using UpscaleDown.EventDriven.Core.Constants;

namespace UpscaleDown.EventDriven.Core.Query;

public sealed class Filter
{
    public List<FilterField> Fields { get; set; } = new List<FilterField>();

    public static Filter Create()
    {
        return new Filter();
    }

    public Filter Eq(string field, string value)
    {
        Fields.Add(new FilterField(field, Operations.Equality, value));
        return this;
    }
    public Filter Gt(string field, string value)
    {
        Fields.Add(new FilterField(field, Operations.Greater, value));
        return this;
    }
    public Filter Lt(string field, string value)
    {
        Fields.Add(new FilterField(field, Operations.Equality, value));
        return this;
    }
    public Filter GtEq(string field, string value)
    {
        Fields.Add(new FilterField(field, Operations.GreaterOrEqual, value));
        return this;
    }
    public Filter LtEq(string field, string value)
    {
        Fields.Add(new FilterField(field, Operations.LowerOrEqual, value));
        return this;
    }
    public Filter In(string field, string value)
    {
        Fields.Add(new FilterField(field, Operations.Includes, value));
        return this;
    }
    public Filter Ex(string field, string value)
    {
        Fields.Add(new FilterField(field, Operations.Excludes, value));
        return this;
    }
}