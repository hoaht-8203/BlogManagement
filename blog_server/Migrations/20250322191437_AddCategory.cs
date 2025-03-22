using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace blog_server.Migrations
{
    /// <inheritdoc />
    public partial class AddCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ParentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 19, 14, 37, 813, DateTimeKind.Utc).AddTicks(2800), new DateTime(2025, 3, 22, 19, 14, 37, 813, DateTimeKind.Utc).AddTicks(2800) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 19, 14, 37, 813, DateTimeKind.Utc).AddTicks(2800), new DateTime(2025, 3, 22, 19, 14, 37, 813, DateTimeKind.Utc).AddTicks(2800) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 3, 22, 19, 14, 37, 813, DateTimeKind.Utc).AddTicks(2800));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 3, 22, 19, 14, 37, 813, DateTimeKind.Utc).AddTicks(2800));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 19, 14, 37, 813, DateTimeKind.Utc).AddTicks(2800), "LYujlZGR5azlHk2VjgogH8mvDXKcBPQwOh0vTiVToa4WMLJVtcnxPGg1oYPDZA+LHc2fOqPpP5hXwi/ww1tfN4k29n/sFD36A8uFNHRx+fE=", new DateTime(2025, 3, 22, 19, 14, 37, 813, DateTimeKind.Utc).AddTicks(2800) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 22, 19, 14, 37, 813, DateTimeKind.Utc).AddTicks(2800), "UYiJT8dizm0dyN5VLXBRbH9+mDFA4459vCIDA42BZcIV7YQAP3Pi5ehdtXtpqtj1LWEeCgYKq29yQAbKz98NEkiGYuFhlFY+sE4iq4+oTF8=", new DateTime(2025, 3, 22, 19, 14, 37, 813, DateTimeKind.Utc).AddTicks(2800) });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

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
    }
}
