using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrissCargoApp.Migrations
{
    public partial class updateprocurement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Customers",
                table: "Procurements");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Procurements",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Procurements_CustomerId",
                table: "Procurements",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Procurements_AspNetUsers_CustomerId",
                table: "Procurements",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Procurements_AspNetUsers_CustomerId",
                table: "Procurements");

            migrationBuilder.DropIndex(
                name: "IX_Procurements_CustomerId",
                table: "Procurements");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Procurements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Customers",
                table: "Procurements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
