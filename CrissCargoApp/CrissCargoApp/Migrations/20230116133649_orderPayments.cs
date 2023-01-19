using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrissCargoApp.Migrations
{
    public partial class orderPayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customersName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomersBank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomersAccountNumber = table.Column<int>(type: "int", nullable: false),
                    CompanyAccountPaidTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountPaid = table.Column<int>(type: "int", nullable: false),
                    PaymentImageFileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrderNo = table.Column<int>(type: "int", nullable: true),
                    PaymentForProcurement = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPayments_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderPayments_CustomerId",
                table: "OrderPayments",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderPayments");
        }
    }
}
