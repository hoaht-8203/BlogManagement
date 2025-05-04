using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_server.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCreateByToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "update_by",
                table: "users",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "create_by",
                table: "users",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "update_by",
                table: "roles",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "create_by",
                table: "roles",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "update_by",
                table: "categories",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "create_by",
                table: "categories",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_by", "create_date", "update_by", "update_date" },
                values: new object[] { null, new DateTime(2025, 5, 4, 0, 14, 12, 822, DateTimeKind.Utc).AddTicks(2200), null, new DateTime(2025, 5, 4, 0, 14, 12, 822, DateTimeKind.Utc).AddTicks(2200) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_by", "create_date", "update_by", "update_date" },
                values: new object[] { null, new DateTime(2025, 5, 4, 0, 14, 12, 822, DateTimeKind.Utc).AddTicks(2200), null, new DateTime(2025, 5, 4, 0, 14, 12, 822, DateTimeKind.Utc).AddTicks(2200) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 5, 4, 0, 14, 12, 822, DateTimeKind.Utc).AddTicks(2200));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 5, 4, 0, 14, 12, 822, DateTimeKind.Utc).AddTicks(2200));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_by", "create_date", "password_hash", "update_by", "update_date" },
                values: new object[] { null, new DateTime(2025, 5, 4, 0, 14, 12, 822, DateTimeKind.Utc).AddTicks(2200), "n+5v26OcaAzEgsRJlyVog5jD9gSS/TkMEVTrZFU9SEMDK/4qCWOAOFlVtK9dTJmQAy0d326n+lOzD2dhwQgOubQbhZ8A9hxy5151G8Da5G0=", null, new DateTime(2025, 5, 4, 0, 14, 12, 822, DateTimeKind.Utc).AddTicks(2200) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_by", "create_date", "password_hash", "update_by", "update_date" },
                values: new object[] { null, new DateTime(2025, 5, 4, 0, 14, 12, 822, DateTimeKind.Utc).AddTicks(2200), "J17F2TwefZ9YoBJxO+8xywNbSUYqN2TYQPyAP9+dF1DG6GwXd7a3py9PBChn3Nx9AzhPRcTy83poT54Bf69gYPqjXO8v4nuPSmeoBPMRmQo=", null, new DateTime(2025, 5, 4, 0, 14, 12, 822, DateTimeKind.Utc).AddTicks(2200) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "update_by",
                table: "users",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "create_by",
                table: "users",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "update_by",
                table: "roles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "create_by",
                table: "roles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "update_by",
                table: "categories",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "create_by",
                table: "categories",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_by", "create_date", "update_by", "update_date" },
                values: new object[] { null, new DateTime(2025, 5, 3, 4, 41, 20, 433, DateTimeKind.Utc).AddTicks(4150), null, new DateTime(2025, 5, 3, 4, 41, 20, 433, DateTimeKind.Utc).AddTicks(4150) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_by", "create_date", "update_by", "update_date" },
                values: new object[] { null, new DateTime(2025, 5, 3, 4, 41, 20, 433, DateTimeKind.Utc).AddTicks(4150), null, new DateTime(2025, 5, 3, 4, 41, 20, 433, DateTimeKind.Utc).AddTicks(4150) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 5, 3, 4, 41, 20, 433, DateTimeKind.Utc).AddTicks(4150));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 5, 3, 4, 41, 20, 433, DateTimeKind.Utc).AddTicks(4150));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_by", "create_date", "password_hash", "update_by", "update_date" },
                values: new object[] { null, new DateTime(2025, 5, 3, 4, 41, 20, 433, DateTimeKind.Utc).AddTicks(4150), "j1oSvoeWbXOHJQ19NNKpKJOwu/0kC9Cl/CZL2ixXNR2R4Px3cZ7WdPmQoAbeoX6BgbznKRe2lGnhcs2wYP6I01L3+uXt50y5se4Orpp+qkE=", null, new DateTime(2025, 5, 3, 4, 41, 20, 433, DateTimeKind.Utc).AddTicks(4150) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_by", "create_date", "password_hash", "update_by", "update_date" },
                values: new object[] { null, new DateTime(2025, 5, 3, 4, 41, 20, 433, DateTimeKind.Utc).AddTicks(4150), "ZJWtTk8vsxR3Aej/95bJJFPZcgFTXYDiXBQjMSpgNL5S5Bi1vFfFZnBWftS5M+LkQmDjnmklwmzvEOt7/StBXMDS9xYNvnLVCWMH0YN3fyA=", null, new DateTime(2025, 5, 3, 4, 41, 20, 433, DateTimeKind.Utc).AddTicks(4150) });
        }
    }
}
