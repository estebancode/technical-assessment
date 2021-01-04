﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Technical.Assessment.Infrastructure.Context;

namespace Technical.Assessment.Infrastructure.Migrations
{
    [DbContext(typeof(TechnicalAssessmentContext))]
    [Migration("20210103235158_ResponsesReport_SP")]
    partial class ResponsesReport_SP
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Technical.Assessment.Domain.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Text")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Question", "Surveys");
                });

            modelBuilder.Entity("Technical.Assessment.Domain.Entities.QuestionOrder", b =>
                {
                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("SurverId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("QuestionId", "SurverId");

                    b.HasIndex("SurverId");

                    b.ToTable("QuestionOrder", "Surveys");
                });

            modelBuilder.Entity("Technical.Assessment.Domain.Entities.Respondent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Email")
                        .HasMaxLength(254)
                        .HasColumnType("varchar(254)");

                    b.Property<string>("HashedPassword")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Respondent", "Users");
                });

            modelBuilder.Entity("Technical.Assessment.Domain.Entities.Response", b =>
                {
                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("RespondentId")
                        .HasColumnType("int");

                    b.Property<int>("SurverResponseId")
                        .HasColumnType("int");

                    b.Property<string>("Answer")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("QuestionId", "RespondentId", "SurverResponseId");

                    b.HasIndex("RespondentId");

                    b.HasIndex("SurverResponseId");

                    b.ToTable("Response", "Responses");
                });

            modelBuilder.Entity("Technical.Assessment.Domain.Entities.ResponseReport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Question")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int>("RespondentId")
                        .HasColumnType("int");

                    b.Property<int>("SurverResponseId")
                        .HasColumnType("int");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<string>("SurveyName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ResponseReports");
                });

            modelBuilder.Entity("Technical.Assessment.Domain.Entities.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Survey", "Surveys");
                });

            modelBuilder.Entity("Technical.Assessment.Domain.Entities.SurveyResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<int>("RespondentId")
                        .HasColumnType("int");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RespondentId");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyResponse", "Responses");
                });

            modelBuilder.Entity("Technical.Assessment.Domain.Entities.QuestionOrder", b =>
                {
                    b.HasOne("Technical.Assessment.Domain.Entities.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Technical.Assessment.Domain.Entities.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("Technical.Assessment.Domain.Entities.Response", b =>
                {
                    b.HasOne("Technical.Assessment.Domain.Entities.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Technical.Assessment.Domain.Entities.Respondent", "Respondent")
                        .WithMany()
                        .HasForeignKey("RespondentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Technical.Assessment.Domain.Entities.SurveyResponse", "SurveyResponse")
                        .WithMany()
                        .HasForeignKey("SurverResponseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Respondent");

                    b.Navigation("SurveyResponse");
                });

            modelBuilder.Entity("Technical.Assessment.Domain.Entities.SurveyResponse", b =>
                {
                    b.HasOne("Technical.Assessment.Domain.Entities.Respondent", "Respondent")
                        .WithMany()
                        .HasForeignKey("RespondentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Technical.Assessment.Domain.Entities.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Respondent");

                    b.Navigation("Survey");
                });
#pragma warning restore 612, 618
        }
    }
}
