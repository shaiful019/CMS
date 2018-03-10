using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CMS.Domain.Migrations
{
    public partial class test_march_9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTerm",
                table: "PostTerm");

            migrationBuilder.AlterColumn<int>(
                name: "PostTermID",
                table: "PostTerm",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTerm",
                table: "PostTerm",
                columns: new[] { "PostID", "TermID" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTerm_Post_PostID",
                table: "PostTerm");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTerm_Term_TermID",
                table: "PostTerm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTerm",
                table: "PostTerm");

            migrationBuilder.DropIndex(
                name: "IX_PostTerm_TermID",
                table: "PostTerm");

            migrationBuilder.AlterColumn<int>(
                name: "PostTermID",
                table: "PostTerm",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTerm",
                table: "PostTerm",
                column: "PostTermID");
        }
    }
}
