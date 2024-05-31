namespace UpscaleDown.EventDriven.Architecture.Configuration;

public sealed class AuthOptions
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;

    public int RefreshTokenValidityInDays { get; set; } = 1;
}