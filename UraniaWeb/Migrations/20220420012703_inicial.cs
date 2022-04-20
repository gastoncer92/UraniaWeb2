using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UraniaWeb.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    IdAdmin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAdmin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PassAdmin = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.IdAdmin);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    IdAticle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleArticle = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    DescritionArticle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrlImagen1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlImagen2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlSound1 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.IdAticle);
                });

            migrationBuilder.CreateTable(
                name: "Slider",
                columns: table => new
                {
                    IdSlider = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleSlider = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    UrlSlider1 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slider", x => x.IdSlider);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Slider");
        }
    }
}
