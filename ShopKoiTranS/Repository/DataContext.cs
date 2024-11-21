using Microsoft.EntityFrameworkCore;
using ShopKoiTranS.Models;

namespace ShopKoiTranS.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) 
        {

        }
        public DbSet<TransportModel> DonVanChuyens { get; set; }
        public DbSet<AdviseModel> LichTuVans {  get; set; }
        public DbSet<KoiWorldModel> KoiWorld { get; set; }
        public DbSet<CategoriKoisModel> LoaiCaKoi { get; set; }
        public DbSet<PriceModel> Price {  get; set; }   
        public DbSet<SalesModel> MemberCars { get; set; }
        public DbSet<FeedbackModel> Feedbacks { get; set; }
    }
}
