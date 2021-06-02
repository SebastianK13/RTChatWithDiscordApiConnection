using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace RTChatWithDiscordApiConnection
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Config = configuration;
        public IConfiguration Config { get;}

        public void ConfigureServices(IServiceCollection services) =>
            services.AddSignalR();

        public void Configure(IApplicationBuilder builder)
        {
            builder.UseRouting();
            builder.UseEndpoints(endpoints => endpoints.MapHub<Chat>("/chat"));
        }

    }
}
