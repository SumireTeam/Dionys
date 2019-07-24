using System;
using System.Collections.Generic;
using Dionys.Models;
using Dionys.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Dionys.Seeds
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Create products
            var products = new List<ProductDTO>
            {
                new ProductDTO
                {
                    Id         = new Guid("274684A2-D52B-4FB8-8BAD-1F065BA76071"),
                    Name       = "Баклажан",
                    Protein   =  1.2f, Fat = 0.1f, Carbohydrates = 4.5f,
                    Energy     = 24f,
                    Description = "Баклажан как баклажан. На вкус как баклажан, на вид как баклажан. Ничего удивительного."
                },
                new ProductDTO
                {
                    Id         = new Guid("274684A2-D52B-4FB8-8BAD-1F065BA76072"),
                    Name       = "Alpen Gold. Молочный шоколад. Чернично-йогуртовая начинка, 90 г",
                    Protein   =  3.90f, Fat = 34.00f, Carbohydrates = 58.00f,
                    Energy     = 556f,
                    Description = "Вкусная шоколадка. Жаль, что мало. Хотелось бы ещё. Обязательно надо закупать огромными партиями."
                },
                new ProductDTO
                {
                    Id         = new Guid("274684A2-D52B-4FB8-8BAD-1F065BA76073"),
                    Name       = "Сибирские колбасы. Окорочок цыплёнка-бройлера, 260 г",
                    Protein   =  10.00f, Fat = 13.00f, Carbohydrates = 00.00f,
                    Energy     = 160f,
                    Description = "Цыплёнок как циплёнок. На вкус был как цыплёнок..."
                },
                new ProductDTO
                {
                    Id         = new Guid("274684A2-D52B-4FB8-8BAD-1F065BA76074"),
                    Name       = "Яшкино. Французский крекер с кунжутом, 185 г",
                    Protein   =  8.50f, Fat = 24.00f, Carbohydrates = 66.00f,
                    Energy     = 510f,
                    Description = "Внешний вид напоминает крекеры. На упаковке написано \"крекеры\". Возможно крекеры."
                }
            };

            modelBuilder.Entity<ProductDTO>().HasData(products);
        }
    }
}
