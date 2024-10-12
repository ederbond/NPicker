namespace NPicker;

public interface IDatePicker : IView, ITextStyle
	{
		/// <summary>
		/// Gets the format of the date to display to the user. 
		/// </summary>
		string Format { get; set; }

		/// <summary>
		/// Gets the displayed date.
		/// </summary>
		DateTime? Date { get; set; }

		/// <summary>
		/// Gets the minimum DateTime selectable.
		/// </summary>
		DateTime? MinimumDate { get; }

		/// <summary>
		/// Gets the maximum DateTime selectable.
		/// </summary>
		DateTime? MaximumDate { get; }
	}