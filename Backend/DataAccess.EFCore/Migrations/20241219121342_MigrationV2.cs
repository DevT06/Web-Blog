using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class MigrationV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CategoryEntries",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "CategoryEntries",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CategoryEntries",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageId" },
                values: new object[] { "Exploring new destinations, cultures, and experiences around the world.", null });

            migrationBuilder.UpdateData(
                table: "CategoryEntries",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageId" },
                values: new object[] { "Special occasions and celebrations, often marked by leisure and joy.", null });

            migrationBuilder.UpdateData(
                table: "CategoryEntries",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "ImageId" },
                values: new object[] { "Everything about cars, from models and features to maintenance and reviews.", null });

            migrationBuilder.UpdateData(
                table: "CategoryEntries",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "ImageId" },
                values: new object[] { "Topics about skincare, makeup, fashion, and enhancing personal appearance.", null });

            migrationBuilder.InsertData(
                table: "CategoryEntries",
                columns: new[] { "Id", "Description", "ImageId", "Name" },
                values: new object[,]
                {
                    { 5, "Insights and details about nations, their cultures, and landmarks.", null, "Country" },
                    { 6, "Adventures in nature, including trails, mountains, and outdoor exploration.", null, "Hiking" },
                    { 7, "Activities and competitions that involve physical effort and skill.", null, "Sports" },
                    { 8, "Reflections and insights on everyday living and personal experiences.", null, "Life" },
                    { 9, "Learning opportunities, academic subjects, and personal growth.", null, "Education" },
                    { 10, "Milestones and accomplishments in personal or professional life.", null, "Achievement" },
                    { 11, "Narratives and accounts of experiences, real or fictional.", null, "Story" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoryEntries",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CategoryEntries",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CategoryEntries",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CategoryEntries",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CategoryEntries",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CategoryEntries",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CategoryEntries",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CategoryEntries");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "CategoryEntries");
        }
    }
}
