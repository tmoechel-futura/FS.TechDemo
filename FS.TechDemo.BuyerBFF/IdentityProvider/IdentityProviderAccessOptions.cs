namespace FS.Services.User.IdentityProvider;

public class IdentityProviderAccessOptions
{
    public const string IdentityProviderAccess = "IdentityProviderAccessOptions";

    public string Url { get; set; } = "";
    public string Realm { get; set; } = "";
    public string AdminUserName { get; set; } = "";
    public string AdminPassword { get; set; } = "";
}
