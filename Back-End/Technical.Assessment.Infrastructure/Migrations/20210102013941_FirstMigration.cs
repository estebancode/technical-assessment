using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Technical.Assessment.Infrastructure.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Surveys");

            migrationBuilder.EnsureSchema(
                name: "Users");

            migrationBuilder.EnsureSchema(
                name: "Responses");

            migrationBuilder.CreateTable(
                name: "Question",
                schema: "Surveys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Respondent",
                schema: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    HashedPassword = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respondent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Survey",
                schema: "Surveys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOrder",
                schema: "Surveys",
                columns: table => new
                {
                    SurverId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOrder", x => new { x.QuestionId, x.SurverId });
                    table.ForeignKey(
                        name: "FK_QuestionOrder_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "Surveys",
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionOrder_Survey_SurverId",
                        column: x => x.SurverId,
                        principalSchema: "Surveys",
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyResponse",
                schema: "Responses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    RespondentId = table.Column<int>(type: "int", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyResponse_Respondent_RespondentId",
                        column: x => x.RespondentId,
                        principalSchema: "Users",
                        principalTable: "Respondent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SurveyResponse_Survey_SurveyId",
                        column: x => x.SurveyId,
                        principalSchema: "Surveys",
                        principalTable: "Survey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Response",
                schema: "Responses",
                columns: table => new
                {
                    SurverResponseId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    RespondentId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Response", x => new { x.QuestionId, x.RespondentId, x.SurverResponseId });
                    table.ForeignKey(
                        name: "FK_Response_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "Surveys",
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Response_Respondent_RespondentId",
                        column: x => x.RespondentId,
                        principalSchema: "Users",
                        principalTable: "Respondent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Response_SurveyResponse_SurverResponseId",
                        column: x => x.SurverResponseId,
                        principalSchema: "Responses",
                        principalTable: "SurveyResponse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOrder_SurverId",
                schema: "Surveys",
                table: "QuestionOrder",
                column: "SurverId");

            migrationBuilder.CreateIndex(
                name: "IX_Response_RespondentId",
                schema: "Responses",
                table: "Response",
                column: "RespondentId");

            migrationBuilder.CreateIndex(
                name: "IX_Response_SurverResponseId",
                schema: "Responses",
                table: "Response",
                column: "SurverResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponse_RespondentId",
                schema: "Responses",
                table: "SurveyResponse",
                column: "RespondentId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponse_SurveyId",
                schema: "Responses",
                table: "SurveyResponse",
                column: "SurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionOrder",
                schema: "Surveys");

            migrationBuilder.DropTable(
                name: "Response",
                schema: "Responses");

            migrationBuilder.DropTable(
                name: "Question",
                schema: "Surveys");

            migrationBuilder.DropTable(
                name: "SurveyResponse",
                schema: "Responses");

            migrationBuilder.DropTable(
                name: "Respondent",
                schema: "Users");

            migrationBuilder.DropTable(
                name: "Survey",
                schema: "Surveys");
        }
    }
}
