using Microsoft.Maui.Platform;
using NPicker.Platforms.iOS;
using UIKit;
using MauiDatePicker = NPicker.Platforms.iOS.MauiDatePicker;

namespace NPicker;

#if !MACCATALYST
public partial class DatePickerHandler : Microsoft.Maui.Handlers.ViewHandler<IDatePicker, MauiDatePicker>
{
    protected override MauiDatePicker CreatePlatformView()
    {
        MauiDatePicker platformDatePicker = new MauiDatePicker();
        return platformDatePicker;
    }

    internal UIDatePicker? DatePickerDialog { get { return PlatformView?.InputView as UIDatePicker; } }

    internal bool UpdateImmediately { get; set; }

    protected override void ConnectHandler(MauiDatePicker platformView)
    {
        platformView.MauiDatePickerDelegate = new DatePickerDelegate(this);

        if (DatePickerDialog is UIDatePicker picker)
        {
            var date = VirtualView?.Value;
            if (date != null)
            {
                picker.Date = date.Value.ToDateTime(new TimeOnly()).ToNSDate();
            }
        }

        base.ConnectHandler(platformView);
    }

    protected override void DisconnectHandler(MauiDatePicker platformView)
    {
        platformView.MauiDatePickerDelegate = null;

        base.DisconnectHandler(platformView);
    }

    public static partial void MapFormat(IDatePickerHandler handler, IDatePicker datePicker)
    {
        var picker = (handler as DatePickerHandler)?.DatePickerDialog;
        handler.PlatformView?.UpdateFormat(datePicker, picker);
    }

    public static partial void MapValue(IDatePickerHandler handler, IDatePicker datePicker)
    {
        var picker = (handler as DatePickerHandler)?.DatePickerDialog;
        handler.PlatformView?.UpdateValue(datePicker, picker);
    }

    public static partial void MapMinimumValue(IDatePickerHandler handler, IDatePicker datePicker)
    {
        if (handler is DatePickerHandler platformHandler)
            handler.PlatformView?.UpdateMinimumValue(datePicker, platformHandler.DatePickerDialog);
    }

    public static partial void MapMaximumValue(IDatePickerHandler handler, IDatePicker datePicker)
    {
        if (handler is DatePickerHandler platformHandler)
            handler.PlatformView?.UpdateMaximumValue(datePicker, platformHandler.DatePickerDialog);
    }

    public static partial void MapPlaceholder(IDatePickerHandler handler, IDatePicker datePicker)
    {
        if (handler is DatePickerHandler platformHandler && datePicker is IEntry placeholder)
            handler.PlatformView?.UpdatePlaceholder(placeholder);
    }

    public static partial void MapPlaceholderColor(IDatePickerHandler handler, IDatePicker datePicker)
    {
        if (handler is DatePickerHandler platformHandler && datePicker is IEntry placeholder)
            handler.PlatformView?.UpdatePlaceholder(placeholder, datePicker.PlaceholderColor);
    }

    static void OnValueChanged(object? sender)
    {
        if (sender is DatePickerHandler datePickerHandler)
        {
            if (datePickerHandler.UpdateImmediately)  // Platform Specific
                datePickerHandler.SetVirtualViewDate();

            if (datePickerHandler.VirtualView != null)
                datePickerHandler.VirtualView.IsFocused = true;
        }
    }

    static void OnStarted(object? sender)
    {
        if (sender is IDatePickerHandler datePickerHandler && datePickerHandler.VirtualView != null)
            datePickerHandler.VirtualView.IsFocused = true;
    }

    static void OnEnded(object? sender)
    {
        if (sender is IDatePickerHandler datePickerHandler && datePickerHandler.VirtualView != null)
            datePickerHandler.VirtualView.IsFocused = false;
    }

    static void OnDoneClicked(object? sender)
    {
        if (sender is DatePickerHandler handler)
        {
            handler.SetVirtualViewDate();
            handler.PlatformView.ResignFirstResponder();
        }
    }

    static void OnCancelClicked(object? sender)
    {
        if (sender is DatePickerHandler handler)
        {
            handler.PlatformView.ResignFirstResponder();
        }
    }

    void SetVirtualViewDate()
    {
        if (VirtualView == null || DatePickerDialog == null)
            return;

        VirtualView.Value = DateOnly.FromDateTime(DatePickerDialog.Date.ToDateTime());
    }

    class DatePickerDelegate : NPicker.Platforms.iOS.MauiDatePickerDelegate
    {
        readonly WeakReference<IDatePickerHandler> _handler;

        public DatePickerDelegate(IDatePickerHandler handler) =>
            _handler = new WeakReference<IDatePickerHandler>(handler);

        IDatePickerHandler? Handler
        {
            get
            {
                if (_handler?.TryGetTarget(out IDatePickerHandler? target) == true)
                    return target;

                return null;
            }
        }

        public override void DatePickerEditingDidBegin()
        {
            DatePickerHandler.OnStarted(Handler);
        }

        public override void DatePickerEditingDidEnd()
        {
            DatePickerHandler.OnEnded(Handler);
        }

        public override void DatePickerValueChanged()
        {
            DatePickerHandler.OnValueChanged(Handler);
        }

        public override void DoneClicked()
        {
            DatePickerHandler.OnDoneClicked(Handler);
        }

        public override void CancelClicked()
        {
            DatePickerHandler.OnCancelClicked(Handler);
        }
    }
}
#endif
