namespace NPicker;

public static class HandlersRegistry
{
    public static MauiAppBuilder UseNPicker(this MauiAppBuilder builder)
    {
        return builder.ConfigureMauiHandlers(handlers =>
        {
            handlers.AddHandler(typeof(DatePicker), typeof(DatePickerHandler));
        });
    }
}
