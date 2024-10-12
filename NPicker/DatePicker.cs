namespace NPicker;

public partial class DatePicker : Entry, IDatePicker
{
    /// <summary>Bindable property for <see cref="Format"/>.</summary>
    public static readonly BindableProperty FormatProperty = BindableProperty.Create(nameof(Format), typeof(string), typeof(DatePicker), "d");

    /// <summary>Bindable property for <see cref="Date"/>.</summary>
    public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime?), typeof(DatePicker), default(DateTime?), BindingMode.TwoWay,
        coerceValue: CoerceDate,
        propertyChanged: DatePropertyChanged,
        defaultValueCreator: (bindable) => default(DateTime?));

    /// <summary>Bindable property for <see cref="MinimumDate"/>.</summary>
    public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate), typeof(DateTime?), typeof(DatePicker), default(DateTime?),
        validateValue: ValidateMinimumDate, coerceValue: CoerceMinimumDate);

    /// <summary>Bindable property for <see cref="MaximumDate"/>.</summary>
    public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(nameof(MaximumDate), typeof(DateTime?), typeof(DatePicker), default(DateTime?),
        validateValue: ValidateMaximumDate, coerceValue: CoerceMaximumDate);

    readonly Lazy<PlatformConfigurationRegistry<DatePicker>> _platformConfigurationRegistry;

    public DatePicker()
    {
        _platformConfigurationRegistry = new Lazy<PlatformConfigurationRegistry<DatePicker>>(() => new PlatformConfigurationRegistry<DatePicker>(this));
    }

    public DateTime? Date
    {
        get { return (DateTime?)GetValue(DateProperty); }
        set { SetValue(DateProperty, value); }
    }

    public string Format
    {
        get { return (string)GetValue(FormatProperty); }
        set { SetValue(FormatProperty, value); }
    }

    public DateTime? MaximumDate
    {
        get { return (DateTime?)GetValue(MaximumDateProperty); }
        set { SetValue(MaximumDateProperty, value); }
    }

    public DateTime? MinimumDate
    {
        get { return (DateTime?)GetValue(MinimumDateProperty); }
        set { SetValue(MinimumDateProperty, value); }
    }

    static object CoerceDate(BindableObject bindable, object value)
    {
        var picker = (DatePicker)bindable;
        DateTime? dateValue = ((DateTime?)value)?.Date;

        if (dateValue != null && picker.MaximumDate != null && dateValue > picker.MaximumDate)
            dateValue = picker.MaximumDate;

        if (dateValue != null && picker.MaximumDate != null && dateValue < picker.MinimumDate)
            dateValue = picker.MinimumDate;

        return dateValue!;
    }

    static object CoerceMaximumDate(BindableObject bindable, object value)
    {
        var picker = (DatePicker)bindable;
        DateTime? dateValue = ((DateTime?)value)?.Date;

        if (dateValue != null && picker.MaximumDate != null && picker.Date > dateValue)
            picker.Date = dateValue;

        return dateValue!;
    }

    static object CoerceMinimumDate(BindableObject bindable, object value)
    {
        var picker = (DatePicker)bindable;
        DateTime? dateValue = ((DateTime?)value)?.Date;

        if (dateValue != null && picker.MaximumDate != null && picker.Date < dateValue)
            picker.Date = dateValue;

        return dateValue!;
    }

    public event EventHandler<DateChangedEventArguments> DateSelected;

    static void DatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var datePicker = (DatePicker)bindable;
        EventHandler<DateChangedEventArguments> selected = datePicker.DateSelected;

        if (selected != null)
            selected(datePicker, new DateChangedEventArguments((DateTime?)oldValue, (DateTime?)newValue));
    }

    static bool ValidateMaximumDate(BindableObject bindable, object value)
    {
        DateTime? dateValue = ((DateTime?)value)?.Date;
        DateTime? minimunDateValue = ((DatePicker)bindable).MinimumDate?.Date;

        if (dateValue == null || minimunDateValue == null)
        {
            return true;
        }

        return dateValue >= minimunDateValue;
    }

    static bool ValidateMinimumDate(BindableObject bindable, object value)
    {
        DateTime? dateValue = ((DateTime?)value)?.Date;
        DateTime? maximunDateValue = ((DatePicker)bindable).MaximumDate?.Date;

        if (dateValue == null || maximunDateValue == null)
        {
            return true;
        }

        return dateValue <= maximunDateValue;
    }

    DateTime? IDatePicker.Date
    {
        get => Date;
        set => SetValue(DateProperty, value);
    }

    string IDatePicker.Format
    {
        get => Format;
        set => SetValue(FormatProperty, value);
    }
}