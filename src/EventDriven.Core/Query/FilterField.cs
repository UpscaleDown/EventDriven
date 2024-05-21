using UpscaleDown.EventDriven.Exceptions;

namespace UpscaleDown.EventDriven.Core.Query;

public sealed class FilterField
{
    public string Field { get; set; }
    public string Operation { get; set; }
    public object Value { get; set; }

    public FilterField(string field, string operation, string value)
    {
        if (string.IsNullOrWhiteSpace(field) || string.IsNullOrWhiteSpace(operation) || string.IsNullOrWhiteSpace(value))
            throw new InvalidFilterException(Errors.InvalidFilterCode, Errors.InvalidFilterMessage);
        Field = field;
        Operation = operation;
        Value = value;
    }
}