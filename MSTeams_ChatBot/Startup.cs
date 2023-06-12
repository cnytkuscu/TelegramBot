using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Integration;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSTeams_ChatBot.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace MSTeams_ChatBot
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Bot Framework eklemek için gerekli hizmetleri yapılandırın
            services.AddSingleton(sp =>
            {
                var options = new BotFrameworkOptions
                {
                    CredentialProvider = new SimpleCredentialProvider(Configuration["MicrosoftAppId"], Configuration["MicrosoftAppPassword"])
                };
                var conversationState = new ConversationState(new MemoryStorage());
                options.State.Add(conversationState);
                var userState = new UserState(new MemoryStorage());
                options.State.Add(userState);
                var bot = new ChatBot(conversationState, userState);
                return bot;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDefaultFiles()
                .UseStaticFiles()
                .UseMvc();
        }
    }
}
