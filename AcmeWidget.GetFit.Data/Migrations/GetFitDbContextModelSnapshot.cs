﻿// <auto-generated />
using System;
using AcmeWidget.GetFit.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AcmeWidget.GetFit.Data.Migrations
{
    [DbContext(typeof(GetFitDbContext))]
    partial class GetFitDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AcmeWidget.GetFit.Domain.Activities.Activity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("AcmeWidget.GetFit.Domain.Activities.ActivityDate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("ActivityId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.ToTable("ActivityDates", (string)null);
                });

            modelBuilder.Entity("AcmeWidget.GetFit.Domain.ActivitySignups.ActivitySignUp", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("ActivityDateId")
                        .HasColumnType("bigint");

                    b.Property<long>("ActivityId")
                        .HasColumnType("bigint");

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("YearsOfExperienceInActivity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActivityDateId");

                    b.HasIndex("ActivityId");

                    b.ToTable("ActivitySignUps");
                });

            modelBuilder.Entity("AcmeWidget.GetFit.Domain.Activities.ActivityDate", b =>
                {
                    b.HasOne("AcmeWidget.GetFit.Domain.Activities.Activity", "Activity")
                        .WithMany("Dates")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");
                });

            modelBuilder.Entity("AcmeWidget.GetFit.Domain.ActivitySignups.ActivitySignUp", b =>
                {
                    b.HasOne("AcmeWidget.GetFit.Domain.Activities.ActivityDate", "ActivityDate")
                        .WithMany("ActivitySignUps")
                        .HasForeignKey("ActivityDateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcmeWidget.GetFit.Domain.Activities.Activity", "Activity")
                        .WithMany("ActivitySignUps")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("ActivityDate");
                });

            modelBuilder.Entity("AcmeWidget.GetFit.Domain.Activities.Activity", b =>
                {
                    b.Navigation("ActivitySignUps");

                    b.Navigation("Dates");
                });

            modelBuilder.Entity("AcmeWidget.GetFit.Domain.Activities.ActivityDate", b =>
                {
                    b.Navigation("ActivitySignUps");
                });
#pragma warning restore 612, 618
        }
    }
}
