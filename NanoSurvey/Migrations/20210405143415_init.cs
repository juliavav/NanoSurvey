using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NanoSurvey.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Surveys",
                table => new
                {
                    Id = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>("text", nullable: true),
                    Description = table.Column<string>("text", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Surveys", x => x.Id); });

            migrationBuilder.CreateTable(
                "Interviews",
                table => new
                {
                    Id = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>("text", nullable: true),
                    Date = table.Column<DateTimeOffset>("timestamp with time zone", nullable: false),
                    SurveyId = table.Column<int>("integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.Id);
                    table.ForeignKey(
                        "FK_Interviews_Surveys_SurveyId",
                        x => x.SurveyId,
                        "Surveys",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Questions",
                table => new
                {
                    Id = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>("text", nullable: true),
                    SurveyId = table.Column<int>("integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        "FK_Questions_Surveys_SurveyId",
                        x => x.SurveyId,
                        "Surveys",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Answers",
                table => new
                {
                    Id = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>("text", nullable: true),
                    QuestionId = table.Column<int>("integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        "FK_Answers_Questions_QuestionId",
                        x => x.QuestionId,
                        "Questions",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Results",
                table => new
                {
                    Id = table.Column<int>("integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InterviewId = table.Column<int>("integer", nullable: true),
                    SurveyId = table.Column<int>("integer", nullable: true),
                    QuestionId = table.Column<int>("integer", nullable: true),
                    AnswerId = table.Column<int>("integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        "FK_Results_Answers_AnswerId",
                        x => x.AnswerId,
                        "Answers",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Results_Interviews_InterviewId",
                        x => x.InterviewId,
                        "Interviews",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Results_Questions_QuestionId",
                        x => x.QuestionId,
                        "Questions",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Results_Surveys_SurveyId",
                        x => x.SurveyId,
                        "Surveys",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Answers_QuestionId",
                "Answers",
                "QuestionId");

            migrationBuilder.CreateIndex(
                "IX_Interviews_SurveyId",
                "Interviews",
                "SurveyId");

            migrationBuilder.CreateIndex(
                "IX_Interviews_UserName",
                "Interviews",
                "UserName");

            migrationBuilder.CreateIndex(
                "IX_Questions_SurveyId",
                "Questions",
                "SurveyId");

            migrationBuilder.CreateIndex(
                "IX_Results_AnswerId",
                "Results",
                "AnswerId");

            migrationBuilder.CreateIndex(
                "IX_Results_InterviewId",
                "Results",
                "InterviewId");

            migrationBuilder.CreateIndex(
                "IX_Results_QuestionId",
                "Results",
                "QuestionId");

            migrationBuilder.CreateIndex(
                "IX_Results_SurveyId",
                "Results",
                "SurveyId");

            migrationBuilder.CreateIndex(
                "IX_Surveys_Title",
                "Surveys",
                "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Results");

            migrationBuilder.DropTable(
                "Answers");

            migrationBuilder.DropTable(
                "Interviews");

            migrationBuilder.DropTable(
                "Questions");

            migrationBuilder.DropTable(
                "Surveys");
        }
    }
}