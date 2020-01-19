using Microsoft.EntityFrameworkCore;
using ShopApp.Core.DataAccess.Concrete;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp.DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : RepositoryBase<Product, ShopContext>, IProductDal
    {
        public Product GetByIdWithCategories(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Products
                    .Include(c => c.ProductCategories)
                    .ThenInclude(c => c.Category)
                    .FirstOrDefault(c => c.Id == id);
            }
        }

        public int GetCountByCategory(string category)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    products = products.Include(c => c.ProductCategories)
                        .ThenInclude(c => c.Category)
                        .Where(c => c.ProductCategories.Any(a => a.Category.Name.ToLower() == category.ToLower()));
                }
                return products.Count();
            }
        }

        public IEnumerable<Product> GetPopularProducts()
        {
            throw new NotImplementedException();
        }

        public Product GetProductDetail(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Products
                    .Where(c => c.Id == id)
                    .Include(c => c.ProductCategories)
                    .ThenInclude(c => c.Category)
                    .FirstOrDefault();
            }
        }

        public List<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    products = products.Include(c => c.ProductCategories)
                        .ThenInclude(c => c.Category)
                        .Where(c => c.ProductCategories.Any(a => a.Category.Name.ToLower() == category.ToLower()));
                }
                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public void Update(Product entity, int[] categories)
        {
            using (var context = new ShopContext())
            {
                var product = context.Products.Include(c => c.ProductCategories)
                    .FirstOrDefault(c => c.Id == entity.Id);

                if (product != null)
                {
                    product.Name = entity.Name;
                    product.Price = entity.Price;
                    product.ImageUrl = entity.ImageUrl;
                    product.Description = entity.Description;
                    product.ProductCategories = categories.Select(c => new ProductCategory
                    {
                        CategoryId = c,
                         ProductId = entity.Id
                    }).ToList();
                    context.SaveChanges();
                }
            }
        }
    }
}
