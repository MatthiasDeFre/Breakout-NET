using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using BreakOutGame.Models.Domain;

namespace BreakOutGame.Data.Migrations
{
    public partial class GroupMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*  migrationBuilder.CreateTable(
                  name: "BOBGROUP",
                  columns: table => new
                  {
                      ID = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                      Blocked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                      BoBSessionId = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                      name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                  },
                  constraints: table =>
                  {
                      table.PrimaryKey("PK_BOBGROUP", x => x.ID);
                      table.ForeignKey(
                          name: "FK_BOBGROUP_BoBSession_BoBSessionId",
                          column: x => x.BoBSessionId,
                          principalTable: "BoBSession",
                          principalColumn: "Id",
                          onDelete: ReferentialAction.Restrict);
                  });*/
            migrationBuilder.AddColumn<GroupStatus>(
                name: "Status",
                table: "BOBGROUP",
                defaultValue: 0
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BOBGROUP"

            );
            /* migrationBuilder.DropTable(
                 name: "BOBGROUP");
                 */

        }
    }
}
