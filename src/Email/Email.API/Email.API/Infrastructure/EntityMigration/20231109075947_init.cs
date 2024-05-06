using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Email.API.Infrastructure.EntityMigration
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CallName = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Email = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Remark = table.Column<string>(type: "longtext", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdateDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreateUser = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    UpdateUser = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Email");
        }
    }
}
