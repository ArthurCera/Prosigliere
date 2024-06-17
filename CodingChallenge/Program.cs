using Microsoft.EntityFrameworkCore;
using Model.Database.Context;
using Lamar.Microsoft.DependencyInjection;
using CodingChallenge.DI;

var builder = WebApplication.CreateBuilder(args);

#region configurating database
var connString = builder.Configuration.GetConnectionString("DefaultConnection");
if(connString != null) builder.Services.AddDbContextPool<DBContext>(options =>
				options.UseSqlServer(connString,
				sqlServerOptionsAction: sqlOptions =>
				{
					sqlOptions.MigrationsAssembly("Model");
					sqlOptions.EnableRetryOnFailure(
						maxRetryCount: 10,
						maxRetryDelay: TimeSpan.FromMinutes(1),
						errorNumbersToAdd: null);
				}), 4096);
#endregion

// Add services to the container.

//using Lamar to map the query handlers of the business project
builder.Host.UseLamar((context, registry) =>
{
	registry.IncludeRegistry<QueryHandlerRegistry>();
});

builder.Services.AddControllers();
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