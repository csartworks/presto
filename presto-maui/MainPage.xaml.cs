using Font = Microsoft.Maui.Font;

namespace presto_maui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        StackLayout stackLayout = new StackLayout
        {
            Padding = new Thickness(0, 20, 0, 0)
        };
        stackLayout.Add(new Label { FontFamily = "BravuraText",  Text = "\uE050", Margin = new Thickness(20) });
        stackLayout.Add(new Label { Text = ".NET iOS", Margin = new Thickness(10, 25) });
        stackLayout.Add(new Label { Text = ".NET Android", Margin = new Thickness(0, 20, 15, 5) });
        InitializeComponent();
    }
}

