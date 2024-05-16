using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.Data.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusNum",
                table: "OrderStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusNum",
                table: "OrderStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
