using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITMeat.DataAccess.Migrations
{
    public partial class FixProblems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmitOrderDate",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDateTime",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmitOrderDate",
                table: "Orders",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDateTime",
                table: "Orders",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
