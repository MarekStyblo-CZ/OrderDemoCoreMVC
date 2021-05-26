using Microsoft.EntityFrameworkCore;
using OrderDemoCoreMVC.Models.DbSets;
using System;
using System.Collections.Generic;

namespace OrderDemoCoreMVC.Data
{
    public class SqlDbContext : DbContext, IDisposable
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }



        /// <summary>
        /// Defines entity relationships + seeds db
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //# Its question - what behaviour to choose when product is deleted - current setup deletes related OrderItems
            //1:N
            modelBuilder.Entity<Product>()
                .HasMany<OrderItem>(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .IsRequired()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            //1:N
            modelBuilder.Entity<Order>()
                .HasMany<OrderItem>(o => o.OderItems)
                .WithOne(oi => oi.Order)
                .IsRequired()
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            SeedDb(modelBuilder);

        }

        //ensures that by using Add-migration -> db will be populated with initial test data
        private void SeedDb(ModelBuilder modelBuilder)
        {
            var today = DateTime.Now;

            modelBuilder.Entity<Product>().HasData(new List<Product>()
            {
                new Product { Id = 1, Code=111, Name="Pračka", Price=7600.5f},
                new Product { Id = 2, Code=112, Name="Myčka", Price=9800.6f},
            });

            modelBuilder.Entity<Order>().HasData(new List<Order>()
            {
                new Order { Id = 1, CustomerName="Aleš Vopěnka", CustomerAddress="Uliční 22, Těškov", Created=new DateTime(2020,05,20) },
                new Order { Id = 2, CustomerName="Jan Novák", CustomerAddress="Testovací 11, Zadov", Created=new DateTime(2020,05,21) },

            });

            modelBuilder.Entity<OrderItem>().HasData(new List<OrderItem>()
            {
                new OrderItem { Id = 1, OrderId=1, ProductId=1, Price=7600.5f, Quantity=1 },
                new OrderItem { Id = 2, OrderId=2, ProductId=2, Price=9800.6f, Quantity=1 }
            });
        }

        public override void Dispose()
        {
            //#todo
            //this.Dispose();
            //GC.SuppressFinalize(this);
        }

    }
}
