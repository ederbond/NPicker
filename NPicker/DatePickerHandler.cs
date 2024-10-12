﻿#if IOS && !MACCATALYST
using PlatformView = NPicker.Platforms.iOS.MauiDatePicker;
#elif MACCATALYST
using PlatformView = UIKit.UIDatePicker;
#elif MONOANDROID || ANDROID
using PlatformView = NPicker.Platforms.Android.MauiDatePicker;
#elif WINDOWS
using PlatformView = Microsoft.UI.Xaml.Controls.CalendarDatePicker;
#elif TIZEN
using PlatformView = Tizen.UIExtensions.NUI.Entry;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID && !TIZEN)
using PlatformView = System.Object;
#endif

namespace NPicker;
/// <summary>
/// Represents the view handler for the abstract <see cref="IDatePicker"/> view and its platform-specific implementation.
/// </summary>
/// <seealso href="https://learn.microsoft.com/dotnet/maui/user-interface/handlers/">Conceptual documentation on handlers</seealso>
public partial class DatePickerHandler : IDatePickerHandler
{
    public static IPropertyMapper<IDatePicker, IDatePickerHandler> Mapper = new PropertyMapper<IDatePicker, IDatePickerHandler>(Microsoft.Maui.Handlers.ViewHandler.ViewMapper)
    {
        [nameof(IDatePicker.Date)] = MapDate,
        [nameof(IDatePicker.Format)] = MapFormat,
        [nameof(IDatePicker.MaximumDate)] = MapMaximumDate,
        [nameof(IDatePicker.MinimumDate)] = MapMinimumDate,
    };

    public static CommandMapper<IPicker, IDatePickerHandler> CommandMapper = new(ViewCommandMapper)
    {
    };

    public DatePickerHandler() : base(Mapper, CommandMapper)
    {
    }

    public DatePickerHandler(IPropertyMapper? mapper)
        : base(mapper ?? Mapper, CommandMapper)
    {
    }

    public DatePickerHandler(IPropertyMapper? mapper, CommandMapper? commandMapper)
        : base(mapper ?? Mapper, commandMapper ?? CommandMapper)
    {
    }

    IDatePicker IDatePickerHandler.VirtualView => VirtualView;

    PlatformView IDatePickerHandler.PlatformView => PlatformView;

    /// <summary>
    /// Maps the abstract <see cref="IDatePicker.Format"/> property to the platform-specific implementations.
    /// </summary>
    /// <param name="handler">The associated handler.</param>
    /// <param name="datePicker">The associated <see cref="IDatePicker"/> instance.</param>
    public static partial void MapFormat(IDatePickerHandler handler, IDatePicker datePicker);

    /// <summary>
    /// Maps the abstract <see cref="IDatePicker.Date"/> property to the platform-specific implementations.
    /// </summary>
    /// <param name="handler">The associated handler.</param>
    /// <param name="datePicker">The associated <see cref="IDatePicker"/> instance.</param>
    public static partial void MapDate(IDatePickerHandler handler, IDatePicker datePicker);

    /// <summary>
    /// Maps the abstract <see cref="IDatePicker.MinimumDate"/> property to the platform-specific implementations.
    /// </summary>
    /// <param name="handler">The associated handler.</param>
    /// <param name="datePicker">The associated <see cref="IDatePicker"/> instance.</param>
    public static partial void MapMinimumDate(IDatePickerHandler handler, IDatePicker datePicker);

    /// <summary>
    /// Maps the abstract <see cref="IDatePicker.MaximumDate"/> property to the platform-specific implementations.
    /// </summary>
    /// <param name="handler">The associated handler.</param>
    /// <param name="datePicker">The associated <see cref="IDatePicker"/> instance.</param>
    public static partial void MapMaximumDate(IDatePickerHandler handler, IDatePicker datePicker);
}