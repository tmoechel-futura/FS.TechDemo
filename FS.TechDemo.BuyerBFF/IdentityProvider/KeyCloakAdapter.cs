using System.Collections.ObjectModel;
using System.Net;
using AutoMapper;
using Flurl.Http;
using FS.Services.User.IdentityProvider;
using Keycloak.Net;
using Keycloak.Net.Models.Roles;
using Keycloak.Net.Models.Users;

namespace FS.TechDemo.BuyerBFF.IdentityProvider;

public class KeyCloakAdapter : KeycloakClient, IIdentityProviderAdapter<Keycloak.Net.Models.Users.User>
{
    private readonly IMapper _mapper;
    private readonly string _realm;

    private const string VerifyEmailRequiredAction = "VERIFY_EMAIL";
    private static readonly List<string> DefaultRequiredActions = new() { "UPDATE_PASSWORD", VerifyEmailRequiredAction, "TERMS_AND_CONDITIONS" };
    private const string OrganisationAdminRole = "organisation_admin";

    public KeyCloakAdapter(IMapper mapper, string baseUrl, string userName, string password, string realm) : base(baseUrl, userName, password)
    {
        _mapper = mapper;
        _realm = realm;
    }

    public async Task<bool> SendUserUpdateEmailAsync(string userId, string clientId, string redirectUri, int? lifespan = null)
    {
        bool success;

        try {
            success = await SendUserUpdateAccountEmailAsync(_realm, userId, DefaultRequiredActions, clientId, lifespan, redirectUri);
        }
        catch (Exception ex) {
            if (ex is FlurlHttpException flurlHttpException && flurlHttpException.Call.Response.StatusCode == HttpStatusCode.NotFound)
                throw new Exception(userId);

            throw;
        }

        return success;
    }

    public async Task<Keycloak.Net.Models.Users.User> GetUserAsync(Guid userId) => await GetUserAsync(_realm, userId.ToString());

    Task<IEnumerable<User>> IIdentityProviderAdapter<User>.GetUserListAsync()
    {
        return GetUsersAsync("buyer");
    }

    public async Task<bool> UpdateUserAsync(Keycloak.Net.Models.Users.User user, Guid userId)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var success = await UpdateUserAsync(_realm, userId.ToString(), user);
        return success;
    }

    public void ChangeEmail(string newEmail, Keycloak.Net.Models.Users.User user)
    {
        if (string.IsNullOrWhiteSpace(newEmail)) return;

        if (user == null) throw new ArgumentNullException(nameof(user));

        user.Email = newEmail;
        user.EmailVerified = false;
        user.RequiredActions = new ReadOnlyCollection<string>(new List<string> { VerifyEmailRequiredAction });
    }

    public async Task SetAdminRoleAsync(bool isAdmin, Keycloak.Net.Models.Users.User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var adminRealmRole = await GetRoleByNameAsync(_realm, OrganisationAdminRole);
        var roles = new List<Role> { adminRealmRole };

        if (isAdmin)
            await AddRealmRoleMappingsToUserAsync(_realm, user.Id, roles);
        else
            await DeleteRealmRoleMappingsFromUserAsync(_realm, user.Id, roles);
    }

    public async Task<bool> DeleteUserAsync(Guid userId)
    {
        var success = await DeleteUserAsync(_realm, userId.ToString());
        return success;
    }
}
