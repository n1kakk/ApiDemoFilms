
using ApiDemoFilms.MiddleWare;
using Films.DAL.Helpers;
using Films.DAL.Interfaces;
using Films.DAL.InterfacesServices;
using Films.DAL.Repository;
using Films.DAL.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.LogTo(Console.WriteLine);
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    connectionString = "User Id=root;Host=localhost;Database=db;Persist Security Info=True;Password=password";

    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddTransient<IRefreshTokenRepository, TokenRepository>();

builder.Services.AddSingleton<ConnectionMultiplexer>(sp =>
{
    return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection"));
});

//Регистрация настроек, раздел AppSettings из файла конфигурации 
//для настройки экземпляра класса AppSettings
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<IFilmRepository, FilmRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IDirectorRepository, DirectorRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IDirectorService, DirectorService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSingleton<IRefreshTokenRepository, TokenRepository>();
builder.Services.AddSingleton<ITokenService, TokenService>();

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


app.UseMiddleware<JwtMiddleWare>();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
