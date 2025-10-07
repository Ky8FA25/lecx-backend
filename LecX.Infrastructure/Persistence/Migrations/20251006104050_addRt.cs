using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LecX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addRt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Categories",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    TokenHash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    ExpiresAtUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedByIp = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    RevokedAtUtc = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    RevokedByIp = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    ReplacedByTokenId = table.Column<Guid>(type: "char(36)", nullable: true),
                    IsUsed = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Categories",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);
        }
    }
}
