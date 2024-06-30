using Microsoft.EntityFrameworkCore;
using PriceListsInfo.DataBase;
using PriceListsInfo.Interfaces;
using PriceListsInfo.Models;
using System.Collections.Generic;

namespace PriceListsInfo.Services
{
    public class PriceListService : IPriceList
    {
        private readonly ApplicationDbContext _context;

        public PriceListService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddPriceList(PriceList priceList)
        {
            priceList.Columns.Add(new PriceListColumn
            {
                ColumnName = "Название товара",
                ColumnType = ColumnType.SingleLineText
            });

            priceList.Columns.Add(new PriceListColumn
            {
                ColumnName = "Код товара",
                ColumnType = ColumnType.Number
            });

            _context.PriceLists.Add(priceList);
            _context.SaveChanges();
        }

        public IEnumerable<PriceList> GetAll()
        {
            return _context.PriceLists.ToList();
        }

        public PriceList GetById(int id)
        {
            return _context.PriceLists
                   .Include(pl => pl.Columns)
                   .ThenInclude(c => c.Values)
                   .FirstOrDefault(pl => pl.Id == id);
        }

        public void AddPriceListColumnValue(PriceListColumnValue value)
        {
            _context.PriceListColumnValues.Add(value);
            _context.SaveChanges();
        }

        public IEnumerable<PriceList> GetAllPriceLists()
        {
            return _context.PriceLists.ToList();
        }

        public IEnumerable<PriceListColumn> GetColumnsByPriceListId(int priceListId)
        {
            return _context.PriceListColumns
                .Where(c => c.PriceListId == priceListId)
                .ToList();
        }
    }
}
