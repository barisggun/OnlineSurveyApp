﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSurveyApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTestIdToCorrectAnswerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "CorrectAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CorrectAnswers_TestId",
                table: "CorrectAnswers",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrectAnswers_Tests_TestId",
                table: "CorrectAnswers",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrectAnswers_Tests_TestId",
                table: "CorrectAnswers");

            migrationBuilder.DropIndex(
                name: "IX_CorrectAnswers_TestId",
                table: "CorrectAnswers");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "CorrectAnswers");
        }
    }
}
