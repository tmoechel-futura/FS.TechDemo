using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace FS.TechDemo.Shared.extensions;

public static class HostBuilderExtension
{
    public static IHostBuilder ConfigureS4HWebHost<TStartup>(this IHostBuilder hostBuilder, bool enforceHttp2 = true)
        where TStartup : class =>
        hostBuilder.ConfigureWebHostDefaults(webBuilder => {
            webBuilder.UseStartup<TStartup>();
            if (enforceHttp2) {
                webBuilder.ConfigureKestrel(options =>
                                                options.ConfigureEndpointDefaults(listenOptions => listenOptions.Protocols = HttpProtocols.Http2));
            }
        });
}
