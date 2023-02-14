using AutoMapper;
using FS.Services.User.IdentityProvider;

namespace FS.TechDemo.BuyerBFF.IdentityProvider.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddKeycloak(this IServiceCollection serviceCollection, IConfiguration configuration) => serviceCollection.AddSingleton<IIdentityProviderAdapter<Keycloak.Net.Models.Users.User>, KeyCloakAdapter>(sp => {
        var idPAccessOptions = configuration.GetSection(IdentityProviderAccessOptions.IdentityProviderAccess).Get<IdentityProviderAccessOptions>();
        var mapper = sp.GetRequiredService<IMapper>();
        var keyCloakAdapter = new KeyCloakAdapter(mapper, idPAccessOptions.Url, idPAccessOptions.AdminUserName, idPAccessOptions.AdminPassword, idPAccessOptions.Realm);
        return keyCloakAdapter;
    });
}
