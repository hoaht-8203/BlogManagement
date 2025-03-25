using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_server.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseEntityToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "create_by",
                table: "categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "create_date",
                table: "categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "update_by",
                table: "categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "update_date",
                table: "categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "create_by",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "create_date",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "update_by",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "update_date",
                table: "categories");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 20, 7, 33, 184, DateTimeKind.Utc).AddTicks(6880), new DateTime(2025, 3, 22, 20, 7, 33, 184, DateTimeKind.Utc).AddTicks(6880) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 20, 7, 33, 184, DateTimeKind.Utc).AddTicks(6880), new DateTime(2025, 3, 22, 20, 7, 33, 184, DateTimeKind.Utc).AddTicks(6880) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 3, 22, 20, 7, 33, 184, DateTimeKind.Utc).AddTicks(6880));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 3, 22, 20, 7, 33, 184, DateTimeKind.Utc).AddTicks(6880));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 20, 7, 33, 184, DateTimeKind.Utc).AddTicks(6880), "SffPuZu1nu719rKXqWIj42+WOo3/YvAmGXbBA3KdJrPnK+tzP4Q7YZKWBzzMfZzrvMIMyPOC+Hkm03SO411N4WJYEAxkzwtA3a/6lBzVO8Y=", new DateTime(2025, 3, 22, 20, 7, 33, 184, DateTimeKind.Utc).AddTicks(6880) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 20, 7, 33, 184, DateTimeKind.Utc).AddTicks(6880), "OK/1J81kUheB3WfUpt2rVNrNFsDo8KKXlgFKvTx7wLiyPoZyttmLVK8XcX177rs0DfDlNWUcJgNqw1/MMn/u7HymhW566/BgwDGRedxBICk=", new DateTime(2025, 3, 22, 20, 7, 33, 184, DateTimeKind.Utc).AddTicks(6880) });
        }
    }
}
