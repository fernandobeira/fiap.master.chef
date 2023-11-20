using fiap.master.chef.core.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MasterChefDBContext>(
    options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.Configure<RouteOptions> (
    opt => opt.LowercaseUrls = true
);

// JWT
builder.Services.AddAuthentication(x => { 
    x.DefaultAuthenticateScheme = "Jwt";
    x.DefaultChallengeScheme    = "Jwt";
}).AddJwtBearer("Jwt", o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer           = true,
        ValidateAudience         = true,
        ValidateLifetime         = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer              = "master.chef.api",
        ValidAudience            = "master.chef.api",
        ClockSkew                = TimeSpan.FromMinutes(5),
        IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("master-chef-api-auth-valid"))

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
