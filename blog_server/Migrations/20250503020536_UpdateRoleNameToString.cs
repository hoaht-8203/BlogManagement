using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace blog_server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoleNameToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") });

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 5, 3, 2, 5, 35, 957, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 5, 3, 2, 5, 35, 957, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 5, 3, 2, 5, 35, 957, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 5, 3, 2, 5, 35, 957, DateTimeKind.Utc).AddTicks(6420) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "address", "avatar_url", "create_by", "create_date", "email", "fullname", "is_email_verified", "password_hash", "phone", "status", "update_by", "update_date", "username" },
                values: new object[,]
                {
                    { new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"), null, null, null, new DateTime(2025, 4, 6, 11, 50, 59, 119, DateTimeKind.Utc).AddTicks(4270), "user@example.com", null, false, "sjd3BFGDYs6ZZFa38g7GP/QF9YnEH7UbZ+u9Oar2vsWowgy0gcsl8qbKXbAdXiG30HvHWIq/SytaOxOAIVJCRRJqYsXZItfOf0NZJ4r51e8=", null, 1, null, new DateTime(2025, 4, 6, 11, 50, 59, 119, DateTimeKind.Utc).AddTicks(4270), "user" },
                    { new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"), null, null, null, new DateTime(2025, 4, 6, 11, 50, 59, 119, DateTimeKind.Utc).AddTicks(4270), "admin@example.com", null, false, "M/k8JoxBwErHOPdd3BYnpY4sVcXXMJuFl4xcCAlw5kDkJsGJLscjzRSdZXcmG/HMn0HaoUOm46RO0hG+GEP309cuE1QBoGp5q5ynL7PAfSg=", null, 1, null, new DateTime(2025, 4, 6, 11, 50, 59, 119, DateTimeKind.Utc).AddTicks(4270), "admin" }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "role_id", "user_id", "join_date" },
                values: new object[,]
                {
                    { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"), new DateTime(2025, 4, 6, 11, 50, 59, 119, DateTimeKind.Utc).AddTicks(4270) },
                    { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"), new DateTime(2025, 4, 6, 11, 50, 59, 119, DateTimeKind.Utc).AddTicks(4270) }
                });
        }
    }
}
