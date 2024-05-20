using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitoringStationAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixedco2emissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CO2Emissions",
                table: "CO2EmissionsData",
                newName: "CO2EmissionsValue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CO2EmissionsValue",
                table: "CO2EmissionsData",
                newName: "CO2Emissions");
        }
    }
}
