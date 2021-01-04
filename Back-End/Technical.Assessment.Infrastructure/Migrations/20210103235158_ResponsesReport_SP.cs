using Microsoft.EntityFrameworkCore.Migrations;

namespace Technical.Assessment.Infrastructure.Migrations
{
    public partial class ResponsesReport_SP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"CREATE PROCEDURE [Responses].[GetAllResponsesBySurveyAndUser]
(
    -- Add the parameters for the stored procedure here
    @SurveyId INT,
	@RespondentId INT 
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
    SELECT NEWID() AS [Id]
	  ,R.[SurverResponseId]
      ,R.[QuestionId]
      ,R.[RespondentId]
      ,R.[Answer]
	  ,S.Id AS SurveyId
	  ,S.[Name] AS SurveyName
	  ,Q.[Text] AS Question
  FROM [Responses].[Response] AS R
  JOIN [Surveys].[QuestionOrder] SQO
  ON R.[QuestionId] = SQO.[QuestionId]
  JOIN [Surveys].[Survey] S
  ON SQO.[SurverId] = S.[Id]
  JOIN [Surveys].[Question] AS Q
  ON SQO.[QuestionId] = Q.[Id]
  WHERE S.[Id] = @SurveyId AND R.RespondentId = @RespondentId
END";
            migrationBuilder.Sql(sp);
            //migrationBuilder.CreateTable(
            //    name: "ResponseReports",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        SurveyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SurverResponseId = table.Column<int>(type: "int", nullable: false),
            //        QuestionId = table.Column<int>(type: "int", nullable: false),
            //        RespondentId = table.Column<int>(type: "int", nullable: false),
            //        SurveyId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ResponseReports", x => x.Id);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ResponseReports");
        }
    }
}
