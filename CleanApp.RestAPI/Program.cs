using CleanApp.Application.UseCases.Booking;
using CleanApp.Application.UseCases.User;
using CleanApp.Domain.Repositories;
using CleanApp.Infrastructure.Repositories;
using CleanApp.RestAPI.Filters;
using CleanApp.RestAPI.Tokenizer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, PostgressUserRepository>();
builder.Services.AddScoped<IBookingRepository, PostgressBookingRepository>();

builder.Services.AddTransient<CreateBookingUseCase>();
builder.Services.AddTransient<GetBookingUseCase>();
builder.Services.AddTransient<UpdateBookingUseCase>();
builder.Services.AddTransient<DeleteBookingUseCase>();

builder.Services.AddTransient<CreateUserUseCase>();
builder.Services.AddTransient<LogInUserUseCase>();

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(new ExceptionFilter()));
builder.Services.AddScoped<JwtTokenizer, JwtTokenizer>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "apiWithAuthBackend",
            ValidAudience = "apiWithAuthBackend",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("$R0na1dM4rR0u$$$$$R0na1dM4rR0u$$$$$R0na1dM4rR0u$$$$")
            ),
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
