using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proj.Infrastructure.Migrations
{
    public partial class edit_timespan_To_string : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TrackTime",
                schema: "dbo",
                table: "Musics",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "TrackTime",
                schema: "dbo",
                table: "Musics",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
