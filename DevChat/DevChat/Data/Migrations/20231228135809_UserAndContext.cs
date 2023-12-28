using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevChat.Migrations
{
    /// <inheritdoc />
    public partial class UserAndContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammableContents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Js = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Css = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Html = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LibrariesCsv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammableContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConversationMembers",
                columns: table => new
                {
                    ConvId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationMembers", x => new { x.ConvId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ConversationMembers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConversationMembers_Conversations_ConvId",
                        column: x => x.ConvId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FromUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConvId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Conversations_ConvId",
                        column: x => x.ConvId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_ProgrammableContents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "ProgrammableContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConversationMembers_UserId",
                table: "ConversationMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ContentId",
                table: "Messages",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConvId",
                table: "Messages",
                column: "ConvId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromUserId",
                table: "Messages",
                column: "FromUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConversationMembers");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "ProgrammableContents");
        }
    }
}
