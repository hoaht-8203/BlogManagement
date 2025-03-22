using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_server.Migrations
{
    /// <inheritdoc />
    public partial class FixLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 18, 34, 37, 431, DateTimeKind.Utc).AddTicks(9980), new DateTime(2025, 3, 22, 18, 34, 37, 431, DateTimeKind.Utc).AddTicks(9980) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 18, 34, 37, 431, DateTimeKind.Utc).AddTicks(9980), new DateTime(2025, 3, 22, 18, 34, 37, 431, DateTimeKind.Utc).AddTicks(9980) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 3, 22, 18, 34, 37, 431, DateTimeKind.Utc).AddTicks(9980));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 3, 22, 18, 34, 37, 431, DateTimeKind.Utc).AddTicks(9980));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 18, 34, 37, 431, DateTimeKind.Utc).AddTicks(9980), "4OiL5LEoyiHCk8NAMdCquJfkb3ZXQGIWlB2Sd4vS5D9Jj8LN3/e5nlO6mX5PoJVAVVKVPI3n97DreOlaciyzyWre8HMMXxfpxN4I6K4NSQE=", new DateTime(2025, 3, 22, 18, 34, 37, 431, DateTimeKind.Utc).AddTicks(9980) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 18, 34, 37, 431, DateTimeKind.Utc).AddTicks(9980), "cxLf5ZaI6wBoGFdNgSTVVyF1zh9QNGOeCh4eIkpdq8njp5pqcjK2w8e4mnwbpZ9NnZ1dUluvkCQIDKFzGx4UpOM8UeqW/YG/JBa6iIhatZA=", new DateTime(2025, 3, 22, 18, 34, 37, 431, DateTimeKind.Utc).AddTicks(9980) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(1670) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(1670) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(2180));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(2180));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(2160), "Bwh9DhUbzVcvjM7/ECw/LyrOItE2F4hbQ0Ond6WJAbwrWqKzUPHhRLLMw4EJxhS7W15gAIXdLgYbk2tac2zEiE3oBbcujK1+LaC/tEjftGs=", new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(2160) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(2130), "6tBqc/UePRvaz9W2ORvEZfkQjIBc3JHrQ/Mxj/EGkO293a178/dTWvE01iswTYnrFGGAePGGJs2wxTrS40xK564iZUR5qpcekHwKDsZ4w08=", new DateTime(2025, 3, 22, 18, 14, 10, 219, DateTimeKind.Utc).AddTicks(2130) });
        }
    }
}
