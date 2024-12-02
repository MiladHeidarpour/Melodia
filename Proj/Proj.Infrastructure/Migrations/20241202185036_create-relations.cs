using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proj.Infrastructure.Migrations
{
    public partial class createrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE [dbo].Musics
                ADD CONSTRAINT FK_Music_MusicAlbum_AlbumId
                FOREIGN KEY (AlbumId) REFERENCES [dbo].MusicAlbums(Id);");

            migrationBuilder.Sql(@"
                ALTER TABLE [Musics].ArtistMusics
                ADD CONSTRAINT FK_ArtistMusics_Artist_ArtistId
                FOREIGN KEY (ArtistId) REFERENCES [dbo].Artists(Id);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
