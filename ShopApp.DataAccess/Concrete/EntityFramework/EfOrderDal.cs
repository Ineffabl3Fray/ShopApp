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
    public class EfOrderDal : RepositoryBase<Order, ShopContext>, IOrderDal
    {
        public List<Order> GetOrders(string userId)
        {
            using (var context = new ShopContext())
            {
                var orders = context.Orders.Include(c => c.OrderItems)
                    .ThenInclude(c => c.Product)
                    .AsQueryable();
                if (!string.IsNullOrEmpty(userId))
                {
                    orders.Where(c => c.UserId == userId).ToList();
                }
                return orders.ToList(); 
            }
        }
    }
}
