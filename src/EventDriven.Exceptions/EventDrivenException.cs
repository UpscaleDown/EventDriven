namespace UpscaleDown.EventDriven.Exceptions;

[System.Serializable]
public class EventDrivenException : System.Exception
{
    public EventDrivenException(string code, string message) : base($"{code} - {message}") { }
    public EventDrivenException(string code, string message, System.Exception inner) : base($"{code} - {message}", inner) { }
    protected EventDrivenException(
        System.Runtime.Serialization.SerializationInfo info,
#pragma warning disable SYSLIB0051 // Type or member is obsolete
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
#pragma warning restore SYSLIB0051 // Type or member is obsolete
}