using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MiAppPronostico;
using MiAppPronostico.Components;
using Radzen;
using MiAppPronostico.Services;
using MiAppPronostico.Services.Forecasting;
using MiAppPronostico.Services.Multivariable;
using MiAppPronostico.Services.Narrative;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddRadzenComponents();

// lectura de archivos
builder.Services.AddScoped<DataService>();
builder.Services.AddScoped<MultivariateDataService>();
builder.Services.AddScoped<MultivariateEngine>();
builder.Services.AddScoped<NarrativeService>();
builder.Services.AddScoped<MultivariateNarrativeService>();
// modelos matemáticos
builder.Services.AddScoped<IForecastModel, MeanModel>();
builder.Services.AddScoped<IForecastModel, NaiveModel>();
builder.Services.AddScoped<IForecastModel, DriftModel>();
builder.Services.AddScoped<IForecastModel, MovingAverageModel>();
builder.Services.AddScoped<IForecastModel, SeasonalNaiveModel>();

await builder.Build().RunAsync();