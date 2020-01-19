using ShopApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product GetById(int id);
        Product GetProductDetails(int id);
        List<Product> GetProductsByCategory(string category, int page, int pageSize);

        bool Create(Product entity);
        bool Update(Product entity);
        void Delete(Product entity);
        int GetCountByCategory(string category);
        Product GetByIdWithCategories(int id);
        bool Update(Product entity, int[] categories);
    }
}
