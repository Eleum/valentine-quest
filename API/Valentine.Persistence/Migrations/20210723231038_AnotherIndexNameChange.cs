using Microsoft.EntityFrameworkCore.Migrations;

namespace Valentine.Persistence.Migrations
{
    public partial class AnotherIndexNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AreaIndex",
                table: "GeoPoints",
                newName: "Index");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Index",
                table: "GeoPoints",
                newName: "AreaIndex");
        }
    }
}
