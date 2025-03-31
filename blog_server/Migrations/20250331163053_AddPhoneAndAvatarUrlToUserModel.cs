using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog_server.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneAndAvatarUrlToUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_used",
                table: "user_tokens");

            migrationBuilder.AddColumn<string>(
                name: "avatar_url",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone",
                table: "users",
                type: "text",
                nullable: true);

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
                columns: new[] { "avatar_url", "create_date", "password_hash", "phone", "update_date" },
                values: new object[] { null, new DateTime(2025, 3, 31, 16, 30, 53, 630, DateTimeKind.Utc).AddTicks(690), "flIlqs1wuPogqZsynER/UEIss0OZvQMZ16gxZcI7/ufSynBBi/AT3NcNr0GMrfezDhbHeuTG6AFP3WZjaITASNe0SF6T2Qo63buIOM827zs=", null, new DateTime(2025, 3, 31, 16, 30, 53, 630, DateTimeKind.Utc).AddTicks(690) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "avatar_url", "create_date", "password_hash", "phone", "update_date" },
                values: new object[] { null, new DateTime(2025, 3, 31, 16, 30, 53, 630, DateTimeKind.Utc).AddTicks(690), "B/6pahLqLf67B15e42OIGvYMMY3YAMsDEWep+X3jma5lA9c4ePMy/fgmVkYWdKfdWwLDf/9ILkDUwlkNbvnfuhDcvA+elhqD55P67DPZ66s=", null, new DateTime(2025, 3, 31, 16, 30, 53, 630, DateTimeKind.Utc).AddTicks(690) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avatar_url",
                table: "users");

            migrationBuilder.DropColumn(
                name: "phone",
                table: "users");

            migrationBuilder.AddColumn<bool>(
                name: "is_used",
                table: "user_tokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200), new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200) });

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "create_date", "update_date" },
                values: new object[] { new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200), new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200) });

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87") },
                column: "join_date",
                value: new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98") },
                column: "join_date",
                value: new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("19542f2e-d222-4a24-a786-c2dc08ccfd87"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200), "5cdutg5Ha7d4hykoY10530V3NjrfxQAipW/2kjn2KXmw3hgXhNaagJeBz64/pjeqV3hiFE3W756kutESw7DcnlnafZh9bY1/hlsIcfU8eiA=", new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("96cb39f1-318f-4b17-97fb-c9bffe823a98"),
                columns: new[] { "create_date", "password_hash", "update_date" },
                values: new object[] { new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200), "AIbUZUfOvNhpIrkhKaAirCFNm9yPeVlhz+HICut1a69na5FNpbbqHmf5hQMi804v/zJrlNFxgvXm8MIKgzdpSl47ywbw+CCzOLb5W0ODRgU=", new DateTime(2025, 3, 30, 8, 1, 36, 757, DateTimeKind.Utc).AddTicks(1200) });
        }
    }
}
