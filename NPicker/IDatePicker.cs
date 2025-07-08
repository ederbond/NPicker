using Microsoft.Maui;

namespace NPicker;

public interface IDatePicker : IView, ITextStyle, IPlaceholder
{
    /// <summary>
    /// Gets the format of the date to display to the user. 
    /// </summary>
    string Format { get; set; }

    /// <summary>
    /// Gets the displayed date.
    /// </summary>
    DateOnly? Value { get; set; }

    /// <summary>
    /// Gets the minimum DateTime selectable.
    /// </summary>
    DateOnly? MinimumValue { get; }

    /// <summary>
    /// Gets the maximum DateTime selectable.
    /// </summary>
    DateOnly? MaximumValue { get; }
}