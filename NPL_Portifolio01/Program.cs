using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NPL_Portifolio01;
using NPL_Portifolio01.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<SiteState>();

var host = builder.Build();

// Load preferences and portfolio.json while the loading screen is still visible.
await host.Services.GetRequiredService<SiteState>().InitializeAsync();

await host.RunAsync();