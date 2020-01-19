using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.Core.Validation.FluentValidation;
using ShopApp.Entities.Concrete;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult ProductList()
        {
            return View(new ProductListModel
            {
                Products = _productService.GetAll()
            });
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                    Price = model.Price,
                };
                if (_productService.Create(entity))
                {
                    return RedirectToAction("ProductList", "Admin");
                }
            }
            ViewBag.error = ValidatorTool.errorMessage;
            return View(model);
        }

        [HttpGet]
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _productService.GetByIdWithCategories((int)id);

            if (product == null)
            {
                return NotFound();
            }

            return View(new ProductModel
            {
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                SelectedCategories = product.ProductCategories.Select(c => c.Category).ToList(),
                Categories = _categoryService.GetAll()
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductModel model, int[] categories, IFormFile file)
        {
                var entity = _productService.GetById(model.Id);
            if (ModelState.IsValid)
            {

                if (entity == null)
                {
                    return NotFound();
                }

                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.Price = model.Price;

                if (file != null)
                {
                    Random rnd = new Random();
                    entity.ImageUrl = file.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", (model.Name+"-"+rnd.Next(100000000, 999999999)+file.FileName));
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                if (_productService.Update(entity, categories))
                {
                    return RedirectToAction("ProductList", "Admin");
                }
            }
            model.Categories = _categoryService.GetAll();
            return View(model);
        }

        public IActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetById((int)id);

            if (product == null)
            {
                return NotFound();
            }
            _productService.Delete(product);

            return RedirectToAction("ProductList", "Admin");
        }

        //---------------------------------//

        public IActionResult CategoryList()
        {
            return View(new CategoryListModel
            {
                Categories = _categoryService.GetAll()
            });
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new Category
                {
                    Name = model.Name
                };
                _categoryService.Create(entity);
                return RedirectToAction("CategoryList", "Admin");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _categoryService.GetByIdWithProducts((int)id);
            if (category == null)
            {
                return NotFound();
            }

            var entity = new CategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.ProductCategories.Select(c => c.Product).ToList()
            };
            return View(entity);
        }

        [HttpPost]
        public IActionResult EditCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _categoryService.GetById(model.Id);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Name = model.Name;
                _categoryService.Update(entity);
                return RedirectToAction("CategoryList", "Admin");
            }
            return View(model);
        }

        public IActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _categoryService.GetById((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            _categoryService.Delete(entity);
            return RedirectToAction("CategoryList", "Admin");
        }

        public IActionResult DeleteProductFromCategory(int productId, int categoryId)
        {
            _categoryService.DeleteProductFromCategory(productId, categoryId);
            return Redirect("/Admin/EditCategory/" + categoryId);
        }
    }
}