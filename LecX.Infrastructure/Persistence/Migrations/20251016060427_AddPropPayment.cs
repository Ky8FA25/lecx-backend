using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LecX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPropPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProfileImagePath",
                table: "Users",
                type: "varchar(512)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(512)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dob",
                table: "Users",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "longtext",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "CheckoutUrl",
                table: "Payments",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Payments",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GatewayTransactionId",
                table: "Payments",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderCode",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Comments",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEdited",
                table: "Comments",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckoutUrl",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "GatewayTransactionId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "OrderCode",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "IsEdited",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImagePath",
                table: "Users",
                type: "varchar(512)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(512)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Users",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dob",
                table: "Users",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "longtext",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}
