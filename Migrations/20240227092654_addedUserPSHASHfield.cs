using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskSchedulerAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedUserPSHASHfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "PasswordSalt");

            migrationBuilder.RenameColumn(
                name: "DueUpdatedDate",
                table: "TaskAssignments",
                newName: "DueDate");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "TaskAssignments",
                newName: "DueUpdatedDate");
        }
    }
}
