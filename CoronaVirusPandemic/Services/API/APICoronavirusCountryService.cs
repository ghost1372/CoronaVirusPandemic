using CoronaVirusPandemic.Models;
using CoronaVirusPandemic.Services.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoronaVirusPandemic.Services.API
{
    public class APICoronavirusCountryService : ICoronavirusCountryService
    {
        public async Task<IEnumerable<CoronavirusCountry>> GetTopCases()
        {
            using (HttpClient client = new HttpClient())
            {
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
    }
}
