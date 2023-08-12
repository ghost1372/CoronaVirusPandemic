using CoronaVirusPandemic.Models;
using System.Collections.ObjectModel;

using Microsoft.UI.Xaml.Media;
using System.Text.Json;
using CommunityToolkit.WinUI.UI;
using WinUICommunity;
using CommunityToolkit.Mvvm.Input;

namespace CoronaVirusPandemic.ViewModels;
public partial class MainViewModel : ObservableRecipient
{
    [ObservableProperty]
    public ObservableCollection<CoronavirusCountry> data;

    [ObservableProperty]
    public AdvancedCollectionView dataACV;

    [ObservableProperty]
    public string titleStatus;

    [ObservableProperty]
    public string messageStatus;

    public MainViewModel(IThemeService themeService)
    {
        themeService.Initialize(App.currentWindow);
        themeService.ConfigBackdrop();
        themeService.ConfigElementTheme();
        themeService.ConfigBackdropFallBackColorForWindow10(Application.Current.Resources["ApplicationPageBackgroundThemeBrush"] as Brush);
        OnRefresh();
    }

    [RelayCommand]
    private async Task OnRefresh()
    {
        IsActive = true;
        Data = new(await GetTopCases());
        DataACV = new AdvancedCollectionView(Data, true);
        TitleStatus = "Updated " + DateTime.Now;
        IsActive = false;
    }

    public async Task<IEnumerable<CoronavirusCountry>> GetTopCases()
    {
        using HttpClient client = new HttpClient();
        string requestUri = "https://corona.lmao.ninja/v3/covid-19/countries?sort=cases";

        HttpResponseMessage apiResponse = await client.GetAsync(requestUri);

        string jsonResponse = await apiResponse.Content.ReadAsStringAsync();

        List<APICoronavirusCountry> apiCountries = JsonSerializer.Deserialize<List<APICoronavirusCountry>>(jsonResponse, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });

        return apiCountries.Select(apiCountry => new CoronavirusCountry()
        {
            CountryName = apiCountry.Country,
            CaseCount = apiCountry.Cases,
            DeathsCount = apiCountry.Deaths,
            TodayDeathsCount = apiCountry.TodayDeaths,
            ActiveCount = apiCountry.Active,
            CriticalCount = apiCountry.Critical,
            RecoveredCount = apiCountry.Recovered,
            TodayCasesCount = apiCountry.TodayCases,
            FlagUri = apiCountry.CountryInfo.Flag
        });
    }
}
