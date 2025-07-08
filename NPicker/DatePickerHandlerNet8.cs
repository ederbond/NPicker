using Microsoft.Maui.Handlers;

namespace NPicker;
#if !(ANDROID || IOS || WINDOWS || MACCATALYST)
    public partial class DatePickerHandler : ViewHandler<IDatePicker, object>
    {
        protected override object CreatePlatformView() => throw new NotImplementedException();

        public static partial void MapFormat(IDatePickerHandler handler, IDatePicker datePicker) { }
        public static partial void MapValue(IDatePickerHandler handler, IDatePicker datePicker) { }
        public static partial void MapMinimumValue(IDatePickerHandler handler, IDatePicker datePicker) { }
        public static partial void MapMaximumValue(IDatePickerHandler handler, IDatePicker datePicker) { }
        public static partial void MapPlaceholder(IDatePickerHandler handler, IDatePicker datePicker) { }
        public static partial void MapPlaceholderColor(IDatePickerHandler handler, IDatePicker datePicker) { }
    }
#endif
