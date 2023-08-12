using CoronaVirusPandemic.Models;

namespace CoronaVirusPandemic.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel { get; }
    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        this.InitializeComponent();
        appTitleBar.Window = App.currentWindow;
    }

    private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        if (ViewModel.Data != null && ViewModel.Data.Any())
        {
            ViewModel.DataACV.Filter = _ => true;
            ViewModel.DataACV.Filter = DataFilter;
        }
    }

    public bool DataFilter(object item)
    {
        var query = (CoronavirusCountry)item;
        var name = query.CountryName ?? "";

        return name.Contains(txtSearch.Text, StringComparison.OrdinalIgnoreCase);
    }
}

