﻿using Android.Content;
using Android.Runtime;
using Android.Text;
using Android.Util;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.Graphics.Drawable;
using static Android.Views.View;

namespace NPicker.Platforms.Android;

public class MauiDatePicker : AppCompatEditText, IOnClickListener
{
    public MauiDatePicker(Context context) : base(context)
    {
        Initialize();
    }

    public MauiDatePicker(Context context, IAttributeSet? attrs) : base(context, attrs)
    {
        Initialize();
    }

    public MauiDatePicker(Context context, IAttributeSet? attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
    {
        Initialize();
    }

    protected MauiDatePicker(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
    {
    }

    public Action? ShowPicker { get; set; }
    public Action? HidePicker { get; set; }

    public void OnClick(global::Android.Views.View? v)
    {
        ShowPicker?.Invoke();
    }

    void Initialize()
    {
        if (Background != null)
            DrawableCompat.Wrap(Background);

        Focusable = true;
        FocusableInTouchMode = false;
        Clickable = true;
        InputType = InputTypes.Null;

        SetOnClickListener(this);
    }
}
