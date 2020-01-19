using ShopApp.Core.DataAccess.Concrete;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal: RepositoryBase<Order, ShopContext>, IOrderDal
    {
    }
}
