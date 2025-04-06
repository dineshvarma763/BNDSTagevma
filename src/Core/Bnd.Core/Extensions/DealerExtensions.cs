namespace Bnd.Core.Extensions
{
    using Bnd.DTO.Models;
    public static class DealerExtensions
    {
        public static IEnumerable<DealerDto> OrderByDealer(this IEnumerable<DealerDto> dealers, string searchTerm)
        {
            if(dealers.Any())
            {
                dealers = dealers.OrderByDescending(d => d.Address.ToLower().Contains(searchTerm.ToLower()));
            }

            return dealers;
        }
    }
}
