using Foundation;
using Microsoft.Maui.Platform;
using System.Globalization;
using UIKit;

namespace NPicker.Platforms.iOS;

public static class DatePickerExtensions
{
    public static void UpdateFormat(this MauiDatePicker platformDatePicker, IDatePicker datePicker, UIDatePicker? picker)
    {
        platformDatePicker.UpdateDate(datePicker, picker);
    }

    public static void UpdateDate(this MauiDatePicker platformDatePicker, IDatePicker datePicker, UIDatePicker? picker)
    {
        if (picker != null && datePicker.Date != null && DateOnly.FromDateTime(picker.Date.ToDateTime()) != datePicker.Date.Value)
            picker.SetDate(datePicker.Date.Value.ToDateTime(new TimeOnly()).ToNSDate(), false);

        string format = datePicker.Format ?? string.Empty;

        // Can't use VirtualView.Format because it won't display the correct format if the region and language are set differently
        if (datePicker.Date == null)
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
            platformDatePicker.Text = datePicker.Date.Value.ToString(format, CultureInfo.InvariantCulture);
        }
        else
        {
            platformDatePicker.Text = datePicker.Date.Value.ToString(format);
        }

        platformDatePicker.UpdateCharacterSpacing(datePicker);
    }

    public static void UpdateMinimumDate(this MauiDatePicker platformDatePicker, IDatePicker datePicker, UIDatePicker? picker)
    {
        picker?.UpdateMinimumDate(datePicker);
    }

    public static void UpdateMinimumDate(this UIDatePicker platformDatePicker, IDatePicker datePicker)
    {
        if (platformDatePicker != null && datePicker.MinimumDate != null)
        {
            platformDatePicker.MinimumDate = datePicker.MinimumDate.Value.ToDateTime(new TimeOnly()).ToNSDate();
        }
    }

    public static void UpdateMaximumDate(this MauiDatePicker platformDatePicker, IDatePicker datePicker, UIDatePicker? picker)
    {
        picker?.UpdateMaximumDate(datePicker);
    }

    public static void UpdateMaximumDate(this UIDatePicker platformDatePicker, IDatePicker datePicker)
    {
        if (platformDatePicker != null && datePicker.MaximumDate != null)
        {
            platformDatePicker.MaximumDate = datePicker.MaximumDate.Value.ToDateTime(new TimeOnly()).ToNSDate();
        }
    }
}