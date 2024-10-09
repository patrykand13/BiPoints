using BiPoints.BLL.Interfaces.Authenticate;
using BiPoints.BLL.Interfaces.Scan;
using BiPoints.BLL.Interfaces.User;
using BiPoints.BLL.Services.Authenticate;
using BiPoints.BLL.Services.Scan;
using BiPoints.BLL.Services.User;
using BiPoints.DAL.Interfaces;
using BiPoints.DAL.Interfaces.Authenticate;
using BiPoints.DAL.Interfaces.Item;
using BiPoints.DAL.Interfaces.Point;
using BiPoints.DAL.Interfaces.Scan;
using BiPoints.DAL.Interfaces.User;
using BiPoints.DAL.Repositories;
using BiPoints.DAL.Repositories.Authenticate;
using BiPoints.DAL.Repositories.Item;
using BiPoints.DAL.Repositories.Point;
using BiPoints.DAL.Repositories.Scan;
using BiPoints.DAL.Repositories.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BiPoints.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // For Entity Framework  
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnStr")));

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });
            //Initialize Services DI
            services.AddScoped<IAuthenticateServices, AuthenticateServices>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IScanService, ScanService>();
            services.AddScoped<IScanHistoryService, ScanHistoryService>();

            //Initialize Repositories DI
            services.AddScoped<IAuthenticateRepositories, AuthenticateRepositories>();
            services.AddScoped<ISaveRepositories, SaveRepositories>();
            services.AddScoped<IGetUserDataRepositories, GetUserDataRepositories>();
            services.AddScoped<IItemRepositories, ItemRepositories>();
            services.AddScoped<ICreatePointRepositories, CreatePointRepositories>();
            services.AddScoped<IScanHistoryRepositories, ScanHistoryRepositories>();
            services.AddScoped<IAddPointsRepositories, AddPointsRepositories>();
            services.AddScoped<IScanRepositories, ScanRepositories>();
            services.AddScoped<IGetPointRepositories, GetPointRepositories>();
            services.AddScoped<ICreateUserRepositories, CreateUserRepositories>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
