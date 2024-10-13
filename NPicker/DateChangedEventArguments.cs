namespace NPicker;

public record DateChangedEventArguments(DateOnly? OldDate, DateOnly? NewDate);