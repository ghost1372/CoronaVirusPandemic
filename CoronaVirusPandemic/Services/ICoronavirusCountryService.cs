using CoronaVirusPandemic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoronaVirusPandemic.Services
{
    public interface ICoronavirusCountryService
    {
        Task<IEnumerable<CoronavirusCountry>> GetTopCases();
    }
}
