using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrissCargoApp.Migrations
{
    public partial class moreDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductNamee",
                table: "Procurements",
                newName: "ProductName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Procurements",
                newName: "ProductNamee");
        }
    }
}
