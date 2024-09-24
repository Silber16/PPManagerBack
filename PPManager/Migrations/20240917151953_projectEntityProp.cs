using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PPManager.Migrations
{
    /// <inheritdoc />
    public partial class projectEntityProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Techs",
                table: "Project");

            migrationBuilder.AlterColumn<string>(
                name: "Desc",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Desc",
                table: "Project",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Techs",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
