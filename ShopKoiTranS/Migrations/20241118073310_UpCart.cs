﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopKoiTranS.Migrations
{

    public partial class UpCart : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Carts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Carts");
        }
    }
}
