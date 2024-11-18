namespace NPicker;

public record ValueChangedEventArguments(DateOnly? OldValue, DateOnly? NewValue);