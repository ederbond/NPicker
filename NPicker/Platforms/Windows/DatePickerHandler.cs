using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml.Controls;
using NPicker.Platforms.Windows;

namespace NPicker;

public partial class DatePickerHandler : ViewHandler<IDatePicker, CalendarDatePicker>
{
    protected override CalendarDatePicker CreatePlatformView() => new CalendarDatePicker();

    protected override void ConnectHandler(CalendarDatePicker platformView)
    {
        platformView.DateChanged += DateChanged;
    }

    protected override void DisconnectHandler(CalendarDatePicker platformView)
    {
        platformView.DateChanged -= DateChanged;
    }

    public static partial void MapFormat(IDatePickerHandler handler, IDatePicker datePicker)
    {
        handler.PlatformView.UpdateValue(datePicker);
    }

    public static partial void MapValue(IDatePickerHandler handler, IDatePicker datePicker)
    {
        handler.PlatformView.UpdateValue(datePicker);
    }

    public static partial void MapMinimumValue(IDatePickerHandler handler, IDatePicker datePicker)
    {
        handler.PlatformView.UpdateMinimumValue(datePicker);
    }

    public static partial void MapMaximumValue(IDatePickerHandler handler, IDatePicker datePicker)
    {
        handler.PlatformView.UpdateMaximumValue(datePicker);
    }

    private void DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
    {
        if (VirtualView == null)
            return;

        if (!args.NewDate.HasValue)
        {
            VirtualView.Value = null;
            return;
        }

        VirtualView.Value = DateOnly.FromDateTime(args.NewDate.Value.Date);
    }
}