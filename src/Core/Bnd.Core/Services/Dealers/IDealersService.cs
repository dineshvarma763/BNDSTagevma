namespace Bnd.Core.Services.Dealers
{
    using Bnd.DTO.Models;
    public interface IDealersService
    {
        Task<IEnumerable<DealerDto>?> GetAllDealers(double lat, double lng, bool freeMeasureQuote, bool serviceOrRepair, bool commercial);
        Task<IEnumerable<DealerDto>?> GetMobileShowrooms(double lat, double lng, bool freeMeasureQuote, bool serviceOrRepair, bool commercial);
        IEnumerable<DealerDto>? SearchDealers(string searchTerm);
        IEnumerable<DealerDto>? SearchDealers(string searchTerm, double latitude, double longitude, bool freeMeasureQuote, bool serviceOrRepair, bool commercial);
        IEnumerable<string>? GetSuggestedDealers(string searchTerm);
    }
}
    