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
        handler.PlatformView.UpdateDate(datePicker);
    }

    public static partial void MapDate(IDatePickerHandler handler, IDatePicker datePicker)
    {
        handler.PlatformView.UpdateDate(datePicker);
    }

    public static partial void MapMinimumDate(IDatePickerHandler handler, IDatePicker datePicker)
    {
        handler.PlatformView.UpdateMinimumDate(datePicker);
    }

    public static partial void MapMaximumDate(IDatePickerHandler handler, IDatePicker datePicker)
    {
        handler.PlatformView.UpdateMaximumDate(datePicker);
    }

    private void DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
    {
        if (VirtualView == null)
            return;

        if (!args.NewDate.HasValue)
        {
            VirtualView.Date = null;
            return;
        }

        VirtualView.Date = DateOnly.FromDateTime(args.NewDate.Value.Date);
    }
}