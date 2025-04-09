using QuestPDF.Infrastructure;
using Microsoft.EntityFrameworkCore;
using backend; // So it can find AppDbContext

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    EnvironmentName = Environments.Development
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=reports.db"));
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

QuestPDF.Settings.License = LicenseType.Community; // ✅ This activates PDF generation

Console.WriteLine("🚀 App is starting...");
app.Run();




