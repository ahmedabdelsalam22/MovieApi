using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MovieApi.Models;
using MovieApi.Services;

var builder = WebApplication.CreateBuilder(args);

//get connextion string froms appsetting.json and put it in var to use it later..
var connectionSring = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(connectionString: connectionSring)
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IGenresService,GenresService>();
builder.Services.AddTransient<IMoviesService, MoviesService>();


builder.Services.AddCors();
builder.Services.AddSwaggerGen(options =>
   {

       options.SwaggerDoc(name: "v1", info: new OpenApiInfo
       {
           Version = "v1",
           Title = "MyMovieHub",
           Description = "Here you can found alot of interesting movies",
           TermsOfService = new Uri("https://github.com/ahmedabdelsalam22"),
           Contact = new OpenApiContact
           {
               Name = "Ahmed Abd Elsalam",
               Email = "er909112@gmail.com",
               Url = new Uri("https://github.com/ahmedabdelsalam22"),
           },
           License = new OpenApiLicense
           {
               Name = "My Api License",
               Url = new Uri("https://github.com/ahmedabdelsalam22"),
           },
       });

       options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
       {
           Name = "Authorization",
           Type = SecuritySchemeType.ApiKey,
           Scheme = "Bearer",
           BearerFormat = "JWT",
           In = ParameterLocation.Header,
           Description = "Enter your JWT key"
       });

       options.AddSecurityRequirement(securityRequirement: new OpenApiSecurityRequirement
       {
           {
             new OpenApiSecurityScheme
             {
                 Reference = new OpenApiReference
                 {
                 Type = ReferenceType.SecurityScheme,
                 Id = "Bearer",
                 },
                 Name = "Bearer",
                 In = ParameterLocation.Header,
             },
             new List<String>()
           } 
           
       });


   }
 );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
