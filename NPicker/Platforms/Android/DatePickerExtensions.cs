using Android.App;

namespace NPicker.Platforms.Android;

public static class DatePickerExtensions
{
    public static void UpdateFormat(this MauiDatePicker platformDatePicker, IDatePicker datePicker)
    {
        platformDatePicker.SetText(datePicker);
    }

    public static void UpdateValue(this MauiDatePicker platformDatePicker, IDatePicker datePicker)
    {
        platformDatePicker.SetText(datePicker);
    }

    public static void UpdateMinimumValue(this MauiDatePicker platformDatePicker, IDatePicker datePicker)
    {
        platformDatePicker.UpdateMinimumValue(datePicker, null);
    }

    public static void UpdateMinimumValue(this MauiDatePicker platformDatePicker, IDatePicker datePicker, DatePickerDialog? datePickerDialog)
    {
        if (datePickerDialog != null && datePicker.MinimumValue != null)
        {
            datePickerDialog.DatePicker.MinDate = (long)datePicker.MinimumValue.Value.ToDateTime(new TimeOnly()).ToUniversalTime().Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;
        }
    }

    public static void UpdateMaximumValue(this MauiDatePicker platformDatePicker, IDatePicker datePicker)
    {
        platformDatePicker.UpdateMinimumValue(datePicker, null);
    }

    public static void UpdateMaximumValue(this MauiDatePicker platformDatePicker, IDatePicker datePicker, DatePickerDialog? datePickerDialog)
    {
        if (datePickerDialog != null && datePicker.MaximumValue != null)
        {
            datePickerDialog.DatePicker.MaxDate = (long)datePicker.MaximumValue.Value.ToDateTime(new TimeOnly()).ToUniversalTime().Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;
        }
    }

    internal static void SetText(this MauiDatePicker platformDatePicker, IDatePicker datePicker)
    {
        platformDatePicker.Text = datePicker.Value?.ToString(datePicker.Format);
    }
}