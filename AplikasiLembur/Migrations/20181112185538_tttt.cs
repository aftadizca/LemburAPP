using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikasiLembur.Migrations
{
    public partial class tttt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LemburDetails_Tasks_TaskId",
                table: "LemburDetails");

            migrationBuilder.DropIndex(
                name: "IX_LemburDetails_TaskId",
                table: "LemburDetails");

            migrationBuilder.DropColumn(
                name: "End",
                table: "LemburDetails");

            migrationBuilder.DropColumn(
                name: "Plan",
                table: "LemburDetails");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "LemburDetails");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "LemburDetails");

            migrationBuilder.AlterColumn<string>(
                name: "Departement",
                table: "Lemburs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Employee",
                table: "Lemburs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Lemburs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Plan",
                table: "Lemburs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "Lemburs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Task",
                table: "LemburDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Employee",
                table: "Lemburs");

            migrationBuilder.DropColumn(
                name: "End",
                table: "Lemburs");

            migrationBuilder.DropColumn(
                name: "Plan",
                table: "Lemburs");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Lemburs");

            migrationBuilder.DropColumn(
                name: "Task",
                table: "LemburDetails");

            migrationBuilder.AlterColumn<string>(
                name: "Departement",
                table: "Lemburs",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "LemburDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Plan",
                table: "LemburDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "LemburDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
    }
}
