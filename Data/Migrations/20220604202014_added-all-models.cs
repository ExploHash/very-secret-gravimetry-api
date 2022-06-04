using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gravimetry_api.Data.Migrations
{
    public partial class addedallmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteMonitor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Instance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteMonitor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isPublic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientSiteMonitor",
                columns: table => new
                {
                    ClientsId = table.Column<int>(type: "int", nullable: false),
                    SiteMonitorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSiteMonitor", x => new { x.ClientsId, x.SiteMonitorsId });
                    table.ForeignKey(
                        name: "FK_ClientSiteMonitor_Client_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientSiteMonitor_SiteMonitor_SiteMonitorsId",
                        column: x => x.SiteMonitorsId,
                        principalTable: "SiteMonitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incident",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    SiteMonitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incident", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incident_SiteMonitor_SiteMonitorId",
                        column: x => x.SiteMonitorId,
                        principalTable: "SiteMonitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UptimeMetric",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    SiteMonitorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UptimeMetric", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UptimeMetric_SiteMonitor_SiteMonitorId",
                        column: x => x.SiteMonitorId,
                        principalTable: "SiteMonitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserTeam",
                columns: table => new
                {
                    ApplicationUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeamsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserTeam", x => new { x.ApplicationUsersId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserTeam_AspNetUsers_ApplicationUsersId",
                        column: x => x.ApplicationUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserTeam_Team_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientTeam",
                columns: table => new
                {
                    ClientsId = table.Column<int>(type: "int", nullable: false),
                    TeamsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTeam", x => new { x.ClientsId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_ClientTeam_Client_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientTeam_Team_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteMonitorTeam",
                columns: table => new
                {
                    SiteMonitorsId = table.Column<int>(type: "int", nullable: false),
                    TeamsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteMonitorTeam", x => new { x.SiteMonitorsId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_SiteMonitorTeam_SiteMonitor_SiteMonitorsId",
                        column: x => x.SiteMonitorsId,
                        principalTable: "SiteMonitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteMonitorTeam_Team_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncidentNote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    IncidentId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncidentNote_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IncidentNote_Incident_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "Incident",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserTeam_TeamsId",
                table: "ApplicationUserTeam",
                column: "TeamsId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSiteMonitor_SiteMonitorsId",
                table: "ClientSiteMonitor",
                column: "SiteMonitorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientTeam_TeamsId",
                table: "ClientTeam",
                column: "TeamsId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_SiteMonitorId",
                table: "Incident",
                column: "SiteMonitorId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentNote_ApplicationUserId",
                table: "IncidentNote",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentNote_IncidentId",
                table: "IncidentNote",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteMonitorTeam_TeamsId",
                table: "SiteMonitorTeam",
                column: "TeamsId");

            migrationBuilder.CreateIndex(
                name: "IX_UptimeMetric_SiteMonitorId",
                table: "UptimeMetric",
                column: "SiteMonitorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserTeam");

            migrationBuilder.DropTable(
                name: "ClientSiteMonitor");

            migrationBuilder.DropTable(
                name: "ClientTeam");

            migrationBuilder.DropTable(
                name: "IncidentNote");

            migrationBuilder.DropTable(
                name: "SiteMonitorTeam");

            migrationBuilder.DropTable(
                name: "UptimeMetric");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Incident");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "SiteMonitor");
        }
    }
}
