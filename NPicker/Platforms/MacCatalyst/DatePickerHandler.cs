﻿using Foundation;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using NPicker.Platforms.MacCatalyst;

namespace NPicker;

public partial class DatePickerHandler : ViewHandler<IDatePicker, UIDatePicker>
{
    readonly UIDatePickerProxy _proxy = new();

    protected override UIDatePicker CreatePlatformView()
    {
        return new UIDatePicker { Mode = UIDatePickerMode.Date, TimeZone = new NSTimeZone("UTC") };
    }

    internal bool UpdateImmediately { get; set; } = true;

    protected override void ConnectHandler(UIDatePicker platformView)
    {
        _proxy.Connect(this, VirtualView, platformView);

        var date = VirtualView?.Value;
        if (date != null)
        {
            platformView.Date = date.Value.ToDateTime(new TimeOnly()).ToNSDate();
        }

        base.ConnectHandler(platformView);
    }

    protected override void DisconnectHandler(UIDatePicker platformView)
    {
        _proxy.Disconnect(platformView);

        base.DisconnectHandler(platformView);
    }

    public static partial void MapFormat(IDatePickerHandler handler, IDatePicker datePicker)
    {
        handler.PlatformView?.UpdateFormat(datePicker);
    }

    public static partial void MapValue(IDatePickerHandler handler, IDatePicker datePicker)
    {
        handler.PlatformView?.UpdateDate(datePicker);
    }

    public static partial void MapMinimumValue(IDatePickerHandler handler, IDatePicker datePicker)
    {
        handler.PlatformView?.UpdateMinimumDate(datePicker);
    }

    public static partial void MapMaximumValue(IDatePickerHandler handler, IDatePicker datePicker)
    {
        handler.PlatformView?.UpdateMaximumDate(datePicker);
    }

    public static partial void MapPlaceholder(IDatePickerHandler handler, IDatePicker datePicker)
    {
        //TODO: Implement it for MacOS
    }

    public static partial void MapPlaceholderColor(IDatePickerHandler handler, IDatePicker datePicker)
    {
        //TODO: Implement it for MacOS
    }

    public static partial void MapFontFamily(IDatePickerHandler handler, IDatePicker datePicker)
    {
        //TODO: Implement it for MacOS
    }

    public static partial void MapFontSize(IDatePickerHandler handler, IDatePicker datePicker)
    {
        //TODO: Implement it for MacOS
    }

    void SetVirtualViewValue()
    {
        if (VirtualView == null)
            return;

        VirtualView.Value = DateOnly.FromDateTime(PlatformView.Date.ToDateTime());
    }

    class UIDatePickerProxy
    {
        WeakReference<DatePickerHandler>? _handler;
        WeakReference<IDatePicker>? _virtualView;

        IDatePicker? VirtualView => _virtualView is not null && _virtualView.TryGetTarget(out var v) ? v : null;

        public void Connect(DatePickerHandler handler, IDatePicker virtualView, UIDatePicker platformView)
        {
            _handler = new(handler);
            _virtualView = new(virtualView);

            platformView.EditingDidBegin += OnStarted;
            platformView.EditingDidEnd += OnEnded;
            platformView.ValueChanged += OnValueChanged;
        }

        public void Disconnect(UIDatePicker platformView)
        {
            platformView.EditingDidBegin -= OnStarted;
            platformView.EditingDidEnd -= OnEnded;
            platformView.ValueChanged -= OnValueChanged;
        }

        void OnValueChanged(object? sender, EventArgs? e)
        {
            if (_handler is not null && _handler.TryGetTarget(out var handler) && handler.UpdateImmediately)
                handler.SetVirtualViewValue();

            if (VirtualView is IDatePicker virtualView)
                virtualView.IsFocused = true;
        }

        void OnStarted(object? sender, EventArgs eventArgs)
        {
            if (VirtualView is IDatePicker virtualView)
                virtualView.IsFocused = true;
        }

        void OnEnded(object? sender, EventArgs eventArgs)
        {
            if (VirtualView is IDatePicker virtualView)
                virtualView.IsFocused = false;
        }
    }
}