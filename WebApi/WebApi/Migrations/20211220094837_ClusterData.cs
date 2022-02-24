using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class ClusterData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClusterData",
                columns: table => new
                {
                    UrlId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClusterData", x => x.UrlId);
                });

            migrationBuilder.CreateTable(
                name: "ExtendedCluster",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClusterDataUrlId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClusterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtendedCluster", x => x.id);
                    table.ForeignKey(
                        name: "FK_ExtendedCluster_Cluster_ClusterId",
                        column: x => x.ClusterId,
                        principalTable: "Cluster",
                        principalColumn: "ClusterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtendedCluster_ClusterData_ClusterDataUrlId",
                        column: x => x.ClusterDataUrlId,
                        principalTable: "ClusterData",
                        principalColumn: "UrlId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtendedCluster_ClusterDataUrlId",
                table: "ExtendedCluster",
                column: "ClusterDataUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtendedCluster_ClusterId",
                table: "ExtendedCluster",
                column: "ClusterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtendedCluster");

            migrationBuilder.DropTable(
                name: "ClusterData");
        }
    }
}
