using Microsoft.Maui.Platform;
using Microsoft.Maui.Graphics;
using Microsoft.UI.Xaml.Controls;
using WBrush = Microsoft.UI.Xaml.Media.Brush;

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

		public static void UpdateDate(this CalendarDatePicker platformDatePicker, DateTime? dateTime)
		{
			platformDatePicker.Date = dateTime;
		}

		public static void UpdateMinimumDate(this CalendarDatePicker platformDatePicker, IDatePicker datePicker)
		{
			platformDatePicker.MinDate = datePicker.MinimumDate ?? DateTimeOffset.MinValue;
		}

		public static void UpdateMaximumDate(this CalendarDatePicker platformDatePicker, IDatePicker datePicker)
		{
			platformDatePicker.MaxDate = datePicker.MinimumDate ?? DateTimeOffset.MaxValue;
		}
	}