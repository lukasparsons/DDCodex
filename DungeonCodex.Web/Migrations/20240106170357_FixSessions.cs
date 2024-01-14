using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DungeonCodex.Web.Migrations
{
    /// <inheritdoc />
    public partial class FixSessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Sessions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CharacterId",
                table: "Characters",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CampaignId",
                table: "Campaigns",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CampaignParticipantId",
                table: "CampaignParticipants",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Sessions",
                newName: "SessionId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Characters",
                newName: "CharacterId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Campaigns",
                newName: "CampaignId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CampaignParticipants",
                newName: "CampaignParticipantId");
        }
    }
}
