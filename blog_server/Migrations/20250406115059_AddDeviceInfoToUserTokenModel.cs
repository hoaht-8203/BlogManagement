using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_server.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceInfoToUserTokenModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "refresh_token",
                table: "users");

            migrationBuilder.DropColumn(
                name: "refresh_token_expiry_time",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "browser",
                table: "user_tokens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "device_type",
                table: "user_tokens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "os",
                table: "user_tokens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "user_tokens",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 4, 6, 11, 50, 59, 119, DateTimeKind.Utc).AddTicks(4270), new DateTime(2025, 4, 6, 11, 50, 59, 119, DateTimeKind.Utc).AddTicks(4270) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 4, 6, 11, 50, 59, 119, DateTimeKind.Utc).AddTicks(4270), new DateTime(2025, 4, 6, 11, 50, 59, 119, DateTimeKind.Utc).AddTicks(4270) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 4, 6, 11, 50, 59, 119, DateTimeKind.Utc).AddTicks(4270));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 4, 6, 11, 50, 59, 119, DateTimeKind.Utc).AddTicks(4270));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 4, 6, 11, 50, 59, 119, DateTimeKind.Utc).AddTicks(4270), "sjd3BFGDYs6ZZFa38g7GP/QF9YnEH7UbZ+u9Oar2vsWowgy0gcsl8qbKXbAdXiG30HvHWIq/SytaOxOAIVJCRRJqYsXZItfOf0NZJ4r51e8=", new DateTime(2025, 4, 6, 11, 50, 59, 119, DateTimeKind.Utc).AddTicks(4270) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 4, 6, 11, 50, 59, 119, DateTimeKind.Utc).AddTicks(4270), "M/k8JoxBwErHOPdd3BYnpY4sVcXXMJuFl4xcCAlw5kDkJsGJLscjzRSdZXcmG/HMn0HaoUOm46RO0hG+GEP309cuE1QBoGp5q5ynL7PAfSg=", new DateTime(2025, 4, 6, 11, 50, 59, 119, DateTimeKind.Utc).AddTicks(4270) });

            migrationBuilder.CreateIndex(
                name: "IX_user_tokens_user_id",
                table: "user_tokens",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_tokens_users_user_id",
                table: "user_tokens",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_tokens_users_user_id",
                table: "user_tokens");

            migrationBuilder.DropIndex(
                name: "IX_user_tokens_user_id",
                table: "user_tokens");

            migrationBuilder.DropColumn(
                name: "browser",
                table: "user_tokens");

            migrationBuilder.DropColumn(
                name: "device_type",
                table: "user_tokens");

            migrationBuilder.DropColumn(
                name: "os",
                table: "user_tokens");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "user_tokens");

            migrationBuilder.AddColumn<string>(
                name: "refresh_token",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "refresh_token_expiry_time",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 4, 1, 19, 48, 3, 430, DateTimeKind.Utc).AddTicks(1890), new DateTime(2025, 4, 1, 19, 48, 3, 430, DateTimeKind.Utc).AddTicks(1890) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 4, 1, 19, 48, 3, 430, DateTimeKind.Utc).AddTicks(1890), new DateTime(2025, 4, 1, 19, 48, 3, 430, DateTimeKind.Utc).AddTicks(1890) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 4, 1, 19, 48, 3, 430, DateTimeKind.Utc).AddTicks(1890));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 4, 1, 19, 48, 3, 430, DateTimeKind.Utc).AddTicks(1890));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_date", "password_hash", "refresh_token", "refresh_token_expiry_time", "update_date" },
                values: new object[] { new DateTime(2025, 4, 1, 19, 48, 3, 430, DateTimeKind.Utc).AddTicks(1890), "C/ADJqAB6Hoa1xeZhNfkM3BjFob5whcjbpWgcjjNT0xIK6IYrziBvbCI2Bmum9KEcGZH1kQFSWOYmeyHQ+dT7a6WfO66ZhSgpnMnFRdo0Uc=", null, null, new DateTime(2025, 4, 1, 19, 48, 3, 430, DateTimeKind.Utc).AddTicks(1890) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_date", "password_hash", "refresh_token", "refresh_token_expiry_time", "update_date" },
                values: new object[] { new DateTime(2025, 4, 1, 19, 48, 3, 430, DateTimeKind.Utc).AddTicks(1890), "uwscY7QZjIHLQkqmFt21LKx+5X8dYyfnx2MjjJGBu7VeDkstHvZ/opJiDktoXNKA36BEclYygKuXQnc/BSig7magx8ZQFKb9oiwjGNJ5zjs=", null, null, new DateTime(2025, 4, 1, 19, 48, 3, 430, DateTimeKind.Utc).AddTicks(1890) });
        }
    }
}
