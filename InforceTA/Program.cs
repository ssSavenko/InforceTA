
using DB;
using InforceTA.Helpers;
using InforceTA.Models.Auth;
using InforceTA.Service;
using InforceTA.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens; 
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var availableCors = "_myAllowSpecificOrigins";
var config = builder.Configuration;
 
builder.Services.AddAuthentication(x =>
{ 
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    
}).AddJwtBearer(x => {
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"], 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
        
        ValidateIssuer = true, 
        ValidateAudience = true, 
        ValidateIssuerSigningKey = true, 
        ValidateLifetime = true
    }; 
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: availableCors,
                      builder =>
                      {
                          builder.AllowAnyOrigin();
                          builder.AllowAnyHeader();
                          builder.AllowAnyMethod();
                      });
});

builder.Services.AddDbContext<PhotoGalleryContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); 
var dbOptions = new DbContextOptionsBuilder<PhotoGalleryContext>()
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .Options; 
builder.Services.AddSingleton(ctx => new Func<PhotoGalleryContext>(() => new PhotoGalleryContext(dbOptions))); 


var jwtSection = builder.Configuration.GetSection("JwtSettings"); 
builder.Services.Configure<JwtBearerTokenSettings>(jwtSection);



builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ILikesService , LikesService>();
builder.Services.AddTransient<IDislikesService, DislikesService>();
builder.Services.AddTransient<IAlbumService, AlbumService>();
builder.Services.AddTransient<IImageService, ImageService>();


builder.Services.AddAuthorization();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
 
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(availableCors);
app.UseHttpsRedirection(); 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
