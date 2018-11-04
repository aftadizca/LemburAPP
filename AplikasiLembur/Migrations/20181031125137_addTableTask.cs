using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikasiLembur.Migrations
{
    public partial class addTableTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_karyawans_AspNetUsers_UserId",
                table: "karyawans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_karyawans",
                table: "karyawans");

            migrationBuilder.RenameTable(
                name: "karyawans",
                newName: "Karyawans");

            migrationBuilder.RenameIndex(
                name: "IX_karyawans_UserId",
                table: "Karyawans",
                newName: "IX_Karyawans_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Karyawans",
                table: "Karyawans",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Task = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_UserId",
                table: "Task",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Karyawans_AspNetUsers_UserId",
                table: "Karyawans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Karyawans_AspNetUsers_UserId",
                table: "Karyawans");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Karyawans",
                table: "Karyawans");

            migrationBuilder.RenameTable(
                name: "Karyawans",
                newName: "karyawans");

            migrationBuilder.RenameIndex(
                name: "IX_Karyawans_UserId",
                table: "karyawans",
                newName: "IX_karyawans_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_karyawans",
                table: "karyawans",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_karyawans_AspNetUsers_UserId",
                table: "karyawans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
