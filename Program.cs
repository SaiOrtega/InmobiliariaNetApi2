using InmobiliariaNetApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var configuration = builder.Configuration;

builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:5001", "http://*:5000", "https://*:5001");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* PARA MySql - usando Pomelo */

builder.Services.AddDbContext<DataContext>(
	options => options.UseMySql(
	configuration["ConnectionStrings:MySql"],
	ServerVersion.AutoDetect(configuration["ConnectionStrings:MySql"])
	)
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

	.AddJwtBearer(options =>//la api web valida con token
	{
		options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = configuration["TokenAuthentication:Issuer"],
			ValidAudience = configuration["TokenAuthentication:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(
				configuration["TokenAuthentication:SecretKey"])),
		};
		// opción extra para usar el token en el hub y otras peticiones sin encabezado (enlaces, src de img, etc.)
		options.Events = new JwtBearerEvents
		{
			OnMessageReceived = context =>
				{
					// Leer el token desde el query string
					var accessToken = context.Request.Query["access_token"];
					// Si el request es para el Hub u otra ruta seleccionada...
					var path = context.HttpContext.Request.Path;
					if (!string.IsNullOrEmpty(accessToken) &&
						(path.StartsWithSegments("/chatsegurohub") ||
						path.StartsWithSegments("/api/propietarios/reset") ||
						path.StartsWithSegments("/api/propietarios/token")))
					{//reemplazar las urls por las necesarias ruta ⬆
						context.Token = accessToken;
					}
					return Task.CompletedTask;
				}
		};
	});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthorization(options =>
{
	//options.AddPolicy("Empleado", policy => policy.RequireClaim(ClaimTypes.Role, "Administrador", "Empleado"));
	options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
	// options.AddPolicy("Empleado", policy => policy.RequireRole("Empleado"));


});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseStaticFiles();

//app.UseHttpsRedirection();

// Habilitar CORS
app.UseCors(x => x
	.AllowAnyOrigin()
	.AllowAnyMethod()
	.AllowAnyHeader());



app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
