using FitGpt.Data;
using FitGpt.Filter;
using FitGpt.IRepository;
using FitGpt.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using FitGpt.IServices;
using FitGpt.Services;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using FitGpt.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//adding service for jwt token authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });
builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(@"Keys"));

//adding service for dbcontext
builder.Services.AddDbContext<FitGptDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection_Local"));
    options.EnableSensitiveDataLogging();
});

//adding service for swagger
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1",
       new OpenApiInfo
       {
           Title = "FitGpt",
           Version = "v1",
           Description = "All API for FitGpt"
       });

    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
});



//adding other services
builder.Services.AddAutoMapper(typeof(AppMapper));
builder.Services.AddScoped<CustomAuthorizeFilter>();
builder.Services.AddScoped<GlobalExceptionFilter>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFoodItemService, FoodItemService>();
builder.Services.AddScoped<IMealService, MealService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IDietitianService, DietitianService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IDietitianRepository, DietitianRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));





builder.Services.AddControllers(options =>
{
    //options.Filters.Add<GlobalExceptionFilter>();
})
.ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(ms => ms.Value.Errors.Count > 0)
            .Select(ms => new
            {
                Field = ms.Key,
                Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            });

        var errorResponse = new
        {
            StatusCode = 400,
            Message = "Validation failed",
            Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
    };
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//adding CORE service to restrict the origins 
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder => builder
            .WithOrigins("http://localhost:7217/swagger/index.html")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .SetIsOriginAllowed((hosts) => true));

});

var app = builder.Build();
app.UseCors("CORSPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
