namespace FS.TechDemo.BuyerBFF.IdentityProvider;

public interface IIdentityProviderAdapter<TUser>
    where TUser : class
{
    //Task<Guid> InviteUserAsync(UserInvitationRequest userInvitationRequest, string clientId, string redirectUri, bool isAdmin, int? lifespan = null);

    Task<bool> SendUserUpdateEmailAsync(string userId, string clientId, string redirectUri, int? lifespan = null);

    Task<TUser> GetUserAsync(Guid userId);
    
    Task<IEnumerable<TUser>> GetUserListAsync();

    Task<bool> UpdateUserAsync(TUser user, Guid userId);

    Task<bool> DeleteUserAsync(Guid userId);

    void ChangeEmail(string newEmail, TUser user);

    Task SetAdminRoleAsync(bool isAdmin, TUser user);
}
