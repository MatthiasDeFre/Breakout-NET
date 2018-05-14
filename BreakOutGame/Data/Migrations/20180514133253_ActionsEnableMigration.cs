using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BreakOutGame.Data.Migrations
{
    public partial class ActionsEnableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Boolean>(
                name: "ActionsEnabled",
                table: "BoBSession",
                defaultValue: true
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionsEnabled",
                table: "BoBSession"
            );
        }
    }
}
