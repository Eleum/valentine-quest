using Microsoft.EntityFrameworkCore.Migrations;

namespace Valentine.Persistence.Migrations
{
    public partial class GeoPointAreaPropsRenaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Position",
                table: "GeoPoints",
                newName: "AreaIndex");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AreaIndex",
                table: "GeoPoints",
                newName: "Position");
        }
    }
}
