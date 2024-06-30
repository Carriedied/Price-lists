using Microsoft.AspNetCore.Mvc;
using PriceListsInfo.DataBase;
using PriceListsInfo.Interfaces;
using PriceListsInfo.Models;
using PriceListsInfo.Services;
using System.Diagnostics;

namespace PriceListsInfo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPriceList _priceListService;
        private readonly IPriceListColumn _priceListColumnService;

        public HomeController(ILogger<HomeController> logger, IPriceList priceList, IPriceListColumn priceListColumnService)
        {
            _logger = logger;
            _priceListService = priceList;
            _priceListColumnService = priceListColumnService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.AllPriceLists = _priceListService.GetAll();
            return View(new PriceList());
        }

        [HttpPost]
        public IActionResult Index(PriceList model)
        {
            _priceListService.AddPriceList(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SelectPriceList(int selectedPriceListId)
        {
            var selectedPriceList = _priceListService.GetById(selectedPriceListId);

            ViewBag.AllPriceLists = _priceListService.GetAll();
            ViewBag.SelectedPriceList = selectedPriceList;

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
