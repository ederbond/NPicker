using Foundation;
using Microsoft.Maui.Platform;
using System.Globalization;
using UIKit;

namespace NPicker.Platforms.MacCatalyst;

public static class DatePickerExtensions
	{
		public static void UpdateFormat(this UIDatePicker picker, IDatePicker datePicker)
		{
			picker.UpdateDate(datePicker);
		}

		public static void UpdateDate(this MauiDatePicker platformDatePicker, IDatePicker datePicker)
		{
			platformDatePicker.UpdateDate(datePicker, null);
		}

		public static void UpdateTextColor(this MauiDatePicker platformDatePicker, IDatePicker datePicker, UIColor? defaultTextColor)
		{
			var textColor = datePicker.TextColor;

			if (textColor == null)
			{
				if (defaultTextColor != null)
				{
					platformDatePicker.TextColor = defaultTextColor;
				}
			}
			else
			{
				platformDatePicker.TextColor = textColor.ToPlatform();
			}

			// HACK This forces the color to update; there's probably a more elegant way to make this happen
			platformDatePicker.UpdateDate(datePicker);
		}

		public static void UpdateDate(this UIDatePicker picker, IDatePicker datePicker)
		{
			if (picker != null && datePicker.Value != null && DateOnly.FromDateTime(picker.Date.ToDateTime()) != datePicker.Value.Value)
				picker.SetDate(datePicker.Value.Value.ToDateTime(new TimeOnly()).ToNSDate(), false);
		}

		public static void UpdateDate(this MauiDatePicker platformDatePicker, IDatePicker datePicker, UIDatePicker? picker)
		{
			if (picker != null && datePicker.Value != null  && DateOnly.FromDateTime(picker.Date.ToDateTime()) != datePicker.Value.Value)
				picker.SetDate(datePicker.Value.Value.ToDateTime(new TimeOnly()).ToNSDate(), false);

			string format = datePicker.Format ?? string.Empty;

			// Can't use VirtualView.Format because it won't display the correct format if the region and language are set differently
			if (picker != null && (string.IsNullOrWhiteSpace(format) || format.Equals("d", StringComparison.OrdinalIgnoreCase)))
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
			else if (datePicker.Value != null && format.Contains('/', StringComparison.Ordinal))
			{
				platformDatePicker.Text = datePicker.Value.Value.ToString(format, CultureInfo.InvariantCulture);
			}
			else if(datePicker.Value != null)
			{
				platformDatePicker.Text = datePicker.Value.Value.ToString(format);
			}

			platformDatePicker.UpdateCharacterSpacing(datePicker);
		}

		public static void UpdateMinimumDate(this MauiDatePicker platformDatePicker, IDatePicker datePicker, UIDatePicker? picker)
		{
			picker?.UpdateMinimumDate(datePicker);
		}

		public static void UpdateMinimumDate(this UIDatePicker platformDatePicker, IDatePicker datePicker)
		{
			if (platformDatePicker != null && datePicker.MinimumValue != null)
			{
				platformDatePicker.MinimumDate = datePicker.MinimumValue.Value.ToDateTime(new TimeOnly()).ToNSDate();
			}
		}

		public static void UpdateMaximumDate(this MauiDatePicker platformDatePicker, IDatePicker datePicker, UIDatePicker? picker)
		{
			picker?.UpdateMaximumDate(datePicker);
		}

		public static void UpdateMaximumDate(this UIDatePicker platformDatePicker, IDatePicker datePicker)
		{
			if (platformDatePicker != null && datePicker.MaximumValue != null)
			{
				platformDatePicker.MaximumDate = datePicker.MaximumValue.Value.ToDateTime(new TimeOnly()).ToNSDate();
			}
		}
	}