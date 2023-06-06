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
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});
app.UseHttpsRedirection();

app.UseCors(policy =>
{
    policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.MapControllers();

app.Run();
