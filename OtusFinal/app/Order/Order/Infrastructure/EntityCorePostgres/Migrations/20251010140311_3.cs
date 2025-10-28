using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityCorePostgres.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_date",
                table: "order_event");

            migrationBuilder.RenameColumn(
                name: "json",
                table: "order_event",
                newName: "event_type");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "order_event",
                newName: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "aggregate_id",
                table: "order_event",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "aggregate_type",
                table: "order_event",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "data",
                table: "order_event",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "event_id",
                table: "order_event",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "aggregate_id",
                table: "order_event");

            migrationBuilder.DropColumn(
                name: "aggregate_type",
                table: "order_event");

            migrationBuilder.DropColumn(
                name: "data",
                table: "order_event");

            migrationBuilder.DropColumn(
                name: "event_id",
                table: "order_event");

            migrationBuilder.RenameColumn(
                name: "event_type",
                table: "order_event",
                newName: "json");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "order_event",
                newName: "OrderId");

            migrationBuilder.AddColumn<DateTime>(
                name: "order_date",
                table: "order_event",
                type: "timestamp(6) without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
