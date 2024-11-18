using Foundation;
using Microsoft.Maui.Platform;
using System.Globalization;
using UIKit;

namespace NPicker.Platforms.iOS;

public static class DatePickerExtensions
{
    public static void UpdateFormat(this MauiDatePicker platformDatePicker, IDatePicker datePicker, UIDatePicker? picker)
    {
        platformDatePicker.UpdateValue(datePicker, picker);
    }

    public static void UpdateValue(this MauiDatePicker platformDatePicker, IDatePicker datePicker, UIDatePicker? picker)
    {
        if (picker != null && datePicker.Value != null && DateOnly.FromDateTime(picker.Date.ToDateTime()) != datePicker.Value.Value)
            picker.SetDate(datePicker.Value.Value.ToDateTime(new TimeOnly()).ToNSDate(), false);

        string format = datePicker.Format ?? string.Empty;

        // Can't use VirtualView.Format because it won't display the correct format if the region and language are set differently
        if (datePicker.Value == null)
        {
            platformDatePicker.Text = null;
        }
        else if (picker != null && (string.IsNullOrWhiteSpace(format) || format.Equals("d", StringComparison.OrdinalIgnoreCase)))
        {
            NSDateFormatter dateFormatter = new NSDateFormatter
            {
                TimeZone = NSTimeZone.FromGMT(0)
            };

            if (format.Equals("D", StringComparison.Ordinal) == true)
            {
                dateFormatter.DateStyle = NSDateFormatterStyle.Long;
                var strDate = dateFormatter.StringFor(picker.Date);
                platformDatePicker.Text = strDate;
            }
            else
            {
                dateFormatter.DateStyle = NSDateFormatterStyle.Short;
                var strDate = dateFormatter.StringFor(picker.Date);
                platformDatePicker.Text = strDate;
            }
        }
        else if (format.Contains('/', StringComparison.Ordinal))
        {
            platformDatePicker.Text = datePicker.Value.Value.ToString(format, CultureInfo.InvariantCulture);
        }
        else
        {
            platformDatePicker.Text = datePicker.Value.Value.ToString(format);
        }

        platformDatePicker.UpdateCharacterSpacing(datePicker);
    }

    public static void UpdateMinimumValue(this MauiDatePicker platformDatePicker, IDatePicker datePicker, UIDatePicker? picker)
    {
        picker?.UpdateMinimumValue(datePicker);
    }

    public static void UpdateMinimumValue(this UIDatePicker platformDatePicker, IDatePicker datePicker)
    {
        if (platformDatePicker != null && datePicker.MinimumValue != null)
        {
            platformDatePicker.MinimumDate = datePicker.MinimumValue.Value.ToDateTime(new TimeOnly()).ToNSDate();
        }
    }

    public static void UpdateMaximumValue(this MauiDatePicker platformDatePicker, IDatePicker datePicker, UIDatePicker? picker)
    {
        picker?.UpdateMaximumValue(datePicker);
    }

    public static void UpdateMaximumValue(this UIDatePicker platformDatePicker, IDatePicker datePicker)
    {
        if (platformDatePicker != null && datePicker.MaximumValue != null)
        {
            platformDatePicker.MaximumDate = datePicker.MaximumValue.Value.ToDateTime(new TimeOnly()).ToNSDate();
        }
    }
}