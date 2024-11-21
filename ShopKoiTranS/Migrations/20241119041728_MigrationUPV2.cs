using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopKoiTranS.Migrations
{
    /// <inheritdoc />
    public partial class MigrationUPV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonVanChuyens_Orders_OrderModelId",
                table: "DonVanChuyens");

            migrationBuilder.DropForeignKey(
                name: "FK_LichTuVans_Orders_OrderModelId",
                table: "LichTuVans");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Orders",
                newName: "GrandTota");

            migrationBuilder.RenameColumn(
                name: "OrderModelId",
                table: "LichTuVans",
                newName: "CartModelCartId");

            migrationBuilder.RenameIndex(
                name: "IX_LichTuVans_OrderModelId",
                table: "LichTuVans",
                newName: "IX_LichTuVans_CartModelCartId");

            migrationBuilder.RenameColumn(
                name: "OrderModelId",
                table: "DonVanChuyens",
                newName: "CartModelCartId");

            migrationBuilder.RenameIndex(
                name: "IX_DonVanChuyens_OrderModelId",
                table: "DonVanChuyens",
                newName: "IX_DonVanChuyens_CartModelCartId");

            migrationBuilder.AddColumn<int>(
                name: "AdviseId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransportId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdviseId",
                table: "OrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransportId",
                table: "OrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalTransportPrice",
                table: "Carts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AdviseId",
                table: "Orders",
                column: "AdviseId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TransportId",
                table: "Orders",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_AdviseId",
                table: "OrderDetails",
                column: "AdviseId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_TransportId",
                table: "OrderDetails",
                column: "TransportId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonVanChuyens_Carts_CartModelCartId",
                table: "DonVanChuyens",
                column: "CartModelCartId",
                principalTable: "Carts",
                principalColumn: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_LichTuVans_Carts_CartModelCartId",
                table: "LichTuVans",
                column: "CartModelCartId",
                principalTable: "Carts",
                principalColumn: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_DonVanChuyens_TransportId",
                table: "OrderDetails",
                column: "TransportId",
                principalTable: "DonVanChuyens",
                principalColumn: "TransportId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_LichTuVans_AdviseId",
                table: "OrderDetails",
                column: "AdviseId",
                principalTable: "LichTuVans",
                principalColumn: "AdviseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DonVanChuyens_TransportId",
                table: "Orders",
                column: "TransportId",
                principalTable: "DonVanChuyens",
                principalColumn: "TransportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_LichTuVans_AdviseId",
                table: "Orders",
                column: "AdviseId",
                principalTable: "LichTuVans",
                principalColumn: "AdviseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonVanChuyens_Carts_CartModelCartId",
                table: "DonVanChuyens");

            migrationBuilder.DropForeignKey(
                name: "FK_LichTuVans_Carts_CartModelCartId",
                table: "LichTuVans");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_DonVanChuyens_TransportId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_LichTuVans_AdviseId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DonVanChuyens_TransportId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_LichTuVans_AdviseId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AdviseId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TransportId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_AdviseId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_TransportId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "AdviseId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TransportId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AdviseId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "TransportId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "TotalTransportPrice",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "GrandTota",
                table: "Orders",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "CartModelCartId",
                table: "LichTuVans",
                newName: "OrderModelId");

            migrationBuilder.RenameIndex(
                name: "IX_LichTuVans_CartModelCartId",
                table: "LichTuVans",
                newName: "IX_LichTuVans_OrderModelId");

            migrationBuilder.RenameColumn(
                name: "CartModelCartId",
                table: "DonVanChuyens",
                newName: "OrderModelId");

            migrationBuilder.RenameIndex(
                name: "IX_DonVanChuyens_CartModelCartId",
                table: "DonVanChuyens",
                newName: "IX_DonVanChuyens_OrderModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonVanChuyens_Orders_OrderModelId",
                table: "DonVanChuyens",
                column: "OrderModelId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LichTuVans_Orders_OrderModelId",
                table: "LichTuVans",
                column: "OrderModelId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
