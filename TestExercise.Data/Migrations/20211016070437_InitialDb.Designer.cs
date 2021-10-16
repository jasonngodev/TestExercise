﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestExercise.Entities;

namespace TestExercise.Data.Migrations
{
    [DbContext(typeof(TestDbContext))]
    [Migration("20211016070437_InitialDb")]
    partial class InitialDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TestExercise.Models.BeautyNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Numbers")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BeautyNumbers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Numbers = "19, 24, 26, 37, 34"
                        });
                });

            modelBuilder.Entity("TestExercise.Models.FengShuiNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LastNum")
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("varchar(2)");

                    b.Property<int>("OperatorID")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("OperatorID");

                    b.ToTable("FengShuiNumbers");
                });

            modelBuilder.Entity("TestExercise.Models.Last2NumCase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("chars")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Last2NumCases");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            chars = "00,66"
                        },
                        new
                        {
                            Id = 2,
                            chars = "04, 45, 85, 27, 67"
                        },
                        new
                        {
                            Id = 3,
                            chars = "17, 57, 97, 98, 58"
                        },
                        new
                        {
                            Id = 4,
                            chars = "42, 82"
                        },
                        new
                        {
                            Id = 5,
                            chars = "69"
                        });
                });

            modelBuilder.Entity("TestExercise.Models.MatchCondition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Conditions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rule")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MatchConditions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Conditions = "24/29"
                        },
                        new
                        {
                            Id = 2,
                            Conditions = "24/28"
                        });
                });

            modelBuilder.Entity("TestExercise.Models.Operator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProviderName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Operators");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ProviderName = "MobiFone"
                        },
                        new
                        {
                            Id = 2,
                            ProviderName = "Vinaphone"
                        },
                        new
                        {
                            Id = 3,
                            ProviderName = "Viettel"
                        });
                });

            modelBuilder.Entity("TestExercise.Models.PrefixNumbers", b =>
                {
                    b.Property<int>("PrefixId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OperatorId")
                        .HasColumnType("int");

                    b.Property<string>("PrefixNumber")
                        .IsRequired()
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)");

                    b.HasKey("PrefixId");

                    b.HasIndex("OperatorId");

                    b.ToTable("PrefixNumbers");

                    b.HasData(
                        new
                        {
                            PrefixId = 1,
                            OperatorId = 1,
                            PrefixNumber = "089"
                        },
                        new
                        {
                            PrefixId = 2,
                            OperatorId = 1,
                            PrefixNumber = "090"
                        },
                        new
                        {
                            PrefixId = 3,
                            OperatorId = 1,
                            PrefixNumber = "093"
                        },
                        new
                        {
                            PrefixId = 4,
                            OperatorId = 2,
                            PrefixNumber = "088"
                        },
                        new
                        {
                            PrefixId = 5,
                            OperatorId = 2,
                            PrefixNumber = "091"
                        },
                        new
                        {
                            PrefixId = 6,
                            OperatorId = 2,
                            PrefixNumber = "094"
                        },
                        new
                        {
                            PrefixId = 7,
                            OperatorId = 3,
                            PrefixNumber = "086"
                        },
                        new
                        {
                            PrefixId = 8,
                            OperatorId = 3,
                            PrefixNumber = "096"
                        },
                        new
                        {
                            PrefixId = 9,
                            OperatorId = 3,
                            PrefixNumber = "097"
                        });
                });

            modelBuilder.Entity("TestExercise.Models.FengShuiNumber", b =>
                {
                    b.HasOne("TestExercise.Models.Operator", "Operator")
                        .WithMany("FengShuiNumbers")
                        .HasForeignKey("OperatorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Operator");
                });

            modelBuilder.Entity("TestExercise.Models.PrefixNumbers", b =>
                {
                    b.HasOne("TestExercise.Models.Operator", "Operator")
                        .WithMany("PrefixNumbers")
                        .HasForeignKey("OperatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Operator");
                });

            modelBuilder.Entity("TestExercise.Models.Operator", b =>
                {
                    b.Navigation("FengShuiNumbers");

                    b.Navigation("PrefixNumbers");
                });
#pragma warning restore 612, 618
        }
    }
}