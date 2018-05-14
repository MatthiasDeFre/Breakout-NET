using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using BreakOutGame.Models.Domain;

namespace BreakOutGame.Data.Migrations
{
    public partial class AddAssignmentStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<AssignmentStatus>(
                name: "Status",
                table: "Assignment",
                defaultValue: 0
            );
            migrationBuilder.AddColumn<int>(
                name: "WrongCount",
                table: "Assignment",
                defaultValue: 0
            );
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Assignment"

            );
            migrationBuilder.DropColumn(
                name: "WrongCount",
                table: "Assignment"

            );
        }
    }
}
