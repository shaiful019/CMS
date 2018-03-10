using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CMS.Domain.Migrations
{
    public partial class test_3_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTerm_Post_PostID",
                table: "PostTerm");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTerm_Term_TermID",
                table: "PostTerm");

            migrationBuilder.DropIndex(
                name: "IX_PostTerm_PostID",
                table: "PostTerm");

            migrationBuilder.DropIndex(
                name: "IX_PostTerm_TermID",
                table: "PostTerm");

            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "Post",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Post");

            migrationBuilder.CreateIndex(
                name: "IX_PostTerm_PostID",
                table: "PostTerm",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_PostTerm_TermID",
                table: "PostTerm",
                column: "TermID");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTerm_Post_PostID",
                table: "PostTerm",
                column: "PostID",
                principalTable: "Post",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTerm_Term_TermID",
                table: "PostTerm",
                column: "TermID",
                principalTable: "Term",
                principalColumn: "TermID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
