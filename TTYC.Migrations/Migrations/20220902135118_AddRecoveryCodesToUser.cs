using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTYC.Migrations.Migrations
{
    public partial class AddRecoveryCodesToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsReseted",
                table: "Users",
                newName: "IsPasswordReseted");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumder",
                table: "RecoveryCodes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsActivated",
                table: "RecoveryCodes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_RecoveryCodes_PhoneNumder",
                table: "RecoveryCodes",
                column: "PhoneNumder");

            migrationBuilder.AddForeignKey(
                name: "FK_RecoveryCodes_Users_PhoneNumder",
                table: "RecoveryCodes",
                column: "PhoneNumder",
                principalTable: "Users",
                principalColumn: "PhoneNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecoveryCodes_Users_PhoneNumder",
                table: "RecoveryCodes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_PhoneNumber",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_RecoveryCodes_PhoneNumder",
                table: "RecoveryCodes");

            migrationBuilder.DropColumn(
                name: "IsActivated",
                table: "RecoveryCodes");

            migrationBuilder.RenameColumn(
                name: "IsPasswordReseted",
                table: "Users",
                newName: "IsReseted");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumder",
                table: "RecoveryCodes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
