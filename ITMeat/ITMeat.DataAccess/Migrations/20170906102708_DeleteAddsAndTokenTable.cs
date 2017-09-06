using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITMeat.DataAccess.Migrations
{
    public partial class DeleteAddsAndTokenTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOrderAdds");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Adds");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Expense = table.Column<decimal>(type: "DECIMAL(16,2)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    PubId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adds_Pub_PubId",
                        column: x => x.PubId,
                        principalTable: "Pub",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    SecretToken = table.Column<string>(type: "NVARCHAR(256)", nullable: true),
                    SecretTokenTimeStamp = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserOrderAdds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AddsId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    UserOrderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrderAdds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOrderAdds_Adds_AddsId",
                        column: x => x.AddsId,
                        principalTable: "Adds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserOrderAdds_UserOrders_UserOrderId",
                        column: x => x.UserOrderId,
                        principalTable: "UserOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adds_PubId",
                table: "Adds",
                column: "PubId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrderAdds_AddsId",
                table: "UserOrderAdds",
                column: "AddsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrderAdds_UserOrderId",
                table: "UserOrderAdds",
                column: "UserOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                table: "UserTokens",
                column: "UserId");
        }
    }
}
