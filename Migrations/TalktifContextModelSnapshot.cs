﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TalktifAPI.Models;

namespace TalktifAPI.Migrations
{
    [DbContext(typeof(TalktifContext))]
    partial class TalktifContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TalktifAPI.Models.ChatRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChatRoomName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("chatRoomName");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("createdAt");

                    b.Property<int>("NumOfMember")
                        .HasColumnType("int")
                        .HasColumnName("numOfMember");

                    b.HasKey("Id");

                    b.ToTable("Chat_Room");
                });

            modelBuilder.Entity("TalktifAPI.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId")
                        .HasColumnType("int")
                        .HasColumnName("countryId");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("createdAt");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("TalktifAPI.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime")
                        .HasColumnName("createAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("TalktifAPI.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChatRoomId")
                        .HasColumnType("int")
                        .HasColumnName("chatRoomId");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("content");

                    b.Property<int>("Sender")
                        .HasColumnType("int")
                        .HasColumnName("sender");

                    b.Property<DateTime?>("SentAt")
                        .HasColumnType("datetime")
                        .HasColumnName("sentAt");

                    b.HasKey("Id");

                    b.HasIndex("ChatRoomId");

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

                    b.Property<int>("CityId")
                        .HasColumnType("int")
                        .HasColumnName("cityId");

                    b.Property<bool?>("ConfirmedEmail")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("confirmedEmail")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("createdAt");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<bool?>("Gender")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("gender")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("Hobbies")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("hobbies");

                    b.Property<bool?>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("isActive")
                        .HasDefaultValueSql("((1))");

                    b.Property<bool?>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("isAdmin")
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

                    b.HasIndex("CityId");

                    b.HasIndex(new[] { "Email" }, "UQ__User__AB6E6164565CBAC3")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("TalktifAPI.Models.UserChatRoom", b =>
                {
                    b.Property<int>("User")
                        .HasColumnType("int")
                        .HasColumnName("user");

                    b.Property<int>("ChatRoomId")
                        .HasColumnType("int")
                        .HasColumnName("chatRoomId");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("nickName");

                    b.HasKey("User", "ChatRoomId")
                        .HasName("PK__User_Cha__4372E63ACB17237C");

                    b.HasIndex("ChatRoomId");

                    b.ToTable("User_ChatRoom");
                });

            modelBuilder.Entity("TalktifAPI.Models.UserRefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateAt")
                        .HasColumnType("datetime")
                        .HasColumnName("createAt");

                    b.Property<string>("Device")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("device");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(false)
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("refreshToken");

                    b.Property<int>("User")
                        .HasColumnType("int")
                        .HasColumnName("user");

                    b.HasKey("Id");

                    b.HasIndex("User");

                    b.ToTable("User_RefreshToken");
                });

            modelBuilder.Entity("TalktifAPI.Models.City", b =>
                {
                    b.HasOne("TalktifAPI.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("FK__City__countryId__7849DB76")
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("TalktifAPI.Models.Message", b =>
                {
                    b.HasOne("TalktifAPI.Models.ChatRoom", "ChatRoom")
                        .WithMany("Messages")
                        .HasForeignKey("ChatRoomId")
                        .HasConstraintName("FK__Message__chatRoo__2334397B")
                        .IsRequired();

                    b.Navigation("ChatRoom");
                });

            modelBuilder.Entity("TalktifAPI.Models.Report", b =>
                {
                    b.HasOne("TalktifAPI.Models.User", "ReporterNavigation")
                        .WithMany("Reports")
                        .HasForeignKey("Reporter")
                        .HasConstraintName("FK__Report__reporter__2610A626")
                        .IsRequired();

                    b.Navigation("ReporterNavigation");
                });

            modelBuilder.Entity("TalktifAPI.Models.User", b =>
                {
                    b.HasOne("TalktifAPI.Models.City", "City")
                        .WithMany("Users")
                        .HasForeignKey("CityId")
                        .HasConstraintName("FK__User__cityId__14E61A24")
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("TalktifAPI.Models.UserChatRoom", b =>
                {
                    b.HasOne("TalktifAPI.Models.ChatRoom", "ChatRoom")
                        .WithMany("UserChatRooms")
                        .HasForeignKey("ChatRoomId")
                        .HasConstraintName("FK__User_Chat__chatR__2057CCD0")
                        .IsRequired();

                    b.HasOne("TalktifAPI.Models.User", "UserNavigation")
                        .WithMany("UserChatRooms")
                        .HasForeignKey("User")
                        .HasConstraintName("FK__User_ChatR__user__1F63A897")
                        .IsRequired();

                    b.Navigation("ChatRoom");

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("TalktifAPI.Models.UserRefreshToken", b =>
                {
                    b.HasOne("TalktifAPI.Models.User", "UserNavigation")
                        .WithMany("UserRefreshTokens")
                        .HasForeignKey("User")
                        .HasConstraintName("FK__User_Refre__user__1A9EF37A")
                        .IsRequired();

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("TalktifAPI.Models.ChatRoom", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("UserChatRooms");
                });

            modelBuilder.Entity("TalktifAPI.Models.City", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("TalktifAPI.Models.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("TalktifAPI.Models.User", b =>
                {
                    b.Navigation("Reports");

                    b.Navigation("UserChatRooms");

                    b.Navigation("UserRefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
