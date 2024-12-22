﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogAz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _202412221300 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Admins");
        }
    }
}
