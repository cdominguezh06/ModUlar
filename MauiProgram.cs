using Microsoft.Extensions.Logging;
using PacoYakuzaMAUI.services;
using PacoYakuzaMAUI.Services;
using PacoYakuzaMAUI.utils;

namespace PacoYakuzaMAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddSingleton<EventAggregator>();
        builder.Services.AddScoped<BackgroundState>();
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<GitHubService>();
        builder.Services.AddSingleton<ILauncherService, LauncherService>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}