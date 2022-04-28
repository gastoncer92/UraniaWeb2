using Microsoft.EntityFrameworkCore.Migrations;

namespace UraniaWeb.Migrations
{
    public partial class hhhffff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Article_ArticleIdAticle",
                table: "Article");

            migrationBuilder.RenameColumn(
                name: "ArticleIdAticle",
                table: "Article",
                newName: "articleIdAticle");

            migrationBuilder.RenameIndex(
                name: "IX_Article_ArticleIdAticle",
                table: "Article",
                newName: "IX_Article_articleIdAticle");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Article_articleIdAticle",
                table: "Article",
                column: "articleIdAticle",
                principalTable: "Article",
                principalColumn: "IdAticle",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Article_articleIdAticle",
                table: "Article");

            migrationBuilder.RenameColumn(
                name: "articleIdAticle",
                table: "Article",
                newName: "ArticleIdAticle");

            migrationBuilder.RenameIndex(
                name: "IX_Article_articleIdAticle",
                table: "Article",
                newName: "IX_Article_ArticleIdAticle");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Article_ArticleIdAticle",
                table: "Article",
                column: "ArticleIdAticle",
                principalTable: "Article",
                principalColumn: "IdAticle",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
