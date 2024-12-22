using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairdresserApp.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailAndPasswordToWorker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Workers");
        }
    }
}
