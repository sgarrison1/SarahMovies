using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SarahMovies.Data.Migrations
{
    public partial class newActors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieID",
                table: "Actor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actor_MovieID",
                table: "Actor",
                column: "MovieID");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Movie_MovieID",
                table: "Actor",
                column: "MovieID",
                principalTable: "Movie",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Movie_MovieID",
                table: "Actor");

            migrationBuilder.DropIndex(
                name: "IX_Actor_MovieID",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "MovieID",
                table: "Actor");
        }
    }
}
