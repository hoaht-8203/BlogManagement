using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace blog_server.Migrations
{
    /// <inheritdoc />
    public partial class FixAndAddInitData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Administrator role", "Admin" },
                    { 2, "User role", "User" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Email", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "Status", "Username" },
                values: new object[,]
                {
                    { new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"), "user@example.com", "user", null, null, 1, "user" },
                    { new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"), "admin@example.com", "admin", null, null, 1, "admin" }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "RoleId", "UserId", "JoinDate" },
                values: new object[,]
                {
                    { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"), new DateTime(2025, 3, 16, 10, 34, 35, 874, DateTimeKind.Utc).AddTicks(3580) },
                    { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"), new DateTime(2025, 3, 16, 10, 34, 35, 874, DateTimeKind.Utc).AddTicks(3580) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") });

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"));
        }
    }
}
