using FitMax.DataAccess.Identity;
using FitMax.Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.DataAccess.Contexts
{
    public class FitMaxContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public FitMaxContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartLine> CartLines { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PrivateLesson> PrivateLessons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<WalletDetail> WalletDetails { get; set; }
        public DbSet<Contact> Contacts { get; set; }





        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Product>().Property("Price").HasColumnType("money");
            builder.Entity<Product>().Property("Purchaseprice").HasColumnType("money");
            builder.Entity<Cart>().Property("TotalPrice").HasColumnType("money");
            builder.Entity<CartLine>().Property("TotalPrice").HasColumnType("money");
            builder.Entity<CartLine>().Property("UnitPrice").HasColumnType("money");
            builder.Entity<PrivateLesson>().Property("TrainerPrice").HasColumnType("money");
            builder.Entity<Wallet>().Property("Balance").HasColumnType("money");
            builder.Entity<WalletDetail>().Property("Amount").HasColumnType("money");
            builder.Entity<Package>().Property("Price").HasColumnType("money");
            builder.Entity<AppUser>().Property("TrainerPrice").HasColumnType("money");


            //Seed Data
            builder.Entity<Product>().HasData(
                new Product() { Id = 1, Name = "Musclecloth Lifting Strap Kırmızı", ProductType = "spor", Description = "Musclecloth Lifting Strap Kırmızı", Purchaseprice = 60, Price = 95, Status = true, Stock = 50, ImgUrl = "/images/Musclecloth.jpeg" },

                new Product() { Id = 2, Name = "Musclecloth Pro Wrist Wraps", ProductType = "spor", Description = "Musclecloth Pro Wrist Wraps Siyah Kırmızı 2'li Paket", Purchaseprice = 70, Price = 150, Status = true, Stock = 25, ImgUrl = "/images/Wrist.jpeg" },

                new Product() { Id = 3, Name = "Busso Klips-20 Kısa Bar Kelebek Klips", ProductType = "spor", Description = "Busso Klips-20 Kısa Bar Kelebek Klips", Purchaseprice = 30, Price = 67, Status = true, Stock = 45, ImgUrl = "/images/kelebek.jpeg" },

                new Product() { Id = 4, Name = "Delta Bat Grip Pad (El Pedi) Ağırlık Body ", ProductType = "spor", Description = "Delta Bat Grip Pad (El Pedi) Ağırlık Body Fitness Dambıl Eldiveni", Purchaseprice = 90, Price = 145, Status = true, Stock = 55, ImgUrl = "/images/10.jpeg" },

                new Product() { Id = 5, Name = "Dragon Fat Gripz Silikon Dambıl Halter Sapları", ProductType = "spor", Description = "Dragon Fat Gripz Silikon Dambıl Halter Sapları Kaymaz Koruma Pedi", Purchaseprice = 130, Price = 160, Status = true, Stock = 12, ImgUrl = "/images/11.jpeg" },

                new Product() { Id = 6, Name = "Proforce Db-Zen Ayarlanabilir Dambıl", ProductType = "spor", Description = "MProforce Db-Zen Ayarlanabilir Dambıl - 24 kg", Purchaseprice = 2700, Price = 3600, Status = true, Stock = 16, ImgUrl = "/images/123.jpeg" },

                new Product() { Id = 15, Name = "Scucs Koordinasyon Çemberi 12'li", ProductType = "spor", Description = "Scucs Koordinasyon Çemberi 12'li", Purchaseprice = 100, Price = 198, Status = true, Stock = 50, ImgUrl = "/images/14.jpeg" },

                new Product() { Id = 7, Name = "Hardline Whey 3 Matrix Protein Tozu 2300 gr", ProductType = "besin", Description = "Hardline Whey 3 Matrix Protein Tozu 2300 gr", Purchaseprice = 1000, Price = 166, Status = true, Stock = 22, ImgUrl = "/images/p1.jpeg" },

                new Product() { Id = 8, Name = "Navy Plus Nutrition 2300 gr Whey Protein Tozu ", ProductType = "besin", Description = "Navy Plus Nutrition 2300 gr Whey Protein Tozu Çilek Aromalı + Shaker + Askılı Çanta + Antrenman Havlusu + 2 x Protein Bar", Purchaseprice =1200, Price = 1645, Status = true, Stock = 50, ImgUrl = "/images/p2.jpeg" },

                new Product() { Id = 9, Name = "High Whey Protein 2280 gr Çikolata Aromalı ", ProductType = "besin", Description = "High Whey Protein 2280 gr Çikolata Aromalı Protein Tozu 24 Gram Protein 76 Servis", Purchaseprice = 201, Price =452, Status = true, Stock = 47, ImgUrl = "/images/p4.jpeg" },

                new Product() { Id = 10, Name = "Ronic Nutrition Ultimate Isolate Whey Protein Tozu ", ProductType = "besin", Description = "Ronic Nutrition Ultimate Isolate Whey Protein Tozu 2270 gr + Shaker ve 2 Adet Tek Kullanımlık Whey Protein", Purchaseprice = 650, Price = 980, Status = true, Stock = 20, ImgUrl = "/images/p5.jpeg" },

                new Product() { Id = 11, Name = "Nutripure Whey Classic Protein Tozu 2000 gr", ProductType = "besin", Description = "Nutripure Whey Classic Protein Tozu 2000 gr - Çikolata Aromalı", Purchaseprice = 252, Price = 682, Status = true, Stock = 24, ImgUrl = "/images/p6.jpeg" },

                new Product() { Id = 12, Name = "High Whey Protein Tozu 2280 gr Çilek Aromalı ", ProductType = "besin", Description = "High Whey Protein Tozu 2280 gr Çilek Aromalı Protein Tozu 24 Gram Protein Kas Güç 76 Servis Shaker Hediyeli", Purchaseprice = 230, Price = 470, Status = true, Stock = 44, ImgUrl = "/images/p9.jpeg" },

                new Product() { Id = 13, Name = "Pharma Whey Protein 2196 gr Çikolata Aromalı", ProductType = "besin", Description = "Pharma Whey Protein 2196 gr Çikolata Aromalı Whey Protein Tozu 24 gr Protein 5 gr Bcaa", Purchaseprice = 460, Price = 690, Status = true, Stock = 24, ImgUrl = "/images/p12.jpeg" },

                new Product() { Id = 14, Name = "Bigjoy BIGWHEY Whey Protein Classic Çikolata", ProductType = "besin", Description = "Bigjoy Sports BIGWHEY Whey Protein Classic Çikolata 1020g 30 Servis", Purchaseprice = 360, Price = 786, Status = true, Stock = 15, ImgUrl = "/images/p13.jpeg" }
                );

            builder.Entity<Package>().HasData(
                new Package() { Id=1,Name="3 Ay",Price=3050, Description= "3 Aylık Salon Üyeliği" ,Description2= "Standart Program Desteği" ,Description3= "Grup Derslerine Katılma Hakkı" },
                new Package() { Id=2,Name="6 Ay", Price = 6200, Description= "6 Aylık Salon Üyeliği" ,Description2= "Aktif Program Desteği", Description3= "Grup Derslerine Katılma Hakkı" },
                new Package() { Id=3,Name="12 Ay", Price = 10100, Description= "12 Aylık Salon Üyeliği" ,Description2= "Tam Program Desteği", Description3= "Grup Derslerine Katılma Hakkı" }
                );




           base.OnModelCreating(builder);
        }






    }
}
