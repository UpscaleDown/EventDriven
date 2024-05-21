namespace UpscaleDown.EventDriven.Exceptions;

[System.Serializable]
public class InvalidFilterException : EventDrivenException
{
    public InvalidFilterException(string code, string message) : base(code, message)
    {
    }
}