﻿// <auto-generated />
using System;
using Dionys.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Dionys.Migrations
{
    [DbContext(typeof(DionysContext))]
    [Migration("20190725062938_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Dionys.Models.ConsumedProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ProductId");

                    b.Property<DateTime>("Timestamp");

                    b.Property<float>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ConsumedProducts");
                });

            modelBuilder.Entity("Dionys.Models.DTO.ProductDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Calories");

                    b.Property<float>("Carbohydrates");

                    b.Property<string>("Description");

                    b.Property<float>("Fat");

                    b.Property<string>("Name");

                    b.Property<float>("Protein");

                    b.HasKey("Id");

                    b.ToTable("ProductDTO");

                    b.HasData(
                        new
                        {
                            Id = new Guid("274684a2-d52b-4fb8-8bad-1f065ba76071"),
                            Calories = 24f,
                            Carbohydrates = 4.5f,
                            Description = "Баклажан как баклажан. На вкус как баклажан, на вид как баклажан. Ничего удивительного.",
                            Fat = 0.1f,
                            Name = "Баклажан",
                            Protein = 1.2f
                        },
                        new
                        {
                            Id = new Guid("274684a2-d52b-4fb8-8bad-1f065ba76072"),
                            Calories = 556f,
                            Carbohydrates = 58f,
                            Description = "Вкусная шоколадка. Жаль, что мало. Хотелось бы ещё. Обязательно надо закупать огромными партиями.",
                            Fat = 34f,
                            Name = "Alpen Gold. Молочный шоколад. Чернично-йогуртовая начинка, 90 г",
                            Protein = 3.9f
                        },
                        new
                        {
                            Id = new Guid("274684a2-d52b-4fb8-8bad-1f065ba76073"),
                            Calories = 160f,
                            Carbohydrates = 0f,
                            Description = "Цыплёнок как циплёнок. На вкус был как цыплёнок...",
                            Fat = 13f,
                            Name = "Сибирские колбасы. Окорочок цыплёнка-бройлера, 260 г",
                            Protein = 10f
                        },
                        new
                        {
                            Id = new Guid("274684a2-d52b-4fb8-8bad-1f065ba76074"),
                            Calories = 510f,
                            Carbohydrates = 66f,
                            Description = "Внешний вид напоминает крекеры. На упаковке написано \"крекеры\". Возможно крекеры.",
                            Fat = 24f,
                            Name = "Яшкино. Французский крекер с кунжутом, 185 г",
                            Protein = 8.5f
                        });
                });

            modelBuilder.Entity("Dionys.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Calories");

                    b.Property<float>("Carbohydrates");

                    b.Property<string>("Description");

                    b.Property<float>("Fat");

                    b.Property<string>("Name");

                    b.Property<float>("Protein");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Dionys.Models.ConsumedProduct", b =>
                {
                    b.HasOne("Dionys.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}
