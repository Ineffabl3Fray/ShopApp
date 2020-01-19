using Microsoft.EntityFrameworkCore;
using ShopApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp.DataAccess.Concrete.EntityFramework
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new ShopContext();
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Categories.Count() == 0)
                {
                    context.AddRange(Categories);
                }
                if (context.Products.Count() == 0)
                {
                    context.AddRange(Products);
                    context.AddRange(ProductCategory);
                }
                context.SaveChanges();
            }

        }

        private static Category[] Categories =
        {
            new Category{Name = "Telefon"},
            new Category{Name = "Bilgisayar"},
            new Category{Name = "Tv"},
            new Category{Name = "Beyaz esya"},
            new Category{Name = "Elektronik"},
        };

        private static Product[] Products =
        {
            new Product(){ Name="Samsung S5", Price=2000, ImageUrl="1.jpg", Description="<p>güzel telefon</p>"},
            new Product(){ Name="LG S6", Price=3000, ImageUrl="2.jpg", Description="<p>güzel telefon</p>"},
            new Product(){ Name="Sony S7", Price=4000, ImageUrl="3.jpg", Description="<p>güzel telefon</p>"},
            new Product(){ Name="Asus", Price=5000, ImageUrl="4.jpg", Description="<p>güzel Bilgisayar</p>"},
            new Product(){ Name="Canon TR$#", Price=6000, ImageUrl="5.jpg", Description="<p>Iyi camera</p>"},
            new Product(){ Name="IPhone 6", Price=4000, ImageUrl="6.jpg", Description="<p>güzel telefon</p>"},
            new Product(){ Name="Samsung Smart TV", Price=5000, ImageUrl="7.jpg", Description="<p>güzel TV</p>"}
        };


        private static ProductCategory[] ProductCategory =
        {
            new ProductCategory() { Product= Products[0],Category= Categories[0]},
            new ProductCategory() { Product= Products[0],Category= Categories[2]},
            new ProductCategory() { Product= Products[1],Category= Categories[0]},
            new ProductCategory() { Product= Products[1],Category= Categories[1]},
            new ProductCategory() { Product= Products[2],Category= Categories[0]},
            new ProductCategory() { Product= Products[2],Category= Categories[2]},
            new ProductCategory() { Product= Products[3],Category= Categories[3]}
        };
    }
}
