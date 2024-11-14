using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopKoiTranS.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonVanChuyens",
                columns: table => new
                {
                    TransportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiCa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenCa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanNang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiaDiemXuatPhat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaDiemDen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhuongThucVanChuyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhuongTienVanChuyen = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonVanChuyens", x => x.TransportId);
                });

            migrationBuilder.CreateTable(
                name: "KoiWorld",
                columns: table => new
                {
                    KoiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KoiName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CategoryKoiId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KoiWorld", x => x.KoiId);
                });

            migrationBuilder.CreateTable(
                name: "LichTuVans",
                columns: table => new
                {
                    AdviseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianTuVan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichTuVans", x => x.AdviseId);
                });

            migrationBuilder.CreateTable(
                name: "LoaiCaKoi",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiCaKoi", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransportMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeightRange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price0_50 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price51_100 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price101_200 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceOver200 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceOver300 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceOver20Kg = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonVanChuyens");

            migrationBuilder.DropTable(
                name: "KoiWorld");

            migrationBuilder.DropTable(
                name: "LichTuVans");

            migrationBuilder.DropTable(
                name: "LoaiCaKoi");

            migrationBuilder.DropTable(
                name: "Price");
        }
    }
}
