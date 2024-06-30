using Microsoft.EntityFrameworkCore;
using PriceListsInfo.DataBase;
using PriceListsInfo.Interfaces;
using PriceListsInfo.Models;

namespace PriceListsInfo.Services
{
    public class PriceListColumnService : IPriceListColumn
    {
        private readonly ApplicationDbContext _context;

        public PriceListColumnService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PriceListColumn> GetByPriceListId(int priceListId)
        {
            return _context.PriceListColumns.Where(c => c.PriceListId == priceListId).ToList();
        }
    }
}
