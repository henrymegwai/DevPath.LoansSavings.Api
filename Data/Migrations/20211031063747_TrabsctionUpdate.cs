using Microsoft.EntityFrameworkCore.Migrations;

namespace BlinkCash.Data.Migrations
{
    public partial class TrabsctionUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlanId",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Transactions");
        }
    }
}
