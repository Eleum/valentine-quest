using Microsoft.EntityFrameworkCore.Migrations;

namespace Valentine.Persistence.Migrations
{
    public partial class IsDefaultMap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Maps",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Maps");
        }
    }
}
