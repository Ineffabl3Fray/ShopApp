using ShopApp.Core.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Entities.Concrete
{
    public class ProductCategory: IEntity
    {
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
