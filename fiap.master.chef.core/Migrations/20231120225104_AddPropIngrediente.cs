using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fiap.master.chef.core.Migrations
{
    /// <inheritdoc />
    public partial class AddPropIngrediente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Unidade",
                table: "Ingredientes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unidade",
                table: "Ingredientes");
        }
    }
}
