namespace UpscaleDown.EventDriven.Exceptions;

[System.Serializable]
public class OptionsNotFoundException<T> : EventDrivenException
{
    public OptionsNotFoundException(string code, string message) : base(code, string.Format(message, typeof(T).Name))
    {
    }
}