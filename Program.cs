using InfiCare.Extenisons;

var builder = WebApplication.CreateBuilder(args);

WebApplicationExtensions.ConfigureServices(builder);

var app = builder.Build();

WebConfiguration.ConfigureMiddlewares(app);

app.Run();
