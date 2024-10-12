#if IOS && !MACCATALYST
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
public partial interface IDatePickerHandler : IViewHandler
{
    new IDatePicker VirtualView { get; }
    new PlatformView PlatformView { get; }
}