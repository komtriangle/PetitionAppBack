using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PetitionApp.API.Configuration;
using PetitionApp.Core.Models;
using PetitionApp.Core.Repositories;
using PetitionApp.Core.Services;
using PetitionApp.Data;
using PetitionApp.Data.Configuration;
using PetitionApp.Services;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

AppSettings appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

AuthSettings authSettings = configuration.GetSection("AuthSettings").Get<AuthSettings>();
builder.Services.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));


// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                },
                UnresolvedReference = true
            },
            new List<string>()
        }
    });
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPetitionService, PetitionService>();

builder.Services.AddDbContext<PetitionAppDbContext>(options => options.UseNpgsql(appSettings.DbConnection, x => x.MigrationsAssembly("PetitionApp.Data")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<PetitionAppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication((options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}))
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {


        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = authSettings.Issuer,
            ValidAudience = authSettings.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Secret)),
        };
    });





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();
app.UseAuthorization();

//app.UseHttpsRedirection();

app.MapControllers();


app.Run();
