# NPicker
This is a collection of native picker controls for .NET MAUI that allows nullable values.
- It supports Windows, Android, iOS and MacOS
- Based on the original source code of .NET MAUI it renders directly to native platforms the same way as the built-in controls found on .NET MAUI, but better cause it supports nullable values.

## Get Started

1) Install it from [![NuGet Version](https://img.shields.io/nuget/v/NPicker)](https://www.nuget.org/packages/NPicker)


2) Add the following namespace to your MauiProgram.cs:  
```using NPicker;```

3) Call `.UseNPicker()` on your AppBuilder as described bellow.
```csharp
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
            
        builder.UseMauiApp<App>()
               .UseNPicker();

        return builder.Build();
    }
}
```

Add the followin xmlns to the XAML pages where you wanna use it
```
xmlns:NPicker="clr-namespace:NPicker;assembly=NPicker"
```

Then add a reference to <NPicker:DatePicker/> to your view and use it just like you've been using before. With the difference that, now you can set null to it's `Date` property.

```xmls
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage x:Class="NPicker.Samples.MainPage"
             xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:NPicker="clr-namespace:NPicker;assembly=NPicker">

    <NPicker:DatePicker Value="{Binding MyDate}" Format="dd/MM/yyyy"/>

</ContentPage>
```
| Android  | iOS | Windows |
| ------------- | ------------- | ------------- |
| <img src="Docs/Android.gif" alt="Android demo.gif">  | <video width="300px" src="https://github.com/user-attachments/assets/224ed475-d357-48de-9b54-8ad0a91fc299" alt="iOS demo">  | <video src="https://private-user-images.githubusercontent.com/12549812/375948554-987dd0cd-8eb5-49d4-9936-5d123974cafb.mp4" alt="Windows.gif">  |


## Release Notes
Version 3.0 intruduced a breaking change: The bindable property `Date` was renamed to `Value` to better align with other set of controls on the ecosystem.
Version 2.0 intruduced a breaking change: The `Date`'s bindable property is now of type `DateOnly?` rather then `DateTime?` so please, make sure to change your VM's property to `DateOnly?`.


This library was created based on the original source code of [.NET MAUI](https://github.com/dotnet/maui).
