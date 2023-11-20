using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fiap.master.chef.core.Migrations
{
    /// <inheritdoc />
    public partial class AddPropReceita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Receitas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Receitas");
        }
    }
}
