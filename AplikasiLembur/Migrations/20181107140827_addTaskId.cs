using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikasiLembur.Migrations
{
    public partial class addTaskId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "LemburDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LemburDetails_TaskId",
                table: "LemburDetails",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_LemburDetails_Tasks_TaskId",
                table: "LemburDetails",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LemburDetails_Tasks_TaskId",
                table: "LemburDetails");

            migrationBuilder.DropIndex(
                name: "IX_LemburDetails_TaskId",
                table: "LemburDetails");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "LemburDetails");
        }
    }
}
