using Microsoft.EntityFrameworkCore.Migrations;

namespace BlinkCash.Data.Migrations
{
    public partial class SavingAndLoanConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Config = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanConfiguration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SavingsConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Config = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingsConfiguration", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanConfiguration");

            migrationBuilder.DropTable(
                name: "SavingsConfiguration");
        }
    }
}
