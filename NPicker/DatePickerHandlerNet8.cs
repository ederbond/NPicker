using Microsoft.Maui.Handlers;

namespace NPicker;
#if !(ANDROID || IOS || WINDOWS || MACCATALYST)
    public partial class DatePickerHandler : ViewHandler<IDatePicker, object>
    {
        protected override object CreatePlatformView() => throw new NotImplementedException();

        public static partial void MapFormat(IDatePickerHandler handler, IDatePicker datePicker) { }
        public static partial void MapDate(IDatePickerHandler handler, IDatePicker datePicker) { }
        public static partial void MapMinimumDate(IDatePickerHandler handler, IDatePicker datePicker) { }
        public static partial void MapMaximumDate(IDatePickerHandler handler, IDatePicker datePicker) { }
    }
#endif