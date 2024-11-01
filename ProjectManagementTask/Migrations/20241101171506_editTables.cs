using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementTask.Migrations
{
    /// <inheritdoc />
    public partial class editTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_CreatedById",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_CreatedById",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_LastModifiedById",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatedById",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_LastModifiedById",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CreatedById",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_LastModifiedById",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CreatedById",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_LastModifiedById",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CreatedById",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Comments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedById",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedById",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreatedById",
                table: "Tasks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_LastModifiedById",
                table: "Tasks",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedById",
                table: "Projects",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_LastModifiedById",
                table: "Projects",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CreatedById",
                table: "Comments",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_CreatedById",
                table: "Comments",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_CreatedById",
                table: "Projects",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_LastModifiedById",
                table: "Projects",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatedById",
                table: "Tasks",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_LastModifiedById",
                table: "Tasks",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
