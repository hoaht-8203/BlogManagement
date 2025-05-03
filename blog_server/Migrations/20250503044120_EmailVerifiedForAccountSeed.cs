using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_server.Migrations
{
    /// <inheritdoc />
    public partial class EmailVerifiedForAccountSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 5, 3, 4, 41, 20, 433, DateTimeKind.Utc).AddTicks(4150), new DateTime(2025, 5, 3, 4, 41, 20, 433, DateTimeKind.Utc).AddTicks(4150) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 5, 3, 4, 41, 20, 433, DateTimeKind.Utc).AddTicks(4150), new DateTime(2025, 5, 3, 4, 41, 20, 433, DateTimeKind.Utc).AddTicks(4150) });

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
                columns: new[] { "create_date", "is_email_verified", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 5, 3, 4, 41, 20, 433, DateTimeKind.Utc).AddTicks(4150), true, "j1oSvoeWbXOHJQ19NNKpKJOwu/0kC9Cl/CZL2ixXNR2R4Px3cZ7WdPmQoAbeoX6BgbznKRe2lGnhcs2wYP6I01L3+uXt50y5se4Orpp+qkE=", new DateTime(2025, 5, 3, 4, 41, 20, 433, DateTimeKind.Utc).AddTicks(4150) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_date", "is_email_verified", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 5, 3, 4, 41, 20, 433, DateTimeKind.Utc).AddTicks(4150), true, "ZJWtTk8vsxR3Aej/95bJJFPZcgFTXYDiXBQjMSpgNL5S5Bi1vFfFZnBWftS5M+LkQmDjnmklwmzvEOt7/StBXMDS9xYNvnLVCWMH0YN3fyA=", new DateTime(2025, 5, 3, 4, 41, 20, 433, DateTimeKind.Utc).AddTicks(4150) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 5, 3, 4, 40, 21, 203, DateTimeKind.Utc).AddTicks(1080), new DateTime(2025, 5, 3, 4, 40, 21, 203, DateTimeKind.Utc).AddTicks(1080) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 5, 3, 4, 40, 21, 203, DateTimeKind.Utc).AddTicks(1080), new DateTime(2025, 5, 3, 4, 40, 21, 203, DateTimeKind.Utc).AddTicks(1080) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 5, 3, 4, 40, 21, 203, DateTimeKind.Utc).AddTicks(1080));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 5, 3, 4, 40, 21, 203, DateTimeKind.Utc).AddTicks(1080));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_date", "is_email_verified", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 5, 3, 4, 40, 21, 203, DateTimeKind.Utc).AddTicks(1080), false, "/tok7BnPlKW6vZ4LYBcB7cDl4O8Ixl3lWrNQXWXreAjuns+LtE+C7qMpDBfNAjzjIzgUi0jq5RnurRBtgS0tCywxKa6sC4o5HKFQ5NdhZaE=", new DateTime(2025, 5, 3, 4, 40, 21, 203, DateTimeKind.Utc).AddTicks(1080) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_date", "is_email_verified", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 5, 3, 4, 40, 21, 203, DateTimeKind.Utc).AddTicks(1080), false, "04UzNdiuV3KcLwH/aQqLE+/BK2IfLjkka+6YxVFI0GoSjlyC+gP5AXAN1QFUmL55ZxLEU0CAPCGVwAKvbtdnVybff+VsS5osQnVZhKBClGc=", new DateTime(2025, 5, 3, 4, 40, 21, 203, DateTimeKind.Utc).AddTicks(1080) });
        }
    }
}
