using Microsoft.EntityFrameworkCore.Migrations;

namespace AcmeCorporation.Core.Migrations
{
    public partial class removedUserSerialId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserSerials");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserSerials",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
