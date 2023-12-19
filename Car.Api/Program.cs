using CarWebService.API.Middlewares;
using CarWebService.BLL.Services.Contracts;
using CarWebService.BLL.Services.Implementations;
using CarWebService.DAL.Models.Entity;
using CarWebService.DAL.Repositories.Contracts;
using CarWebService.DAL.Repositories.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;

var MyAllowCors = "_myAllowOrigins";

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JWT");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Подключение NLog
builder.Logging.ClearProviders();
builder.Host.UseNLog();

//Подключение AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Подключение DbContext
builder.Services.AddApplicationContext(builder.Configuration);

//Подключение сервисов
builder.Services.AddScoped<ITokenServices, TokenServices>();
builder.Services.AddScoped<ICarServices, CarServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Подключение swagger
builder.Services.AddSwaggerGen();

//Настройка cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowCors, policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowCredentials();
        policy.WithExposedHeaders("IS-REFRESHTOKEN-EXPIRED", "IS-TOKEN-EXPIRED");
    });
});

//Подключение Identity
builder.Services.AddIdentityCore<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddUserManager<UserManager<User>>()
    .AddSignInManager<SignInManager<User>>();

//Подключение схемы авторизации через JwtBearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = TokenServices.GetSymmetricSecurityKey(),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
            ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                }

                return Task.CompletedTask;
            }
        };

    });


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<JwtRefreshTokenMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.RoutePrefix = string.Empty;
    config.SwaggerEndpoint("swagger/v1/swagger.json", "Car API");
});

app.UseHttpsRedirection();
app.MapControllers();

app.UseCors(MyAllowCors);

app.UseAuthentication();
app.UseAuthorization();

app.Run();
