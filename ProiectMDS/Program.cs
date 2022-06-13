using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProiectMDS.DAL;
using ProiectMDS.DAL.Entities.Auth;
using ProiectMDS.DAL.Seeders;
using ProiectMDS.Services;
using ProiectMDS.Services.AuthService;
using ProiectMDS.Services.Helpers;
using ProiectMDS.Services.Hubs;
using ProiectMDS.Services.Managers;
using ProiectMDS.Services.UriServices;
using ProiectMDS.Services.UriServicess;
using System.Text;
using System.Text.Json.Serialization;
using Utils;
using Utils.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        B => B.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
});


builder.Services.AddSignalR();

builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        var secret = builder.Configuration.GetSection("JWT").GetSection("Secret").Get<String>();
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("Admin", policy => policy.RequireRole("Admin").RequireAuthenticatedUser().AddAuthenticationSchemes("AuthScheme").Build());
    opt.AddPolicy("User", policy => policy.RequireRole("User").RequireAuthenticatedUser().AddAuthenticationSchemes("AuthScheme").Build());  
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "ProiectMDS", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter your token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme, Id = "Bearer"
                },
            },
            new string[]{}
        }
    });
});

//Cycle detection
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//Call services
builder.Services.AddServices();
builder.Services.AddTransient<ITokenHelper, TokenHelper>(); //probleme cu ele in extensie, le las aici
builder.Services.AddTransient<IAuthManager, AuthManager>();

builder.Services.AddSingleton<IUriServices>(o => //pentru paginare
{
    var accesor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accesor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriServices(uri);
});

//Call Seeders
builder.Services.AddTransient<RoleSeeder>();


//Add mappers

builder.Services.AddMappings();

// Add services to the container.


var app = builder.Build();

SeedInjection(app);

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chatsocket");

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

//Global error handler
app.UseMiddleware<Middleware>();

app.Run();

void SeedInjection(IHost app)
{
    using (var scope = app.Services.CreateScope())
    {
        var seedRole = scope.ServiceProvider.GetRequiredService<RoleSeeder>();
        seedRole.CreateRoles();
    }
}