using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addusercodeexpirationtime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ForgetDate",
                schema: "Identity",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForgetDate",
                schema: "Identity",
                table: "AspNetUsers");
        }
    }
}
