using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlinkCash.Data.Migrations
{
    public partial class WithDrawalSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InitialAmountSaved = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FrequentAmountSaved = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmountSaved = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalInterestAccrued = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tenor = table.Column<int>(type: "int", nullable: false),
                    MaturityDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DailyInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaystackTransactionRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateForDebit = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SavingsType = table.Column<int>(type: "int", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    DebitFrequency = table.Column<int>(type: "int", nullable: false),
                    IsNonInterest = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WithDrawalSetting",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithDrawalSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WithDrawalSetting_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WithDrawalSetting_BankId",
                table: "WithDrawalSetting",
                column: "BankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanHistory");

            migrationBuilder.DropTable(
                name: "WithDrawalSetting");
        }
    }
}
