using Reactive.Bindings;
using System.Reactive.Linq;

namespace NPicker.Samples;

public class MainPageViewModel
{
    public MainPageViewModel()
    {
        SelectedDate = new ReactiveProperty<DateOnly?>();

        SelectedDateAsString = SelectedDate
            .Select(x => $"Current Value: " + (x.HasValue ? x.Value.ToString("d") : "null"))
            .ToReactiveProperty();

        ClearCommand = new ReactiveCommand().WithSubscribe(ClearValue, (x)=>{ });
    }

    public ReactiveProperty<DateOnly?> SelectedDate { get; }
    public ReactiveProperty<string?> SelectedDateAsString { get; }
    public ReactiveCommand ClearCommand { get; }

    private void ClearValue()
    {
        SelectedDate.Value = null;
    }
}