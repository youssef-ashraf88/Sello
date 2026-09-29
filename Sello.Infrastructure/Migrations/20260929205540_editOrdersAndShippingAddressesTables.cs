using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sello.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editOrdersAndShippingAddressesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingAddresses_OrderId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "ShippingAddresses",
                newName: "UserId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "ShippingAddresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ShippingAddressId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddresses_UserId",
                table: "ShippingAddresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingAddressId",
                table: "Orders",
                column: "ShippingAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingAddresses_ShippingAddressId",
                table: "Orders",
                column: "ShippingAddressId",
                principalTable: "ShippingAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddresses_AspNetUsers_UserId",
                table: "ShippingAddresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingAddresses_ShippingAddressId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddresses_AspNetUsers_UserId",
                table: "ShippingAddresses");

            migrationBuilder.DropIndex(
                name: "IX_ShippingAddresses_UserId",
                table: "ShippingAddresses");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShippingAddressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "ShippingAddresses");

            migrationBuilder.DropColumn(
                name: "ShippingAddressId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ShippingAddresses",
                newName: "OrderId");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderId",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingAddresses_OrderId",
                table: "Orders",
                column: "OrderId",
                principalTable: "ShippingAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
