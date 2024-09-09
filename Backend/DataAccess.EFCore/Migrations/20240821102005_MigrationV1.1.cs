using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class MigrationV11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Category_FkCategory",
                table: "Blog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog",
                table: "Blog");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "CategoryEntries");

            migrationBuilder.RenameTable(
                name: "Blog",
                newName: "BlogEntries");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_FkCategory",
                table: "BlogEntries",
                newName: "IX_BlogEntries_FkCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryEntries",
                table: "CategoryEntries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogEntries",
                table: "BlogEntries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogEntries_CategoryEntries_FkCategory",
                table: "BlogEntries",
                column: "FkCategory",
                principalTable: "CategoryEntries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogEntries_CategoryEntries_FkCategory",
                table: "BlogEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryEntries",
                table: "CategoryEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogEntries",
                table: "BlogEntries");

            migrationBuilder.RenameTable(
                name: "CategoryEntries",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "BlogEntries",
                newName: "Blog");

            migrationBuilder.RenameIndex(
                name: "IX_BlogEntries_FkCategory",
                table: "Blog",
                newName: "IX_Blog_FkCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog",
                table: "Blog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Category_FkCategory",
                table: "Blog",
                column: "FkCategory",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
