using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VexIT.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<short>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    Country = table.Column<string>(maxLength: 512, nullable: true),
                    City = table.Column<string>(maxLength: 512, nullable: true),
                    Street = table.Column<string>(maxLength: 512, nullable: true),
                    Place = table.Column<string>(maxLength: 512, nullable: true),
                    YouTubeUrl = table.Column<string>(maxLength: 512, nullable: true),
                    Description = table.Column<string>(maxLength: 2048, nullable: true),
                    ScheduledAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    EventId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_EventId",
                table: "Photos",
                column: "EventId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
