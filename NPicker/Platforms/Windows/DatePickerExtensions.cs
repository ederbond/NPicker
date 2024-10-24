using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml.Controls;

namespace NPicker.Platforms.Windows;

public static class DatePickerExtensions
{
    public static void UpdateValue(this CalendarDatePicker platformDatePicker, IDatePicker datePicker)
    {
        var date = datePicker.Value;
        platformDatePicker.UpdateValue(date);

        var format = datePicker.Format;
        var dateFormat = format.ToDateFormat();

        if (!string.IsNullOrEmpty(dateFormat))
            platformDatePicker.DateFormat = dateFormat;

        platformDatePicker.UpdateTextColor(datePicker);
    }

    public static void UpdateValue(this CalendarDatePicker platformDatePicker, DateOnly? value)
    {
        platformDatePicker.Date = value == null ? null : value.Value.ToDateTime(new TimeOnly());
    }

    public static void UpdateMinimumValue(this CalendarDatePicker platformDatePicker, IDatePicker datePicker)
    {
        platformDatePicker.MinDate = datePicker.MinimumValue == null ? DateTimeOffset.MinValue : datePicker.MinimumValue.Value.ToDateTime(new TimeOnly(0));
    }

    public static void UpdateMaximumValue(this CalendarDatePicker platformDatePicker, IDatePicker datePicker)
    {
        platformDatePicker.MaxDate = datePicker.MaximumValue == null ? DateTimeOffset.MaxValue : datePicker.MaximumValue.Value.ToDateTime(new TimeOnly(0));
    }
}