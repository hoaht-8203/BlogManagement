using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace blog_server.Migrations
{
    /// <inheritdoc />
    public partial class AddResetPasswordModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PasswordResets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    ExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResets", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 29, 13, 45, 33, 854, DateTimeKind.Utc).AddTicks(1910), new DateTime(2025, 3, 29, 13, 45, 33, 854, DateTimeKind.Utc).AddTicks(1910) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 29, 13, 45, 33, 854, DateTimeKind.Utc).AddTicks(1910), new DateTime(2025, 3, 29, 13, 45, 33, 854, DateTimeKind.Utc).AddTicks(1910) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 3, 29, 13, 45, 33, 854, DateTimeKind.Utc).AddTicks(1910));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 3, 29, 13, 45, 33, 854, DateTimeKind.Utc).AddTicks(1910));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 29, 13, 45, 33, 854, DateTimeKind.Utc).AddTicks(1910), "McM6F6gfJLegcOQOE4VbU20pMVQe1EDydRNMJ9LQdtei8WJ+nOLDLTLbS+mUYQl2ERJEoiNFJMjsoH9qsumWTXYQWHPYZ0rRfaO0FV40dX8=", new DateTime(2025, 3, 29, 13, 45, 33, 854, DateTimeKind.Utc).AddTicks(1910) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 29, 13, 45, 33, 854, DateTimeKind.Utc).AddTicks(1910), "RArQ83Fy5XErGWvHWIRDeWBuP5BLaJVlTxpp1Llq8jaG8XGCUzfQe3RoeU8pYQ0hmSuTzoV7CxcxXF6cDBCG+vu/+sSsFkMZ/8rWhTKT+58=", new DateTime(2025, 3, 29, 13, 45, 33, 854, DateTimeKind.Utc).AddTicks(1910) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PasswordResets");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 23, 8, 2, 32, 395, DateTimeKind.Utc).AddTicks(5750), new DateTime(2025, 3, 23, 8, 2, 32, 395, DateTimeKind.Utc).AddTicks(5750) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 23, 8, 2, 32, 395, DateTimeKind.Utc).AddTicks(5750), new DateTime(2025, 3, 23, 8, 2, 32, 395, DateTimeKind.Utc).AddTicks(5750) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 3, 23, 8, 2, 32, 395, DateTimeKind.Utc).AddTicks(5750));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 3, 23, 8, 2, 32, 395, DateTimeKind.Utc).AddTicks(5750));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 23, 8, 2, 32, 395, DateTimeKind.Utc).AddTicks(5750), "Osq+ZqyuCq20J1ztPP6kMkGcF3sIoL7OLbdM5eRKhizpYKVfs5TRcMg79rGMfTKK0gntim4ssyPYSMwlzUEw6Npnz6bGuYSw2UVKTxoquXc=", new DateTime(2025, 3, 23, 8, 2, 32, 395, DateTimeKind.Utc).AddTicks(5750) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 23, 8, 2, 32, 395, DateTimeKind.Utc).AddTicks(5750), "vUhHzRedLk8FR1cVTFf0xiZyyAsUZ1a3RNk8uo6bQZH8kibHOrzUrAj3YvYktt+iIFmP0NeYMNjPe7ubH8zp5n+AN32KX2xbFvF0lNFpp1Q=", new DateTime(2025, 3, 23, 8, 2, 32, 395, DateTimeKind.Utc).AddTicks(5750) });
        }
    }
}
