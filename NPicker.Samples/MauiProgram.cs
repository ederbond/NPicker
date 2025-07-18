﻿using Microsoft.Extensions.Logging;

namespace NPicker.Samples
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseNPicker()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular")
                         .AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold")
                         .AddFont("Roboto-Regular.ttf", "Roboto");
                });

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainPageViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
