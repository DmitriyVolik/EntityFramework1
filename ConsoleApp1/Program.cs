using entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace entity
{
    public class Context : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public Context()
        {
            //Доводит базу до последней миграции - создает если ее нет
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //@"Data Source=.\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=True"
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Entity test;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // использование Fluent API
            base.OnModelCreating(modelBuilder);

            //Уникальный индекс на поле
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Title)
                .IsUnique();

            //Уникальный индекс на поле
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Title)
                .IsUnique();
        }
    }

    [Table("Product")] //Перегрузка имени таблицы
    public class Product
    {
        public int Id { get; set; }
        [Column("Title"), Required, StringLength(50)] //Перегрузка имени колонки и его ограничение
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Category"), Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

    [Table("Category")]
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Title { get; set; }
    }
}

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Добавление
            //using (var db = new Context())
            //{
            //    db.Categories.Add(new Category() { Title = "Фрукты" });
            //    db.Categories.Add(new Category() { Title = "Овощи" });

            //    db.SaveChanges();
            //}

            using (var db = new Context())
            {
                //var categories = db.Categories.OrderBy(x => x.Id);
                //foreach (var category in categories)
                //{
                //    Console.WriteLine(category.Id.ToString() + " " + category.Title);
                //}

                //var category = db.Categories.Where(x => x.Title == "Фрукты").First();

                //Console.WriteLine(category.Id.ToString() + " " + category.Title);

                //var product = new Product()
                //{
                //    Title = "Груша",
                //    Price = (decimal)12.85,
                //    Quantity = 8,
                //    Category = category
                //};

                //db.Products.Add(product);
                //db.SaveChanges();

                var product = db.Products.
                    Include(p => p.Category).
                    FirstOrDefault();

                if (product == null) return;

                Console.WriteLine(product.Title);
                Console.WriteLine(product.Category.Title);

            }

        }
    }
}
