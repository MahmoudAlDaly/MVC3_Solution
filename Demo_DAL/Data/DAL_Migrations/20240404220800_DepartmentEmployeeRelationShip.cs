using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo_DAL.Data.DAL_Migrations
{
    public partial class DepartmentEmployeeRelationShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Department_ID",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Department_ID",
                table: "Employees",
                column: "Department_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_Department_ID",
                table: "Employees",
                column: "Department_ID",
                principalTable: "Departments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_Department_ID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Department_ID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Department_ID",
                table: "Employees");
        }
    }
}
