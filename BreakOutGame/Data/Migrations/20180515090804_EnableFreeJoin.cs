using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BreakOutGame.Data.Migrations
{
    public partial class EnableFreeJoin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Boolean>(
                name: "FreeJoinEnabled",
                table: "BoBSession",
                defaultValue: false
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "FreeJoinEnabled",
                table: "BoBSession"
            );
        }
    }
}
