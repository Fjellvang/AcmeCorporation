using Microsoft.EntityFrameworkCore.Migrations;

namespace AcmeCorporation.Data.Migrations
{
    public partial class AddDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSerial_AspNetUsers_UserId",
                table: "UserSerial");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSerial_Serial_SerialId",
                table: "UserSerial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSerial",
                table: "UserSerial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Serial",
                table: "Serial");

            migrationBuilder.RenameTable(
                name: "UserSerial",
                newName: "UserSerials");

            migrationBuilder.RenameTable(
                name: "Serial",
                newName: "Serials");

            migrationBuilder.RenameIndex(
                name: "IX_UserSerial_UserId",
                table: "UserSerials",
                newName: "IX_UserSerials_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSerial_SerialId",
                table: "UserSerials",
                newName: "IX_UserSerials_SerialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSerials",
                table: "UserSerials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Serials",
                table: "Serials",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSerials_AspNetUsers_UserId",
                table: "UserSerials",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSerials_Serials_SerialId",
                table: "UserSerials",
                column: "SerialId",
                principalTable: "Serials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSerials_AspNetUsers_UserId",
                table: "UserSerials");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSerials_Serials_SerialId",
                table: "UserSerials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSerials",
                table: "UserSerials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Serials",
                table: "Serials");

            migrationBuilder.RenameTable(
                name: "UserSerials",
                newName: "UserSerial");

            migrationBuilder.RenameTable(
                name: "Serials",
                newName: "Serial");

            migrationBuilder.RenameIndex(
                name: "IX_UserSerials_UserId",
                table: "UserSerial",
                newName: "IX_UserSerial_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSerials_SerialId",
                table: "UserSerial",
                newName: "IX_UserSerial_SerialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSerial",
                table: "UserSerial",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Serial",
                table: "Serial",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSerial_AspNetUsers_UserId",
                table: "UserSerial",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSerial_Serial_SerialId",
                table: "UserSerial",
                column: "SerialId",
                principalTable: "Serial",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
