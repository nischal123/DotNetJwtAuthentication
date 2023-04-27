using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthentication.ServiceExtensions
{
    public static class ServiceExtensions
    {

        //Firstly, we register the JWT authentication middleware by calling the AddAuthentication method.
        //Next, we specify the default authentication scheme JwtBearerDefaults.AuthenticationScheme as well as DefaultChallengeScheme.

        public static void AddJWTAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:5001",
                    ValidAudience = "https://localhost:5001",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                };
            });




        }
        
        public static void AddCorsSupport(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });




        }
    }
}
