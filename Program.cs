using Microsoft.EntityFrameworkCore;
using Simple_API_Assessment.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
	options.UseNpgsql(
		builder.Configuration.GetConnectionString("DefaultConnection")!));

var app = builder.Build();

using (var scope = app.Services.CreateScope()) {
	var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
	try {
		var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
		if (dbContext.Database.IsNpgsql()) {
			await dbContext.Database.MigrateAsync();
		}
	}
	catch (Exception e) {
		logger.LogError(e, "An error occurred while migrating the database.");
		Console.WriteLine(e);
	}
}


if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankService API V1"); });
}
else {
	app.UseHttpsRedirection();
}

app.MapControllers();

app.Run();