using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace McPos.Server.Data.Migrations
{
    public partial class M0002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contry",
                table: "Customers",
                newName: "County");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "County",
                table: "Customers",
                newName: "Contry");
        }
    }
}
