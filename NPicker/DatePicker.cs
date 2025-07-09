namespace NPicker;

public class DatePicker : Entry, IDatePicker
{
    public event EventHandler<ValueChangedEventArguments>? ValueSelected;

    /// <summary>Bindable property for <see cref="Format"/>.</summary>
    public static readonly BindableProperty FormatProperty = BindableProperty.Create(nameof(Format), typeof(string), typeof(DatePicker), "d");

    /// <summary>Bindable property for <see cref="Value"/>.</summary>
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(DateOnly?), typeof(DatePicker), default(DateOnly?), BindingMode.TwoWay,
        coerceValue: CoerceValue,
        propertyChanged: ValuePropertyChanged,
        defaultValueCreator: (bindable) => default(DateOnly?));

    /// <summary>Bindable property for <see cref="MinimumValue"/>.</summary>
    public static readonly BindableProperty MinimumValueProperty = BindableProperty.Create(nameof(MinimumValue), typeof(DateOnly?), typeof(DatePicker), default(DateOnly?),
        validateValue: ValidateMinimumValue, coerceValue: CoerceMinimumValue);

    /// <summary>Bindable property for <see cref="MaximumValue"/>.</summary>
    public static readonly BindableProperty MaximumValueProperty = BindableProperty.Create(nameof(MaximumValue), typeof(DateOnly?), typeof(DatePicker), default(DateOnly?),
        validateValue: ValidateMaximumValue, coerceValue: CoerceMaximumValue);

    readonly Lazy<PlatformConfigurationRegistry<DatePicker>> _platformConfigurationRegistry;

    public DatePicker()
    {
        _platformConfigurationRegistry = new Lazy<PlatformConfigurationRegistry<DatePicker>>(() => new PlatformConfigurationRegistry<DatePicker>(this));
    }

    public string Format
    {
        get { return (string)GetValue(FormatProperty); }
        set { SetValue(FormatProperty, value); }
    }

    public DateOnly? Value
    {
        get { return (DateOnly?)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value); }
    }

    public DateOnly? MaximumValue
    {
        get { return (DateOnly?)GetValue(MaximumValueProperty); }
        set { SetValue(MaximumValueProperty, value); }
    }

    public DateOnly? MinimumValue
    {
        get { return (DateOnly?)GetValue(MinimumValueProperty); }
        set { SetValue(MinimumValueProperty, value); }
    }

    static object CoerceValue(BindableObject bindable, object value)
    {
        var picker = (DatePicker)bindable;
        DateOnly? dateValue = ((DateOnly?)value);

        if (dateValue != null && picker.MaximumValue != null && dateValue > picker.MaximumValue)
            dateValue = picker.MaximumValue;

        if (dateValue != null && picker.MaximumValue != null && dateValue < picker.MinimumValue)
            dateValue = picker.MinimumValue;

        return dateValue!;
    }

    static object CoerceMaximumValue(BindableObject bindable, object value)
    {
        var picker = (DatePicker)bindable;
        DateOnly? dateValue = ((DateOnly?)value);

        if (dateValue != null && picker.MaximumValue != null && picker.Value > dateValue)
            picker.Value = dateValue;

        return dateValue!;
    }

    static object CoerceMinimumValue(BindableObject bindable, object value)
    {
        var picker = (DatePicker)bindable;
        DateOnly? dateValue = ((DateOnly?)value);

        if (dateValue != null && picker.MaximumValue != null && picker.Value < dateValue)
            picker.Value = dateValue;

        return dateValue!;
    }

    static void ValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var datePicker = (DatePicker)bindable;
        EventHandler<ValueChangedEventArguments>? selected = datePicker.ValueSelected;

        if (selected != null)
            selected(datePicker, new ValueChangedEventArguments((DateOnly?)oldValue, (DateOnly?)newValue));
    }

    static bool ValidateMaximumValue(BindableObject bindable, object value)
    {
        DateOnly? dateValue = (DateOnly?)value;
        DateOnly? minimumDateValue = ((DatePicker)bindable).MinimumValue;

        if (dateValue == null || minimumDateValue == null)
        {
            return true;
        }

        return dateValue >= minimumDateValue;
    }

    static bool ValidateMinimumValue(BindableObject bindable, object value)
    {
        DateOnly? dateValue = (DateOnly?)value;
        DateOnly? maximumDateValue = ((DatePicker)bindable).MaximumValue;

        if (dateValue == null || maximumDateValue == null)
        {
            return true;
        }

        return dateValue <= maximumDateValue;
    }

    DateOnly? IDatePicker.Value
    {
        get => Value;
        set => SetValue(ValueProperty, value);
    }

    string IDatePicker.Format
    {
        get => Format;
        set => SetValue(FormatProperty, value);
    }
}