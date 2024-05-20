using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitoringStationAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSensorRainfallHumidityCO2AirPollutionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCollectionIntervals",
                table: "TemperatureData");

            migrationBuilder.DropColumn(
                name: "DataRange",
                table: "TemperatureData");

            migrationBuilder.DropColumn(
                name: "NormalThreshold",
                table: "TemperatureData");

            migrationBuilder.DropColumn(
                name: "NumberOfSensors",
                table: "TemperatureData");

            migrationBuilder.DropColumn(
                name: "SensorName",
                table: "TemperatureData");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "TemperatureData");

            migrationBuilder.AlterColumn<float>(
                name: "Temperature",
                table: "TemperatureData",
                type: "REAL",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SensorId",
                table: "TemperatureData",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "TemperatureData",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "AirPollutionData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SensorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AirPollution = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirPollutionData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CO2EmissionsData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SensorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CO2Emissions = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CO2EmissionsData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HumidityData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SensorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Humidity = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumidityData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RainfallData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SensorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Rainfall = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RainfallData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sensor",
                columns: table => new
                {
                    SensorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParameterType = table.Column<string>(type: "TEXT", nullable: true),
                    DataCollectionInterval = table.Column<string>(type: "TEXT", nullable: true),
                    DataRangeMin = table.Column<float>(type: "REAL", nullable: false),
                    DataRangeMax = table.Column<float>(type: "REAL", nullable: false),
                    Unit = table.Column<string>(type: "TEXT", nullable: true),
                    NormalThresholdMin = table.Column<float>(type: "REAL", nullable: false),
                    NormalThresholdMax = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensor", x => x.SensorId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirPollutionData");

            migrationBuilder.DropTable(
                name: "CO2EmissionsData");

            migrationBuilder.DropTable(
                name: "HumidityData");

            migrationBuilder.DropTable(
                name: "RainfallData");

            migrationBuilder.DropTable(
                name: "Sensor");

            migrationBuilder.DropColumn(
                name: "SensorId",
                table: "TemperatureData");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "TemperatureData");

            migrationBuilder.AlterColumn<double>(
                name: "Temperature",
                table: "TemperatureData",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AddColumn<string>(
                name: "DataCollectionIntervals",
                table: "TemperatureData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataRange",
                table: "TemperatureData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalThreshold",
                table: "TemperatureData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberOfSensors",
                table: "TemperatureData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SensorName",
                table: "TemperatureData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "TemperatureData",
                type: "TEXT",
                nullable: true);
        }
    }
}
