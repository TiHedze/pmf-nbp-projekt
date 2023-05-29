using Pmf.PublicationTracker.Application;
using Pmf.PublicationTracker.Infrastructure.Db.Neo4j;
using Pmf.PublicationTracker.Infrastructure.Db.Neo4j.Internal.Settings;
using Pmf.PublicationTracker.Infrastructure.Db.Postgres;
using Pmf.PublicationTracker.Presentation.Api;

var builder = WebApplication.CreateBuilder(args);

var pgConnectionString = builder
    .Configuration
    .GetConnectionString("Postgres")!;
var neo4jSettings = builder
    .Configuration
    .GetRequiredSection(Neo4jSettings.Key)
    .Get<Neo4jSettings>()!;

// Add services to the container.
builder.Services.AddPresentationModule();
builder.Services.AddPostgresModule(pgConnectionString);
builder.Services.AddNeo4jModule(neo4jSettings);
builder.Services.AddApplicationModule();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
