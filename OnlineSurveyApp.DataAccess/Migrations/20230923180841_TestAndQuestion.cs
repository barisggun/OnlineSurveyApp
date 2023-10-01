using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSurveyApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TestAndQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Guests_GuestId",
                table: "Tests");

            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.AlterColumn<int>(
                name: "GuestId",
                table: "Tests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "QuestionTest",
                columns: table => new
                {
                    QuestionsID = table.Column<int>(type: "int", nullable: false),
                    TestsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTest", x => new { x.QuestionsID, x.TestsID });
                    table.ForeignKey(
                        name: "FK_QuestionTest_Questions_QuestionsID",
                        column: x => x.QuestionsID,
                        principalTable: "Questions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionTest_Tests_TestsID",
                        column: x => x.TestsID,
                        principalTable: "Tests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTest_TestsID",
                table: "QuestionTest",
                column: "TestsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Guests_GuestId",
                table: "Tests",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Guests_GuestId",
                table: "Tests");

            migrationBuilder.DropTable(
                name: "QuestionTest");

            migrationBuilder.AlterColumn<int>(
                name: "GuestId",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Exam_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exam_QuestionID",
                table: "Exam",
                column: "QuestionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Guests_GuestId",
                table: "Tests",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
