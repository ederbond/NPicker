#if IOS && !MACCATALYST
using System;
using CoreGraphics;
using UIKit;

namespace NPicker.Platforms.iOS
{
	internal class MauiDoneAccessoryView : UIToolbar
	{
		readonly BarButtonItemProxy _proxy;

		public MauiDoneAccessoryView() : base(new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, 44))
		{
			_proxy = new BarButtonItemProxy();
			BarStyle = UIBarStyle.Default;
			Translucent = true;
			var cancel = new UIBarButtonItem(UIBarButtonSystemItem.Cancel, _proxy.OnCancelClicked);
			var spacer = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
			var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, _proxy.OnDoneClicked);

			SetItems(new[] { cancel, spacer, doneButton }, false);
		}

		internal void SetDoneClicked(Action<object>? value) => _proxy.SetDoneClicked(value);

		internal void SetCancelClicked(Action<object>? value) => _proxy.SetCancelClicked(value);

		internal void SetDataContext(object? dataContext) => _proxy.SetDataContext(dataContext);

		class BarButtonItemProxy
		{
			readonly Action? _doneClicked;
			Action<object>? _doneWithDataClicked;
			Action<object>? _cancelWithDataClicked;

			WeakReference<object>? _data;

			public BarButtonItemProxy() { }

			public BarButtonItemProxy(Action doneClicked)
			{
				_doneClicked = doneClicked;
			}

			public void SetDoneClicked(Action<object>? value) => _doneWithDataClicked = value;
			public void SetCancelClicked(Action<object>? value) => _cancelWithDataClicked = value;

			public void SetDataContext(object? dataContext) => _data = dataContext is null ? null : new(dataContext);

            public void OnDoneClicked(object? sender, EventArgs e)
			{
				if (_data is not null && _data.TryGetTarget(out var data))
					_doneWithDataClicked?.Invoke(data);
			}

			public void OnCancelClicked(object? sender, EventArgs e)
			{
				if (_data is not null && _data.TryGetTarget(out var data))
					_cancelWithDataClicked?.Invoke(data);
			}

			public void OnClicked(object? sender, EventArgs e) => _doneClicked?.Invoke();
		}
	}
}
#endif