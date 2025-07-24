using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VBT_proje.Migrations
{
    public partial class InitialCreate : Migration
    {
        // This method is called to apply the migration
        protected override void Up(MigrationBuilder migrationBuilder)
        {   
            // Creates the Users table with relevant columns
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {   
            // If migration is rolled back, drops the Users table
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
