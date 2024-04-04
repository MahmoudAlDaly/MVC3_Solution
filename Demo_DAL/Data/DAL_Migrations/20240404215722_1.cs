using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo_DAL.Data.DAL_Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_Department_NavID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Department_NavID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Department_NavID",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Department_NavID",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Department_NavID",
                table: "Employees",
                column: "Department_NavID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_Department_NavID",
                table: "Employees",
                column: "Department_NavID",
                principalTable: "Departments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
