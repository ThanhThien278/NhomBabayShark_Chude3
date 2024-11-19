using Microsoft.EntityFrameworkCore;
using ShopKoiTranS.Models;

namespace ShopKoiTranS.Repository
{
    public class SeedData
    {
        public static void SeedingData(DataContext _context)
        {
            _context.Database.Migrate();


            if (!_context.LoaiCaKoi.Any())
            {
                var shiro = new CategoriKoisModel
                {
                    CategoryName = "Shiro",
                    Description = "Là loại cá phong thủy, hiện nay được rất nhiều người đam mê cá cảnh yêu thích. Đây là giống cá koi Nhật Bản được giới nuôi cá chép cảnh đặc biệt yêu thích.",
                    IsActive = true
                };

                var tancho = new CategoriKoisModel
                {
                    CategoryName = "Tancho",
                    Description = "Trong các loại cá chép Nhật thì cá koi Tancho luôn có mức giá cao và được nhiều người tìm mua. Vẻ đẹp của koi Tancho thanh lịch, sang trọng với duy nhất một chấm tròn đỏ lớn nằm ở trên đầu.",
                    IsActive = true
                };

                var asagi = new CategoriKoisModel
                {
                    CategoryName = "Asagi",
                    Description = "Cá koi asagi mang vẻ đẹp hút hồn chinh phục ngay cả những người sành chơi koi Nhất. Điểm đặc biệt không lẫn vào đâu được của koi asagi chính là lớp vẩy rõ nét màu xanh dương có ánh kim trên lưng của chúng.",
                    IsActive = true
                };

                var yamabuki = new CategoriKoisModel
                {
                    CategoryName = "Yamabuki",
                    Description = "Cá Koi vàng ánh kim hay còn được gọi với cái tên khác của Nhật Bản là Yamabuki Ogon. Cá Koi Yamabuki Ogon là một thành viên thuộc lớp Hikari Muji, được bao phủ một lớp màu vàng óng óng toàn thân.",
                    IsActive = true
                };

                _context.LoaiCaKoi.AddRange(shiro, tancho, asagi, yamabuki);

                _context.SaveChanges();

                var shiroCategoryId = _context.LoaiCaKoi.First(c => c.CategoryName == "Shiro").CategoryId;
                var tanchoCategoryId = _context.LoaiCaKoi.First(c => c.CategoryName == "Tancho").CategoryId;
                var asagiCategoryId = _context.LoaiCaKoi.First(c => c.CategoryName == "Asagi").CategoryId;
                var yamabukiCategoryId = _context.LoaiCaKoi.First(c => c.CategoryName == "Yamabuki").CategoryId;

                _context.KoiWorld.AddRange(
                    new KoiWorldModel
                    {
                        KoiName = "Shiro Utsuri",
                        Price = 1000000,
                        Image = "Shiro-Utsuri.png",
                        Description = "Màu trắng với các vệt đen",
                        Size = 50,
                        CategoryKoiId = shiroCategoryId
                    },
                    new KoiWorldModel
                    {
                        KoiName = "Shiro Bekko",
                        Price = 1000000,
                        Image = "Shiro Bekko.png",
                        Description = "Màu trắng với các đốm đen",
                        Size = 30,
                        CategoryKoiId = shiroCategoryId
                    },
                    new KoiWorldModel
                    {
                        KoiName = "Tancho Kohaku",
                        Price = 3500000,
                        Image = "Tancho Kohaku.png",
                        Description = "Màu trắng với đốm đỏ duy nhất trên đầu",
                        Size = 60,
                        CategoryKoiId = tanchoCategoryId
                    },
                    new KoiWorldModel
                    {
                        KoiName = "Tancho Showa",
                        Price = 3500000,
                        Image = "Tancho_Showa.png",
                        Description = "Màu đen với đốm đỏ duy nhất trên đầu",
                        Size = 50,
                        CategoryKoiId = tanchoCategoryId
                    },
                    new KoiWorldModel
                    {
                        KoiName = "Tancho Sanke",
                        Price = 4000000,
                        Image = "Tancho_Sanke.png",
                        Description = "Màu trắng với đốm đỏ và đen, và đốm đỏ trên đầu",
                        Size = 50,
                        CategoryKoiId = tanchoCategoryId
                    },
                    new KoiWorldModel
                    {
                        KoiName = "Asagi",
                        Price = 800000,
                        Image = "Asagi_lam.png",
                        Description = "Màu xanh lam với lưng màu đỏ hoặc cam",
                        Size = 45,
                        CategoryKoiId = asagiCategoryId
                    },
                    new KoiWorldModel
                    {
                        KoiName = "Shusui",
                        Price = 800000,
                        Image = "Shusui.png",
                        Description = "Giống như Asagi nhưng có vảy trên lưng",
                        Size = 40,
                        CategoryKoiId = asagiCategoryId
                    },
                    new KoiWorldModel
                    {
                        KoiName = "Asagi Ginrin",
                        Price = 700000,
                        Image = "Asagi_Ginrin.png",
                        Description = "Giống Asagi nhưng có ánh kim trên cơ thể",
                        Size = 45,
                        CategoryKoiId = asagiCategoryId
                    },
                    new KoiWorldModel
                    {
                        KoiName = "Yamato Nishiki",
                        Price = 2500000,
                        Image = "Yamato_Nishiki.png",
                        Description = "Màu trắng với các vệt đỏ hoặc vàng",
                        Size = 40,
                        CategoryKoiId = yamabukiCategoryId
                    },
                    new KoiWorldModel
                    {
                        KoiName = "Yamato Nishiki Ginrin",
                        Price = 3000000,
                        Image = "Yamato_Nishiki_Ginrin.png",
                        Description = "Giống Yamato Nishiki nhưng có ánh kim",
                        Size = 50,
                        CategoryKoiId = yamabukiCategoryId
                    },
                    new KoiWorldModel
                    {
                        KoiName = "Yamato Nishiki Budo",
                        Price = 3500000,
                        Image = "Yamato-Nishiki-Budo.png",
                        Description = "Giống Yamato Nishiki với màu sắc tím hoặc đỏ đậm",
                        Size = 45,
                        CategoryKoiId = yamabukiCategoryId
                    },
                    new KoiWorldModel
                    {
                        KoiName = "Yamabuki Ogon",
                        Price = 800000,
                        Image = "Yamabuki_Ogon.png",
                        Description = "Màu vàng sáng",
                        Size = 55,
                        CategoryKoiId = yamabukiCategoryId
                    },
                    new KoiWorldModel
                    {
                        KoiName = "Yamabuki Ogon Ginrin",
                        Price = 3000000,
                        Image = "Yamabuki_Ginrin.png",
                        Description = "Màu vàng sáng với ánh kim",
                        Size = 30,
                        CategoryKoiId = yamabukiCategoryId
                    }
                );

                _context.SaveChanges();
            }

            
            if (!_context.MemberCars.Any())
            {
                _context.MemberCars.AddRange(
                    new SalesModel
                    {
                        Title = "Hội Viên Thường",
                        Condition = "Tổng số tiền đã đặt trên 1 triệu đồng và hoàn thành đủ 3 đơn hàng",
                        Benefits = new List<string>
                        {
                "Ưu tiên vận chuyển",
                "Giảm giá 3% cho mỗi lần vận chuyển với khoảng cách dưới 50km",
                "Hỗ trợ 24/7"
                        },
                        ImageUrl = "thuong.jpg"
                    },
                    new SalesModel
                    {
                        Title = "Hội Viên Bạc",
                        Condition = "Tổng số tiền đã đặt trên 3 triệu đồng và hoàn thành đủ 7 đơn hàng",
                        Benefits = new List<string>
                        {
                "Ưu tiên vận chuyển",
                "Giảm giá 5% cho mỗi lần vận chuyển",
                "Hỗ trợ 24/7"
                        },
                        ImageUrl = "bachv.jpg"
                    },
                    new SalesModel
                    {
                        Title = "Hội Viên Vàng",
                        Condition = "Tổng số tiền đã đặt trên 5 triệu đồng và hoàn thành đủ 10 đơn hàng",
                        Benefits = new List<string>
                        {
                "Ưu tiên vận chuyển",
                "Giảm giá 15% cho mỗi lần vận chuyển và miễn phí bảo hiểm hàng hóa",
                "Hỗ trợ 24/7"
                        },
                        ImageUrl = "vang.jpg"
                    },
                    new SalesModel
                    {
                        Title = "Hội Viên Kim Cương",
                        Condition = "Tổng số tiền đã đặt trên 15 triệu đồng và hoàn thành đủ 30 đơn hàng",
                        Benefits = new List<string>
                        {
                "Ưu tiên vận chuyển",
                "Giảm giá 20% cho mỗi lần vận chuyển và miễn phí bảo hiểm hàng hóa",
                "Hỗ trợ 24/7"
                        },
                        ImageUrl = "kc.jpg"
                    },
                    new SalesModel
                    {
                        Title = "Hội Viên Hắc Kim",
                        Condition = "Tổng số tiền đã đặt trên 30 triệu đồng và hoàn thành đủ 40 đơn hàng",
                        Benefits = new List<string>
                        {
                "Ưu tiên vận chuyển",
                "Giảm giá 40% cho mỗi lần vận chuyển và miễn phí bảo hiểm hàng hóa",
                "Hỗ trợ 24/7",
                "Được ưu tiên xử lý đơn hàng"
                        },
                        ImageUrl = "hvhk.jpg"
                    }
                );

                _context.SaveChanges(); 
            }
            if (!_context.Price.Any())
            {
                _context.Price.AddRange(
                    new PriceModel
                    {
                        TransportMethod = "Land",
                        VehicleType = "Xe Tải Nhỏ",
                        MinWeight = 1,
                        MaxWeight = 5,
                        Price0_50 = 200000,
                        Price51_100 = 300000,
                        Price101_200 = 500000,
                        PriceOver200 = 800000
                    },
                    new PriceModel
                    {
                        TransportMethod = "Land",
                        VehicleType = "Xe Tải Nhỏ",
                        MinWeight = 6,
                        MaxWeight = 10,
                        Price0_50 = 250000,
                        Price51_100 = 350000,
                        Price101_200 = 600000,
                        PriceOver200 = 900000
                    },
                    new PriceModel
                    {
                        TransportMethod = "Land",
                        VehicleType = "Xe Tải Nhỏ",
                        MinWeight = 11,
                        MaxWeight = 20,
                        Price0_50 = 300000,
                        Price51_100 = 400000,
                        Price101_200 = 700000,
                        PriceOver200 = 1000000
                    },
                    new PriceModel
                    {
                        TransportMethod = "Land",
                        VehicleType = "Xe Tải Lớn",
                        MinWeight = 1,
                        MaxWeight = 5,
                        Price0_50 = 300000,
                        Price51_100 = 400000,
                        Price101_200 = 700000,
                        PriceOver200 = 1000000
                    },
                    new PriceModel
                    {
                        TransportMethod = "Land",
                        VehicleType = "Xe chuyên dụng",
                        MinWeight = 1,
                        MaxWeight = 5,
                        Price0_50 = 500000,
                        Price51_100 = 700000,
                        Price101_200 = 1000000,
                        PriceOver200 = 1500000
                    },
        new PriceModel
        {
            TransportMethod = "Land",
            VehicleType = "Xe chuyên dụng",
            MinWeight = 6,
            MaxWeight = 10,
            Price0_50 = 600000,
            Price51_100 = 800000,
            Price101_200 = 1200000,
            PriceOver200 = 1800000
        },
        new PriceModel
        {
            TransportMethod = "Land",
            VehicleType = "Xe chuyên dụng",
            MinWeight = 11,
            MaxWeight = 20,
            Price0_50 = 700000,
            Price51_100 = 900000,
            Price101_200 = 1500000,
            PriceOver200 = 2000000
        },
                    new PriceModel
                    {
                        TransportMethod = "Land",
                        VehicleType = "Xe Tải Lớn",
                        MinWeight = 6,
                        MaxWeight = 10,
                        Price0_50 = 350000,
                        Price51_100 = 450000,
                        Price101_200 = 800000,
                        PriceOver200 = 1200000
                    },
                    new PriceModel
                    {
                        TransportMethod = "Land",
                        VehicleType = "Xe Tải Lớn",
                        MinWeight = 11,
                        MaxWeight = 20,
                        Price0_50 = 400000,
                        Price51_100 = 500000,
                        Price101_200 = 900000,
                        PriceOver200 = 1500000
                    },
                    new PriceModel
                    {
                        TransportMethod = "Air",
                        VehicleType = "Máy Bay",
                        MinWeight = 1,
                        MaxWeight = 5,
                        Price0_50 = 1000000,
                        Price51_100 = 1500000,
                        Price101_200 = 2000000,
                        PriceOver200 = 3000000
                    },
                    new PriceModel
                    {
                        TransportMethod = "Air",
                        VehicleType = "Máy Bay",
                        MinWeight = 6,
                        MaxWeight = 10,
                        Price0_50 = 1200000,
                        Price51_100 = 1700000,
                        Price101_200 = 2200000,
                        PriceOver200 = 3500000
                    },
                    new PriceModel
                    {
                        TransportMethod = "Air",
                        VehicleType = "Máy Bay",
                        MinWeight = 11,
                        MaxWeight = 20,
                        Price0_50 = 1500000,
                        Price51_100 = 2000000,
                        Price101_200 = 2500000,
                        PriceOver200 = 4000000
                    },
                    new PriceModel
                    {
                        TransportMethod = "Sea",
                        VehicleType = "Tàu Chở Hàng",
                        MinWeight = 1,
                        MaxWeight = 5,
                        Price0_50 = 800000,
                        Price51_100 = 1200000,
                        Price101_200 = 1600000,
                        PriceOver200 = 2500000
                    },
                    new PriceModel
                    {
                        TransportMethod = "Sea",
                        VehicleType = "Tàu Chở Hàng",
                        MinWeight = 6,
                        MaxWeight = 10,
                        Price0_50 = 1000000,
                        Price51_100 = 1400000,
                        Price101_200 = 1800000,
                        PriceOver200 = 3000000
                    },
                    new PriceModel
                    {
                        TransportMethod = "Sea",
                        VehicleType = "Tàu Chở Hàng",
                        MinWeight = 11,
                        MaxWeight = 20,
                        Price0_50 = 1200000,
                        Price51_100 = 1600000,
                        Price101_200 = 2000000,
                        PriceOver200 = 3500000
                    }
                );

                _context.SaveChanges();  
            }



        }
    }
}
