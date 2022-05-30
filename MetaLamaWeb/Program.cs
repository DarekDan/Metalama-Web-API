using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

const string template =
    "{Timestamp:yy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Indent:l}{Message} {SourceContext}{NewLine}{Exception}";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, configuration) =>
{
    //configuration.ReadFrom.Configuration(context.Configuration);
    configuration.MinimumLevel.Debug()
        .Enrich.FromLogContext()
        .WriteTo.Async(w => w.Console(outputTemplate: template, theme: AnsiConsoleTheme.Literate));
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

app.UseAuthorization();

app.MapControllers();

app.Run();