using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(string category, int page = 1)
        {
            const int pageSize = 3;
            return View(new ProductListModel
            {
                PageInfo = new PageInfo
                {
                    TotalItems = _productService.GetCountByCategory(category),
                    CurrenPage = page,
                    CurrentCategory = category,
                    ItemsPerPage = pageSize
                },
                Products = _productService.GetAll()
            });
        }
    }
}