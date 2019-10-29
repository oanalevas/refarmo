// <copyright>
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Oana Leva</author>

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace refarmo.Migrations
{
    public partial class AddGeoJsonSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeatureCollection",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    type = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureCollection", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MyGeometry",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    PolygonCoordinates = table.Column<Geometry>(nullable: true),
                    type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyGeometry", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Crs",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    type = table.Column<string>(nullable: true),
                    FeatureCollectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crs", x => x.id);
                    table.ForeignKey(
                        name: "FK_Crs_FeatureCollection_FeatureCollectionId",
                        column: x => x.FeatureCollectionId,
                        principalTable: "FeatureCollection",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    FeatureCollectionId = table.Column<int>(nullable: false),
                    Geometryid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feature_FeatureCollection_FeatureCollectionId",
                        column: x => x.FeatureCollectionId,
                        principalTable: "FeatureCollection",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feature_MyGeometry_Geometryid",
                        column: x => x.Geometryid,
                        principalTable: "MyGeometry",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    CrsId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.id);
                    table.ForeignKey(
                        name: "FK_Properties_Crs_CrsId",
                        column: x => x.CrsId,
                        principalTable: "Crs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeatureProperties",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    area = table.Column<int>(nullable: false),
                    FeatureId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureProperties", x => x.id);
                    table.ForeignKey(
                        name: "FK_FeatureProperties_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crs_FeatureCollectionId",
                table: "Crs",
                column: "FeatureCollectionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feature_FeatureCollectionId",
                table: "Feature",
                column: "FeatureCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Feature_Geometryid",
                table: "Feature",
                column: "Geometryid");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureProperties_FeatureId",
                table: "FeatureProperties",
                column: "FeatureId",
                unique: true,
                filter: "[FeatureId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CrsId",
                table: "Properties",
                column: "CrsId",
                unique: true,
                filter: "[CrsId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureProperties");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.DropTable(
                name: "Crs");

            migrationBuilder.DropTable(
                name: "MyGeometry");

            migrationBuilder.DropTable(
                name: "FeatureCollection");
        }
    }
}
