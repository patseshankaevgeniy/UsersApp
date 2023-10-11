using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                schema: "dbo",
                table: "Clients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "dbo",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
