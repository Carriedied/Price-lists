using PriceListsInfo.Models;

namespace PriceListsInfo.Interfaces
{
    public interface IPriceList
    {
        void AddPriceList(PriceList priceList);
        IEnumerable<PriceList> GetAll();
        PriceList GetById(int id);
        void AddPriceListColumnValue(PriceListColumnValue value);
        IEnumerable<PriceList> GetAllPriceLists();
        IEnumerable<PriceListColumn> GetColumnsByPriceListId(int priceListId);
    }
}
