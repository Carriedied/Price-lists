using PriceListsInfo.Models;

namespace PriceListsInfo.Interfaces
{
    public interface IPriceListColumn
    {
        IEnumerable<PriceListColumn> GetByPriceListId(int priceListId);
    }
}
