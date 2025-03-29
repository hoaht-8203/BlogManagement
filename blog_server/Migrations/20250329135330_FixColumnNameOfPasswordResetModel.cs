using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_server.Migrations
{
    /// <inheritdoc />
    public partial class FixColumnNameOfPasswordResetModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PasswordResets",
                table: "PasswordResets");

            migrationBuilder.RenameTable(
                name: "PasswordResets",
                newName: "password_resets");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "password_resets",
                newName: "token");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "password_resets",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "password_resets",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "password_resets",
                newName: "is_used");

            migrationBuilder.RenameColumn(
                name: "ExpiryTime",
                table: "password_resets",
                newName: "expiry_time");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "password_resets",
                newName: "created_at");

            migrationBuilder.AddPrimaryKey(
                name: "PK_password_resets",
                table: "password_resets",
                column: "id");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980), new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980), new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980), "32F79tEHmagp3VFMpJsGIr9o4kJuIxk3S7Yc2RfxWz4iObPYg15VXzShTzO5e61zisMgtEqUhGRtSW6yHyre+zFeknGHe5HALrMWZ0wIwj0=", new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980), "y8H44K0t4xESt3LQx5fZWgJgoxq4tNxUnDGNYRFzLwlFtVgqhyZEiPInMa1c/GdvsUJbh8+a6CvdziWkZ1mkwHca7hqBChLMHHL8LBAagH4=", new DateTime(2025, 3, 29, 13, 53, 30, 510, DateTimeKind.Utc).AddTicks(1980) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_password_resets",
                table: "password_resets");

            migrationBuilder.RenameTable(
                name: "password_resets",
                newName: "PasswordResets");

            migrationBuilder.RenameColumn(
                name: "token",
                table: "PasswordResets",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "PasswordResets",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PasswordResets",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "is_used",
                table: "PasswordResets",
                newName: "IsUsed");

            migrationBuilder.RenameColumn(
                name: "expiry_time",
                table: "PasswordResets",
                newName: "ExpiryTime");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "PasswordResets",
                newName: "CreatedAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PasswordResets",
                table: "PasswordResets",
                column: "Id");

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
    }
}
