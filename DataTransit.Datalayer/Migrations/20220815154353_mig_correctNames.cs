using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataTransit.Datalayer.Migrations
{
    public partial class mig_correctNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ExelModels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "ExelModels",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
