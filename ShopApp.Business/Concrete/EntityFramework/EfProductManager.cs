using ShopApp.Business.Abstract;
using ShopApp.Business.Validation.FluentValidation;
using ShopApp.Core.Aspects.Postsharp.ValidationAspects;
using ShopApp.Core.Validation.FluentValidation;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp.Business.Concrete.EntityFramework
{
    public class EfProductManager : IProductService
    {
        private IProductDal _productDal;

        public EfProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [FluentValidationAspect(typeof(ProductValidation))]
        public bool Create(Product entity)
        {
            if (string.IsNullOrEmpty(ValidatorTool.errorMessage))
            {
                _productDal.Create(entity);
                return true;
            }
            return false;
        }

        public void Delete(Product entity)
        {
            _productDal.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public Product GetById(int id)
        {
            return _productDal.GetById(id);
        }

        public Product GetByIdWithCategories(int id)
        {
            return _productDal.GetByIdWithCategories(id);
        }

        public int GetCountByCategory(string category)
        {
            return _productDal.GetCountByCategory(category);
        }

        public Product GetProductDetails(int id)
        {
            return _productDal.GetProductDetail(id);
        }

        public List<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            return _productDal.GetProductsByCategory(category, page, pageSize);
        }

        [FluentValidationAspect(typeof(ProductValidation))]
        public bool Update(Product entity)
        {
            if (string.IsNullOrEmpty(ValidatorTool.errorMessage))
            {
                _productDal.Update(entity);
                return true;
            }
            return false;
        }

        [FluentValidationAspect(typeof(ProductValidation))]
        public bool Update(Product entity, int[] categories)
        {
            if (string.IsNullOrEmpty(ValidatorTool.errorMessage))
            {
                _productDal.Update(entity, categories);
                return true;
            }
            return false;
        }
    }
}
