using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriceListsInfo.Interfaces;
using PriceListsInfo.Models;
using PriceListsInfo.Services;

namespace PriceListsInfo.Controllers
{
    public class PriceListsController : Controller
    {
        private readonly IPriceList _priceList;
        private readonly IPriceListColumn _priceColumn;

        public PriceListsController(IPriceList priceList, IPriceListColumn priceColumn)
        {
            _priceList = priceList;
            _priceColumn = priceColumn;
        }

        public IActionResult ViewAllPriceLists()
        {
            var priceLists = _priceList.GetAll();

            return View(priceLists);
        }

        public IActionResult DetailsPriceList(int id)
        {
            var priceList = _priceList.GetById(id);

            if (priceList == null)
            {
                return NotFound();
            }

            return View(priceList);
        }

        [HttpGet]
        public IActionResult AddPosition(int priceListId)
        {
            var priceList = _priceList.GetById(priceListId);

            if (priceList == null)
            {
                return NotFound();
            }

            return View(priceList);
        }

        [HttpPost]
        public IActionResult AddPosition(int priceListId, int rowIndex, Dictionary<int, string> values)
        {
            if (ModelState.IsValid)
            {
                foreach (var keyValue in values)
                {
                    var value = new PriceListColumnValue
                    {
                        PriceListColumnId = keyValue.Key,
                        RowIndex = rowIndex,
                        Value = keyValue.Value
                    };
                    _priceList.AddPriceListColumnValue(value);
                }
                return RedirectToAction("DetailsPriceList", new { id = priceListId });
            }

            var priceList = _priceList.GetById(priceListId);

            return View(priceList);
        }
    }
}
