using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitoringStationAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTriggerThresholdWarningField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TriggerThresholdWarning",
                table: "TemperatureData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TriggerThresholdWarning",
                table: "RainfallData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TriggerThresholdWarning",
                table: "HumidityData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TriggerThresholdWarning",
                table: "CO2EmissionsData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TriggerThresholdWarning",
                table: "AirPollutionData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TriggerThresholdWarning",
                table: "TemperatureData");

            migrationBuilder.DropColumn(
                name: "TriggerThresholdWarning",
                table: "RainfallData");

            migrationBuilder.DropColumn(
                name: "TriggerThresholdWarning",
                table: "HumidityData");

            migrationBuilder.DropColumn(
                name: "TriggerThresholdWarning",
                table: "CO2EmissionsData");

            migrationBuilder.DropColumn(
                name: "TriggerThresholdWarning",
                table: "AirPollutionData");
        }
    }
}
