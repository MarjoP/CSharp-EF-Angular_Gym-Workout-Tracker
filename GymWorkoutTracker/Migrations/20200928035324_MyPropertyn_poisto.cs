using Microsoft.EntityFrameworkCore.Migrations;

namespace GymWorkoutTracker.Migrations
{
    public partial class MyPropertyn_poisto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Results");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
