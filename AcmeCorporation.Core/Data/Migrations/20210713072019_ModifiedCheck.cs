using Microsoft.EntityFrameworkCore.Migrations;

namespace AcmeCorporation.Data.Migrations
{
    public partial class ModifiedCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE UserSerials ADD CONSTRAINT CK_UserSerial_Uses CHECK (0 < [Uses] AND [Uses] <= 2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE UserSerials DROP CONSTRAINT CK_UserSerial_Uses");
        }
    }
}
