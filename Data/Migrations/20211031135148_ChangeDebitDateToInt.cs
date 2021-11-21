using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlinkCash.Data.Migrations
{
    public partial class ChangeDebitDateToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "DateForDebit",
            table: "PlanHistory");

            migrationBuilder.AddColumn<int>(
            name: "DateForDebit",
            table: "PlanHistory",
            nullable: true);
            
            migrationBuilder.DropColumn(
           name: "DateForDebit",
           table: "Plan");

            migrationBuilder.AddColumn<int>(
            name: "DateForDebit",
            table: "Plan",
            nullable: true);
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "DateForDebit",
            table: "PlanHistory");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateForDebit",
                table: "PlanHistory",
                nullable: true);
            migrationBuilder.DropColumn(
                        name: "DateForDebit",
                        table: "Plan");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateForDebit",
                table: "Plan",
                nullable: true);
        }
    }
}
