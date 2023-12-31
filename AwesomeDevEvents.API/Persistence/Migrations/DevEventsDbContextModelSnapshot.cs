﻿// <auto-generated />
using System;
using AwesomeDevEvents.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AwesomeDevEvents.API.Persistence.Migrations
{
    [DbContext(typeof(DevEventsDbContext))]
    partial class DevEventsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AwesomeDevEvents.API.Entities.DevEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("Description");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("End_Date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("Start_Date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("DevEvents");
                });

            modelBuilder.Entity("AwesomeDevEvents.API.Entities.DevEventSpeaker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("DevEventId")
                        .HasColumnType("char(36)");

                    b.Property<string>("LinkedinProfile")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("Linkedin_Profile");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Name");

                    b.Property<string>("TalkDescription")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("Talk_Description");

                    b.Property<string>("TalkTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Talk_Title");

                    b.HasKey("Id");

                    b.HasIndex("DevEventId");

                    b.ToTable("DevEventSpeakers");
                });

            modelBuilder.Entity("AwesomeDevEvents.API.Entities.DevEventSpeaker", b =>
                {
                    b.HasOne("AwesomeDevEvents.API.Entities.DevEvent", null)
                        .WithMany("Speakers")
                        .HasForeignKey("DevEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AwesomeDevEvents.API.Entities.DevEvent", b =>
                {
                    b.Navigation("Speakers");
                });
#pragma warning restore 612, 618
        }
    }
}
