using System.Runtime.CompilerServices;
using Android.App;
using Microsoft.Maui.Handlers;
using NPicker.Platforms.Android;
using Microsoft.Maui.Platform;
using MauiDatePicker = NPicker.Platforms.Android.MauiDatePicker;

namespace NPicker;

public partial class DatePickerHandler : ViewHandler<IDatePicker, MauiDatePicker>
{
    DatePickerDialog? _dialog;

    protected override MauiDatePicker CreatePlatformView()
    {
        var mauiDatePicker = new MauiDatePicker(Context)
        {
            ShowPicker = ShowPickerDialog,
            HidePicker = HidePickerDialog
        };

        if (VirtualView == null)
            return mauiDatePicker;

        var date = VirtualView?.Value;

        if (date == null)
        {
            // I'm not 100% sure about this. @eder
            _dialog = CreateDatePickerDialog(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        }
        else
        {
            _dialog = CreateDatePickerDialog(date.Value.Year, date.Value.Month, date.Value.Day);
        }

        return mauiDatePicker;
    }

    internal DatePickerDialog? DatePickerDialog { get { return _dialog; } }

    protected override void ConnectHandler(MauiDatePicker platformView)
    {
        base.ConnectHandler(platformView);
        platformView.ViewAttachedToWindow += OnViewAttachedToWindow;
        platformView.ViewDetachedFromWindow += OnViewDetachedFromWindow;

        if (platformView.IsAttachedToWindow)
            OnViewAttachedToWindow();
    }

    void OnViewDetachedFromWindow(object? sender = null, global::Android.Views.View.ViewDetachedFromWindowEventArgs? e = null)
    {
        // I tested and this is called when an activity is destroyed
        DeviceDisplay.MainDisplayInfoChanged -= OnMainDisplayInfoChanged;
    }

    void OnViewAttachedToWindow(object? sender = null, global::Android.Views.View.ViewAttachedToWindowEventArgs? e = null)
    {
        DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
    }

    protected override void DisconnectHandler(MauiDatePicker platformView)
    {
        if (_dialog != null)
        {
            _dialog.Hide();
            _dialog.Dispose();
            _dialog = null;
        }

        platformView.ViewAttachedToWindow -= OnViewAttachedToWindow;
        platformView.ViewDetachedFromWindow -= OnViewDetachedFromWindow;
        OnViewDetachedFromWindow();

        base.DisconnectHandler(platformView);
    }

    protected virtual DatePickerDialog CreateDatePickerDialog(int year, int month, int day)
    {
        var dialog = new DatePickerDialog(Context!, (o, e) =>
        {
            if (VirtualView != null)
            {
                VirtualView.Value = DateOnly.FromDateTime(e.Date);
            }
        }, year, month, day);

        return dialog;
    }

    public static partial void MapFormat(IDatePickerHandler handler, IDatePicker datePicker)
    {
        handler.PlatformView?.UpdateFormat(datePicker);
    }

    public static partial void MapValue(IDatePickerHandler handler, IDatePicker datePicker)
    {
        handler.PlatformView?.UpdateValue(datePicker);
    }

    public static partial void MapMinimumValue(IDatePickerHandler handler, IDatePicker datePicker)
    {
        if (handler is DatePickerHandler platformHandler)
            handler.PlatformView?.UpdateMinimumValue(datePicker, platformHandler._dialog);
    }

    public static partial void MapMaximumValue(IDatePickerHandler handler, IDatePicker datePicker)
    {
        if (handler is DatePickerHandler platformHandler)
            handler.PlatformView?.UpdateMaximumValue(datePicker, platformHandler._dialog);
    }

    public static partial void MapPlaceholder(IDatePickerHandler handler, IDatePicker datePicker)
    {
        if (datePicker is IEntry entry)
            handler.PlatformView?.UpdatePlaceholder(entry);
    }

    public static partial void MapPlaceholderColor(IDatePickerHandler handler, IDatePicker datePicker)
    {
        if (datePicker is IEntry entry)
            handler.PlatformView?.UpdatePlaceholderColor(entry);
    }

    public static partial void MapFontFamily(IDatePickerHandler handler, IDatePicker datePicker)
    {
        MapFont(handler, datePicker);
    }

    public static partial void MapFontSize(IDatePickerHandler handler, IDatePicker datePicker)
    {
        MapFont(handler, datePicker);
    }

    public static void MapFont(IDatePickerHandler handler, IDatePicker datePicker)
    {
        if (datePicker is IEntry entry)
        {
            var fontManager = handler.GetRequiredService<IFontManager>();
            handler.PlatformView?.UpdateFont(entry, fontManager);
        }
    }

    void ShowPickerDialog()
    {
        if (VirtualView == null)
            return;

        if (_dialog != null && _dialog.IsShowing)
            return;

        var date = VirtualView.Value;

        if (date == null)
            ShowPickerDialog(DateTime.Today.Year, DateTime.Today.Month - 1, DateTime.Today.Day);
        else
            ShowPickerDialog(date.Value.Year, date.Value.Month - 1, date.Value.Day);
    }

    void ShowPickerDialog(int year, int month, int day)
    {
        if (_dialog == null)
            _dialog = CreateDatePickerDialog(year, month, day);
        else
        {
            EventHandler? setDateLater = null;
            setDateLater = (sender, e) =>
            {
                _dialog!.UpdateDate(year, month, day);
                _dialog.ShowEvent -= setDateLater;
            };
            _dialog.ShowEvent += setDateLater;
        }

        _dialog.Show();
    }

    void HidePickerDialog()
    {
        _dialog?.Hide();
    }

    void OnMainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
    {
        DatePickerDialog? currentDialog = _dialog;

        if (currentDialog != null && currentDialog.IsShowing)
        {
            currentDialog.Dismiss();

            ShowPickerDialog(currentDialog.DatePicker.Year, currentDialog.DatePicker.Month, currentDialog.DatePicker.DayOfMonth);
        }
    }
}