using CaseManagement.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var provider = builder.Configuration["Database:Provider"];
builder.Services.AddDbContext<CaseDbContext>(options =>
{
    if (string.Equals(provider, "SqlServer", StringComparison.OrdinalIgnoreCase))
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
    else
        options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
});
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CaseDbContext>();
    db.Database.EnsureCreated();
}
app.Run();
