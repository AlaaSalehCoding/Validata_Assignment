using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualShop.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Refactoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "BaseDeletableEntity");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "BaseDeletableEntity",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BaseDeletableEntity",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModified",
                table: "BaseDeletableEntity",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Created",
                table: "BaseDeletableEntity",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "BaseDeletableEntity",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                table: "BaseDeletableEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "BaseDeletableEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnDate",
                table: "BaseDeletableEntity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseDeletableEntity",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "BaseDeletableEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "BaseDeletableEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "BaseDeletableEntity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "BaseDeletableEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "BaseDeletableEntity",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "BaseDeletableEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Product_Price",
                table: "BaseDeletableEntity",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "BaseDeletableEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "BaseDeletableEntity",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BaseDeletableEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseDeletableEntity",
                table: "BaseDeletableEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BaseDeletableEntity_CustomerId",
                table: "BaseDeletableEntity",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseDeletableEntity_OrderId",
                table: "BaseDeletableEntity",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseDeletableEntity_ProductId",
                table: "BaseDeletableEntity",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseDeletableEntity_BaseDeletableEntity_CustomerId",
                table: "BaseDeletableEntity",
                column: "CustomerId",
                principalTable: "BaseDeletableEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseDeletableEntity_BaseDeletableEntity_OrderId",
                table: "BaseDeletableEntity",
                column: "OrderId",
                principalTable: "BaseDeletableEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseDeletableEntity_BaseDeletableEntity_ProductId",
                table: "BaseDeletableEntity",
                column: "ProductId",
                principalTable: "BaseDeletableEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseDeletableEntity_BaseDeletableEntity_CustomerId",
                table: "BaseDeletableEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseDeletableEntity_BaseDeletableEntity_OrderId",
                table: "BaseDeletableEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseDeletableEntity_BaseDeletableEntity_ProductId",
                table: "BaseDeletableEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseDeletableEntity",
                table: "BaseDeletableEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseDeletableEntity_CustomerId",
                table: "BaseDeletableEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseDeletableEntity_OrderId",
                table: "BaseDeletableEntity");

            migrationBuilder.DropIndex(
                name: "IX_BaseDeletableEntity_ProductId",
                table: "BaseDeletableEntity");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "BaseDeletableEntity");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "BaseDeletableEntity");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "BaseDeletableEntity");

            migrationBuilder.DropColumn(
                name: "DeletedOnDate",
                table: "BaseDeletableEntity");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseDeletableEntity");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "BaseDeletableEntity");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "BaseDeletableEntity");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "BaseDeletableEntity");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "BaseDeletableEntity");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "BaseDeletableEntity");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "BaseDeletableEntity");

            migrationBuilder.DropColumn(
                name: "Product_Price",
                table: "BaseDeletableEntity");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "BaseDeletableEntity");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "BaseDeletableEntity");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BaseDeletableEntity");

            migrationBuilder.RenameTable(
                name: "BaseDeletableEntity",
                newName: "Product");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModified",
                table: "Product",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Created",
                table: "Product",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_OrderId",
                table: "Item",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ProductId",
                table: "Item",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");
        }
    }
}
