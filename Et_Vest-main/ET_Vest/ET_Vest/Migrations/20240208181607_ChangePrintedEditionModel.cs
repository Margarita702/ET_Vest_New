using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ET_Vest.Migrations
{
    /// <inheritdoc />
    public partial class ChangePrintedEditionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "TradeObjects");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PrintedEditions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PrintedEditions");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "TradeObjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
