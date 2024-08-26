using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITPLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class BookModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "AddedDateTime",
                table: "Books",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsPromoted",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "Thumbnail",
                table: "Books",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedDateTime",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsPromoted",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Books");
        }
    }
}
