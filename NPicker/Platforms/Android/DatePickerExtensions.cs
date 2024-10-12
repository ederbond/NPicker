using Android.App;

namespace NPicker.Platforms.Android;

public static class DatePickerExtensions
{
    public static void UpdateFormat(this MauiDatePicker platformDatePicker, IDatePicker datePicker)
    {
        platformDatePicker.SetText(datePicker);
    }

    public static void UpdateDate(this MauiDatePicker platformDatePicker, IDatePicker datePicker)
    {
        platformDatePicker.SetText(datePicker);
    }

    public static void UpdateMinimumDate(this MauiDatePicker platformDatePicker, IDatePicker datePicker)
    {
        platformDatePicker.UpdateMinimumDate(datePicker, null);
    }

    public static void UpdateMinimumDate(this MauiDatePicker platformDatePicker, IDatePicker datePicker, DatePickerDialog? datePickerDialog)
    {
        if (datePickerDialog != null && datePicker.MinimumDate != null)
        {
            datePickerDialog.DatePicker.MinDate = (long)datePicker.MinimumDate.Value.ToUniversalTime().Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;
        }
    }

    public static void UpdateMaximumDate(this MauiDatePicker platformDatePicker, IDatePicker datePicker)
    {
        platformDatePicker.UpdateMinimumDate(datePicker, null);
    }

    public static void UpdateMaximumDate(this MauiDatePicker platformDatePicker, IDatePicker datePicker, DatePickerDialog? datePickerDialog)
    {
        if (datePickerDialog != null && datePicker.MaximumDate != null)
        {
            datePickerDialog.DatePicker.MaxDate = (long)datePicker.MaximumDate.Value.ToUniversalTime().Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;
        }
    }

    internal static void SetText(this MauiDatePicker platformDatePicker, IDatePicker datePicker)
    {
        platformDatePicker.Text = datePicker.Date?.ToString(datePicker.Format);
    }
}