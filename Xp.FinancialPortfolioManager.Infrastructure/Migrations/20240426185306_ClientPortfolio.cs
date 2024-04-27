using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xp.FinancialPortfolioManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClientPortfolio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientPortfolio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClientId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    BoughtFor = table.Column<double>(type: "REAL", nullable: false),
                    TotalPrice = table.Column<double>(type: "REAL", nullable: false),
                    BoughtAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPortfolio", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientPortfolio");
        }
    }
}
