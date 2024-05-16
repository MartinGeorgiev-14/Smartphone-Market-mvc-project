using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.Data.Migrations
{
    /// <inheritdoc />
    public partial class update4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageTumbnailImg",
                table: "Smartphone");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Smartphone",
                newName: "Image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Smartphone",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "ImageTumbnailImg",
                table: "Smartphone",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
