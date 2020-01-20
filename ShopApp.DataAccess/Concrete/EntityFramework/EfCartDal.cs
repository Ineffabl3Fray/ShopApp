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
    public class EfCartDal : RepositoryBase<Cart, ShopContext>, ICartDal
    {
        public override void Update(Cart entity)
        {
            using (var context = new ShopContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            using (var context = new ShopContext())
            {
                return context.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(c => c.Product)
                    .FirstOrDefault(c => c.UserId == userId);
            }
        }

        public void DeleteFromCart(int cartId, int productId)
        {
            using (var context = new ShopContext())
            {
                var cmd = "delete from CartItem where CartId=@p0 and ProductId=@p1";
                context.Database.ExecuteSqlCommand(cmd, cartId, productId);
            }
        }
    }
}
