namespace UpscaleDown.EventDriven.Exceptions;

[System.Serializable]
public class InvalidResourceException : EventDrivenException
{
    public InvalidResourceException(string code, string message) : base(code, message)
    {
    }
}