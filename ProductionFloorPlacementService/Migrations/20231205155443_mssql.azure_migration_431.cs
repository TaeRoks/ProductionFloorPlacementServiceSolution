using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductionFloorPlacementService.Migrations
{
    /// <inheritdoc />
    public partial class mssqlazure_migration_431 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductionFloorModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionFloorModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnologicalEquipmentModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnologicalEquipmentModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlacementContractModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionFloorModelId = table.Column<int>(type: "int", nullable: false),
                    TechnologicalEquipmentModelId = table.Column<int>(type: "int", nullable: false),
                    TechnologicalEquipmentQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacementContractModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlacementContractModel_ProductionFloorModel_ProductionFloorModelId",
                        column: x => x.ProductionFloorModelId,
                        principalTable: "ProductionFloorModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlacementContractModel_TechnologicalEquipmentModel_TechnologicalEquipmentModelId",
                        column: x => x.TechnologicalEquipmentModelId,
                        principalTable: "TechnologicalEquipmentModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlacementContractModel_ProductionFloorModelId",
                table: "PlacementContractModel",
                column: "ProductionFloorModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacementContractModel_TechnologicalEquipmentModelId",
                table: "PlacementContractModel",
                column: "TechnologicalEquipmentModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlacementContractModel");

            migrationBuilder.DropTable(
                name: "ProductionFloorModel");

            migrationBuilder.DropTable(
                name: "TechnologicalEquipmentModel");
        }
    }
}
