using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_server.Migrations
{
    /// <inheritdoc />
    public partial class AddAtbToUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fullname",
                table: "users",
                type: "text",
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
                columns: new[] { "address", "create_date", "fullname", "password_hash", "update_date" },
                values: new object[] { null, new DateTime(2025, 4, 1, 19, 48, 3, 430, DateTimeKind.Utc).AddTicks(1890), null, "C/ADJqAB6Hoa1xeZhNfkM3BjFob5whcjbpWgcjjNT0xIK6IYrziBvbCI2Bmum9KEcGZH1kQFSWOYmeyHQ+dT7a6WfO66ZhSgpnMnFRdo0Uc=", new DateTime(2025, 4, 1, 19, 48, 3, 430, DateTimeKind.Utc).AddTicks(1890) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "address", "create_date", "fullname", "password_hash", "update_date" },
                values: new object[] { null, new DateTime(2025, 4, 1, 19, 48, 3, 430, DateTimeKind.Utc).AddTicks(1890), null, "uwscY7QZjIHLQkqmFt21LKx+5X8dYyfnx2MjjJGBu7VeDkstHvZ/opJiDktoXNKA36BEclYygKuXQnc/BSig7magx8ZQFKb9oiwjGNJ5zjs=", new DateTime(2025, 4, 1, 19, 48, 3, 430, DateTimeKind.Utc).AddTicks(1890) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "users");

            migrationBuilder.DropColumn(
                name: "fullname",
                table: "users");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 31, 16, 30, 53, 630, DateTimeKind.Utc).AddTicks(690), new DateTime(2025, 3, 31, 16, 30, 53, 630, DateTimeKind.Utc).AddTicks(690) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 31, 16, 30, 53, 630, DateTimeKind.Utc).AddTicks(690), new DateTime(2025, 3, 31, 16, 30, 53, 630, DateTimeKind.Utc).AddTicks(690) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 3, 31, 16, 30, 53, 630, DateTimeKind.Utc).AddTicks(690));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 3, 31, 16, 30, 53, 630, DateTimeKind.Utc).AddTicks(690));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 31, 16, 30, 53, 630, DateTimeKind.Utc).AddTicks(690), "flIlqs1wuPogqZsynER/UEIss0OZvQMZ16gxZcI7/ufSynBBi/AT3NcNr0GMrfezDhbHeuTG6AFP3WZjaITASNe0SF6T2Qo63buIOM827zs=", new DateTime(2025, 3, 31, 16, 30, 53, 630, DateTimeKind.Utc).AddTicks(690) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 31, 16, 30, 53, 630, DateTimeKind.Utc).AddTicks(690), "B/6pahLqLf67B15e42OIGvYMMY3YAMsDEWep+X3jma5lA9c4ePMy/fgmVkYWdKfdWwLDf/9ILkDUwlkNbvnfuhDcvA+elhqD55P67DPZ66s=", new DateTime(2025, 3, 31, 16, 30, 53, 630, DateTimeKind.Utc).AddTicks(690) });
        }
    }
}
