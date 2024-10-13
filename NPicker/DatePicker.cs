namespace NPicker;

public partial class DatePicker : Entry, IDatePicker
{
    public event EventHandler<DateChangedEventArguments> DateSelected;

    /// <summary>Bindable property for <see cref="Format"/>.</summary>
    public static readonly BindableProperty FormatProperty = BindableProperty.Create(nameof(Format), typeof(string), typeof(DatePicker), "d");

    /// <summary>Bindable property for <see cref="Date"/>.</summary>
    public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateOnly?), typeof(DatePicker), default(DateOnly?), BindingMode.TwoWay,
        coerceValue: CoerceDate,
        propertyChanged: DatePropertyChanged,
        defaultValueCreator: (bindable) => default(DateOnly?));

    /// <summary>Bindable property for <see cref="MinimumDate"/>.</summary>
    public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate), typeof(DateOnly?), typeof(DatePicker), default(DateOnly?),
        validateValue: ValidateMinimumDate, coerceValue: CoerceMinimumDate);

    /// <summary>Bindable property for <see cref="MaximumDate"/>.</summary>
    public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(nameof(MaximumDate), typeof(DateOnly?), typeof(DatePicker), default(DateOnly?),
        validateValue: ValidateMaximumDate, coerceValue: CoerceMaximumDate);

    readonly Lazy<PlatformConfigurationRegistry<DatePicker>> _platformConfigurationRegistry;

    public DatePicker()
    {
        _platformConfigurationRegistry = new Lazy<PlatformConfigurationRegistry<DatePicker>>(() => new PlatformConfigurationRegistry<DatePicker>(this));
    }

    public DateOnly? Date
    {
        get { return (DateOnly?)GetValue(DateProperty); }
        set { SetValue(DateProperty, value); }
    }

    public string Format
    {
        get { return (string)GetValue(FormatProperty); }
        set { SetValue(FormatProperty, value); }
    }

    public DateOnly? MaximumDate
    {
        get { return (DateOnly?)GetValue(MaximumDateProperty); }
        set { SetValue(MaximumDateProperty, value); }
    }

    public DateOnly? MinimumDate
    {
        get { return (DateOnly?)GetValue(MinimumDateProperty); }
        set { SetValue(MinimumDateProperty, value); }
    }

    static object CoerceDate(BindableObject bindable, object value)
    {
        var picker = (DatePicker)bindable;
        DateOnly? dateValue = ((DateOnly?)value);

        if (dateValue != null && picker.MaximumDate != null && dateValue > picker.MaximumDate)
            dateValue = picker.MaximumDate;

        if (dateValue != null && picker.MaximumDate != null && dateValue < picker.MinimumDate)
            dateValue = picker.MinimumDate;

        return dateValue!;
    }

    static object CoerceMaximumDate(BindableObject bindable, object value)
    {
        var picker = (DatePicker)bindable;
        DateOnly? dateValue = ((DateOnly?)value);

        if (dateValue != null && picker.MaximumDate != null && picker.Date > dateValue)
            picker.Date = dateValue;

        return dateValue!;
    }

    static object CoerceMinimumDate(BindableObject bindable, object value)
    {
        var picker = (DatePicker)bindable;
        DateOnly? dateValue = ((DateOnly?)value);

        if (dateValue != null && picker.MaximumDate != null && picker.Date < dateValue)
            picker.Date = dateValue;

        return dateValue!;
    }

    static void DatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var datePicker = (DatePicker)bindable;
        EventHandler<DateChangedEventArguments> selected = datePicker.DateSelected;

        if (selected != null)
            selected(datePicker, new DateChangedEventArguments((DateOnly?)oldValue, (DateOnly?)newValue));
    }

    static bool ValidateMaximumDate(BindableObject bindable, object value)
    {
        DateOnly? dateValue = ((DateOnly?)value);
        DateOnly? minimunDateValue = ((DatePicker)bindable).MinimumDate;

        if (dateValue == null || minimunDateValue == null)
        {
            return true;
        }

        return dateValue >= minimunDateValue;
    }

    static bool ValidateMinimumDate(BindableObject bindable, object value)
    {
        DateOnly? dateValue = ((DateOnly?)value);
        DateOnly? maximunDateValue = ((DatePicker)bindable).MaximumDate;

        if (dateValue == null || maximunDateValue == null)
        {
            return true;
        }

        return dateValue <= maximunDateValue;
    }

    DateOnly? IDatePicker.Date
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