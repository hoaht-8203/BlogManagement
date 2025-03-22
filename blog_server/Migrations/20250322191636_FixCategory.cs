using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_server.Migrations
{
    /// <inheritdoc />
    public partial class FixCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "categories");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "categories",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "categories",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "categories",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "categories",
                newName: "parent_id");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentId",
                table: "categories",
                newName: "IX_categories_parent_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_categories_categories_parent_id",
                table: "categories",
                column: "parent_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_categories_parent_id",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Categories");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Categories",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "parent_id",
                table: "Categories",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_categories_parent_id",
                table: "Categories",
                newName: "IX_Categories_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories",
                column: "ParentId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
