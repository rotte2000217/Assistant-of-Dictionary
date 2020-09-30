using Microsoft.EntityFrameworkCore.Migrations;

namespace DictionaryAssistantMVC.Migrations
{
    public partial class SecondSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Words_TheWord",
                table: "Words",
                column: "TheWord",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Words_TheWord",
                table: "Words");
        }
    }
}
