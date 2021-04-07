﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TalktifAPI.Models;

namespace TalktifAPI.Migrations
{
    [DbContext(typeof(TalktifContext))]
    [Migration("20210331083814_chang hobbies")]
    partial class changhobbies
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TalktifAPI.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("content");

                    b.Property<int>("From")
                        .HasColumnType("int")
                        .HasColumnName("from");

                    b.Property<DateTime?>("SentAt")
                        .HasColumnType("datetime")
                        .HasColumnName("sentAt");

                    b.Property<int>("To")
                        .HasColumnType("int")
                        .HasColumnName("to");

                    b.HasKey("Id");

                    b.HasIndex("From");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("TalktifAPI.Models.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("createdAt");

                    b.Property<string>("Note")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("note");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("reason");

                    b.Property<int>("Reporter")
                        .HasColumnType("int")
                        .HasColumnName("reporter");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.Property<int>("Suspect")
                        .HasColumnType("int")
                        .HasColumnName("suspect");

                    b.HasKey("Id");

                    b.HasIndex("Reporter");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("TalktifAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("address");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("createdAt");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit")
                        .HasColumnName("gender");

                    b.Property<string>("Hobbies")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("hobbies");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("isActive")
                        .HasDefaultValueSql("((1))");

                    b.Property<bool?>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "UQ__User__AB6E6164410B6ED7")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("TalktifAPI.Models.UserFav", b =>
                {
                    b.Property<int>("User")
                        .HasColumnType("int")
                        .HasColumnName("user");

                    b.Property<int>("Favourite")
                        .HasColumnType("int")
                        .HasColumnName("favourite");

                    b.Property<DateTime>("AddAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("addedAt");

                    b.Property<string>("NickName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("nickname");

                    b.HasKey("User", "Favourite")
                        .HasName("PK__User_Fav__A323CB9348DF7B6E");

                    b.ToTable("User_Favs");
                });

            modelBuilder.Entity("TalktifAPI.Models.UserToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("token");

                    b.Property<int>("Uid")
                        .HasMaxLength(100)
                        .HasColumnType("int")
                        .HasColumnName("uid");

                    b.Property<DateTime>("createAt")
                        .HasMaxLength(200)
                        .HasColumnType("datetime2")
                        .HasColumnName("create_at");

                    b.HasKey("Id")
                        .HasName("PK__User_Token__A323CB9448DF7B6E");

                    b.HasIndex("Uid");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("TalktifAPI.Models.Message", b =>
                {
                    b.HasOne("TalktifAPI.Models.User", "FromNavigation")
                        .WithMany("MessageFromNavigations")
                        .HasForeignKey("From")
                        .HasConstraintName("FK__Message__from__2D27B809")
                        .IsRequired();

                    b.Navigation("FromNavigation");
                });

            modelBuilder.Entity("TalktifAPI.Models.Report", b =>
                {
                    b.HasOne("TalktifAPI.Models.User", "ReporterNavigation")
                        .WithMany("ReportReporterNavigations")
                        .HasForeignKey("Reporter")
                        .HasConstraintName("FK__Report__reporter__30F848ED")
                        .IsRequired();

                    b.Navigation("ReporterNavigation");
                });

            modelBuilder.Entity("TalktifAPI.Models.UserFav", b =>
                {
                    b.HasOne("TalktifAPI.Models.User", "UserNavigation")
                        .WithMany("UserFavUserNavigations")
                        .HasForeignKey("User")
                        .HasConstraintName("FK__User_Favs__user__29572725")
                        .IsRequired();

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("TalktifAPI.Models.UserToken", b =>
                {
                    b.HasOne("TalktifAPI.Models.User", "UserTokenNavigation")
                        .WithMany("UserTokenUserNavigations")
                        .HasForeignKey("Uid")
                        .HasConstraintName("FK__User_Token__user__29572725")
                        .IsRequired();

                    b.Navigation("UserTokenNavigation");
                });

            modelBuilder.Entity("TalktifAPI.Models.User", b =>
                {
                    b.Navigation("MessageFromNavigations");

                    b.Navigation("ReportReporterNavigations");

                    b.Navigation("UserFavUserNavigations");

                    b.Navigation("UserTokenUserNavigations");
                });
#pragma warning restore 612, 618
        }
    }
}
