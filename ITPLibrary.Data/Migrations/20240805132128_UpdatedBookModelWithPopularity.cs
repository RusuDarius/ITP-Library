using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITPLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedBookModelWithPopularity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPopular",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPopular",
                table: "Books");
        }
    }
}
