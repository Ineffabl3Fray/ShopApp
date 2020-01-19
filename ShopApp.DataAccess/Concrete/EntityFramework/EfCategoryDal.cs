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
    public class EfCategoryDal : RepositoryBase<Category, ShopContext>, ICategoryDal
    {
        public void DeleteProductFromCategory(int productId, int categoryId)
        {
            using (var context = new ShopContext())
            {
                var cmd = "delete from ProductCategory where CategoryId=@p0 and ProductId=@p1";
                context.Database.ExecuteSqlCommand(cmd, categoryId, productId);
            }
        }

        public Category GetByIdWithProducts(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Categories
                        .Include(i => i.ProductCategories)
                        .ThenInclude(i => i.Product)
                        .FirstOrDefault(c => c.Id == id);
            }
        }
    }
}
