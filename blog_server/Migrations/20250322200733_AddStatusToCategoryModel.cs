using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_server.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToCategoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "categories");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 19, 16, 36, 704, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 3, 22, 19, 16, 36, 704, DateTimeKind.Utc).AddTicks(3090) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 19, 16, 36, 704, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 3, 22, 19, 16, 36, 704, DateTimeKind.Utc).AddTicks(3090) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 3, 22, 19, 16, 36, 704, DateTimeKind.Utc).AddTicks(3090));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 3, 22, 19, 16, 36, 704, DateTimeKind.Utc).AddTicks(3090));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 19, 16, 36, 704, DateTimeKind.Utc).AddTicks(3090), "jtIJCUFvF1BJAJ0FHD2zG1c+pK219FETBe1D/WkxTnRhaxiz9vFhYudQUCak2KctkkvJs/4cFMbg8fODnBHaoAw2lnYLs2YGV+znXLbwAaY=", new DateTime(2025, 3, 22, 19, 16, 36, 704, DateTimeKind.Utc).AddTicks(3090) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 19, 16, 36, 704, DateTimeKind.Utc).AddTicks(3090), "E8xjEUO738EPerHGeSTSt3OUQSvD+D9o2eUd5tDyjoEhctftjbezjqchyEgwM8pbOoiaDSScX1WiFchXxclKrfxsJyaSXsV4AJA4gqUK6Do=", new DateTime(2025, 3, 22, 19, 16, 36, 704, DateTimeKind.Utc).AddTicks(3090) });
        }
    }
}
