using Microsoft.EntityFrameworkCore.Migrations;

namespace BlinkCash.Data.Migrations
{
    public partial class PlanUpdateGoing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanStatus",
                table: "Plan",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanStatus",
                table: "Plan");
        }
    }
}
