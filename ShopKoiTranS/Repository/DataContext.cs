using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopKoiTranS.Models;

namespace ShopKoiTranS.Repository
{
    public class DataContext : IdentityDbContext<AppUserModel>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<TransportModel> DonVanChuyens { get; set; }
        public DbSet<AdviseModel> LichTuVans { get; set; }
        public DbSet<KoiWorldModel> KoiWorld { get; set; }
        public DbSet<CategoriKoisModel> LoaiCaKoi { get; set; }
        public DbSet<PriceModel> Price { get; set; }
        public DbSet<SalesModel> MemberCars { get; set; }
        public DbSet<FeedbackModel> Feedbacks { get; set; }

        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderDetailModel> OrderDetails { get; set; }
        public DbSet<CartModel> Carts { get; set; }
        public DbSet<CartItemModel> CartItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<KoiWorldModel>()
                .Property(k => k.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<KoiWorldModel>()
                .Property(k => k.Size)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<OrderDetailModel>()
                .Property(o => o.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<PriceModel>()
                .Property(p => p.Price0_50)
                .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<PriceModel>()
                .Property(p => p.Price101_200)
                .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<PriceModel>()
                .Property(p => p.Price51_100)
                .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<PriceModel>()
                .Property(p => p.PriceOver200)
                .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<PriceModel>()
                .Property(p => p.PriceOver20Kg)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<TransportModel>()
                .Property(t => t.CanNang)
                .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<TransportModel>()
                .Property(t => t.Distance)
                .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<TransportModel>()
                .Property(t => t.TransportPrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<OrderModel>()
                .Property(o => o.GrandTota)
                .HasColumnType("decimal(18, 2)");


        }

    }
}