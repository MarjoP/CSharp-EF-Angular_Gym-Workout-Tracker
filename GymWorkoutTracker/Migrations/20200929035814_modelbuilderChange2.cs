using Microsoft.EntityFrameworkCore.Migrations;

namespace GymWorkoutTracker.Migrations
{
    public partial class modelbuilderChange2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Exercises_ExerciseId",
                table: "Results");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Exercises_ExerciseId",
                table: "Results",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "ExerciseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Exercises_ExerciseId",
                table: "Results");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Exercises_ExerciseId",
                table: "Results",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "ExerciseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
