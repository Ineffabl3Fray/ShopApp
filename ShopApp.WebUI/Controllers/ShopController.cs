using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.Entities.Concrete;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public ShopController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List(string category, int page = 1)
        {
            const int pageSize = 3;
            var products = _productService.GetProductsByCategory(category, page, pageSize);
            return View(new ProductListModel
            {
                PageInfo = new PageInfo
                {
                    TotalItems = _productService.GetCountByCategory(category),
                     CurrenPage = page,
                      CurrentCategory = category,
                       ItemsPerPage = pageSize
                },
                Products = products
            });
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = _productService.GetProductDetails((int)id);
            ProductDetailsModel model = new ProductDetailsModel
            {
                Product = product,
                 Categories = product.ProductCategories.Select(c=> c.Category).ToList()
            };

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
    }
}