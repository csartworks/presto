using presto;
namespace presto_maui;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
        Image image = new Image
        {
            Source = ImageSource.FromFile(@"..\untitled3.png")
                //Source = ImageSource.FromUri(new Uri("https://aka.ms/campus.jpg"))
        };
        Content = image;
        //InitializeComponent();
	}
    private void OnEditorTextChanged(object sender, TextChangedEventArgs e)
    {
        string newVal = e.NewTextValue;
    }

	private void OnEditorComplete(object sender, EventArgs e)
	{
        //string newVal = e.NewTextValue;
        string newVal = ((Editor)sender).Text;
        Presto.ToPng(newVal);
	}
    async void OnOpenScore(object sender, EventArgs e)
    {
        try
        {
            string filePath = Presto.pwd;
            filePath = Path.Combine(filePath, Presto.title + ".pdf");
            Uri uri = new Uri(filePath);
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            // An unexpected error occured. No browser may be installed on the device.
        }
    }
}

