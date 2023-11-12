
using ApiDemoFilms.Model;
using Films.DAL.Interfaces;
using Films.DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.LogTo(Console.WriteLine);
    //var connetionString = Configuration.GetConnectionString("DefaultConnection");
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    connectionString = "User Id=root;Host=localhost;Database=db;Persist Security Info=True;Password=password";

    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

//use the JwtBearer middleware
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        //options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
        //options.Audience = builder.Configuration["Auth0:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "myAuthServer", //�������� ������
            ValidAudience = "myAuthClient", //����������� ������
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_key"))
        };
    }
    );


builder.Services.AddScoped<IFilmRepository, FilmRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IDirectorRepository, DirectorRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<>

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
        

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
