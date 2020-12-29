using BcpChallenge.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BcpChallenge.Repositories;
using BcpChallenge.Services;
using BcpChallenge.Authetication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BcpChallenge
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("BcpChallenge"));
            RegisterSecurity(services);
            RegisterServices(services);
            RegisterRepository(services);
            services.AddCors();
            services.AddMvc();  
        }

        private void RegisterSecurity(IServiceCollection services)
        {
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("secret_token_bcp_challenge")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddSingleton<IJwtAutheticationManager, JwtApiAutheticationManager>();
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IChangeService, ChangeService>();
        }

        private void RegisterRepository(IServiceCollection services) { 
            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            var context = app.ApplicationServices.GetService<ApiContext>();
            AddUsers(context);
            AddCurrencyChange(context);
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddUsers(ApiContext context)
        {
            var user = new User
            {
                IdUser = 1,
                Username = "cdiaz",
                Password = "123",
                FullName = "Carlos D�az"
            };
            context.Users.Add(user);
            context.SaveChanges();
        }

        private static void AddCurrencyChange(ApiContext context)
        {
            var soles = new CurrencyChange
            {
                IdCurrencyChange = 1,
                CurrencyChangeName = "Soles",
                Change = 3.60,
                Dollars = 1
            };
            context.CurrencyChange.Add(soles);
            var euro = new CurrencyChange
            {
                IdCurrencyChange = 2,
                CurrencyChangeName = "Euro",
                Change = 0.82,
                Dollars = 1
            };
            context.CurrencyChange.Add(euro);
            var pesochileno = new CurrencyChange
            {
                IdCurrencyChange = 3,
                CurrencyChangeName = "Peso chileno",
                Change = 712.00,
                Dollars = 1
            };
            context.CurrencyChange.Add(pesochileno);
            var dollar = new CurrencyChange
            {
                IdCurrencyChange = 4,
                CurrencyChangeName = "Dolar",
                Change = 1,
                Dollars = 1
            };
            context.CurrencyChange.Add(dollar);
            context.SaveChanges();
        }


    }
}
