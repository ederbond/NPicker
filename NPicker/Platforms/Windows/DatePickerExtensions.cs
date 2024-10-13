using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml.Controls;

namespace NPicker.Platforms.Windows;

public static class DatePickerExtensions
{
    public static void UpdateDate(this CalendarDatePicker platformDatePicker, IDatePicker datePicker)
    {
        var date = datePicker.Date;
        platformDatePicker.UpdateDate(date);

        var format = datePicker.Format;
        var dateFormat = format.ToDateFormat();

        if (!string.IsNullOrEmpty(dateFormat))
            platformDatePicker.DateFormat = dateFormat;

        platformDatePicker.UpdateTextColor(datePicker);
    }

    public static void UpdateDate(this CalendarDatePicker platformDatePicker, DateOnly? dateTime)
    {
        platformDatePicker.Date = dateTime == null ? null : dateTime.Value.ToDateTime(new TimeOnly());
    }

    public static void UpdateMinimumDate(this CalendarDatePicker platformDatePicker, IDatePicker datePicker)
    {
        platformDatePicker.MinDate = datePicker.MinimumDate == null ? DateTimeOffset.MinValue : datePicker.MinimumDate.Value.ToDateTime(new TimeOnly(0));
    }

    public static void UpdateMaximumDate(this CalendarDatePicker platformDatePicker, IDatePicker datePicker)
    {
        platformDatePicker.MaxDate = datePicker.MaximumDate == null ? DateTimeOffset.MaxValue : datePicker.MaximumDate.Value.ToDateTime(new TimeOnly(0));
    }
}