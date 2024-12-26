using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniApi.Migrations
{
    /// <inheritdoc />
    public partial class newRaccoonModelV202 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Raccoon_Owners_OwnerId",
                table: "Raccoon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Raccoon",
                table: "Raccoon");

            migrationBuilder.RenameTable(
                name: "Raccoon",
                newName: "Raccoons");

            migrationBuilder.RenameIndex(
                name: "IX_Raccoon_OwnerId",
                table: "Raccoons",
                newName: "IX_Raccoons_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Raccoons",
                table: "Raccoons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Raccoons_Owners_OwnerId",
                table: "Raccoons",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Raccoons_Owners_OwnerId",
                table: "Raccoons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Raccoons",
                table: "Raccoons");

            migrationBuilder.RenameTable(
                name: "Raccoons",
                newName: "Raccoon");

            migrationBuilder.RenameIndex(
                name: "IX_Raccoons_OwnerId",
                table: "Raccoon",
                newName: "IX_Raccoon_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Raccoon",
                table: "Raccoon",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Raccoon_Owners_OwnerId",
                table: "Raccoon",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id");
        }
    }
}
