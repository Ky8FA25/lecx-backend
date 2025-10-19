using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LecX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCertInx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Certificates_StudentId",
                table: "Certificates");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_StudentId_CourseId",
                table: "Certificates",
                columns: new[] { "StudentId", "CourseId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Certificates_StudentId_CourseId",
                table: "Certificates");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_StudentId",
                table: "Certificates",
                column: "StudentId");
        }
    }
}
