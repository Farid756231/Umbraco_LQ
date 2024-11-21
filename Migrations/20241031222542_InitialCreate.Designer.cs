﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using umbraco_lingoquest.Data;

#nullable disable

namespace umbraco_lingoquest.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241031222542_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("umbraco_lingoquest.Moduller.QuizViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CorrectAnswer")
                        .HasColumnType("TEXT");

                    b.Property<string>("EnglishText")
                        .HasColumnType("TEXT");

                    b.Property<string>("MissingWords")
                        .HasColumnType("TEXT");

                    b.Property<string>("SwedishText")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Quizzes");
                });
#pragma warning restore 612, 618
        }
    }
}
