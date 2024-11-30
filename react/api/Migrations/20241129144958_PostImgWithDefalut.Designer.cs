﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.DAL;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241129144958_PostImgWithDefalut")]
    partial class PostImgWithDefalut
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("api.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("PostId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("api.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tag")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("UpvoteCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "student.expert@example.com",
                            Content = "Final exams can be overwhelming, but with proper preparation, you can ace them. Here are the top 10 study tips that helped me improve my grades and stay focused during finals.",
                            CreatedDate = new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(74),
                            ImageUrl = "images/post1.webp",
                            Tag = "study, finals, productivity",
                            Title = "Top 10 Study Tips for Finals",
                            UpvoteCount = 35
                        },
                        new
                        {
                            Id = 2,
                            Author = "study.pro@example.com",
                            Content = "Online classes require discipline and focus. In this post, I will share some strategies to stay productive during virtual lectures and get the most out of your online education.",
                            CreatedDate = new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(75),
                            ImageUrl = "images/post2.webp",
                            Tag = "productivity, online classes, study",
                            Title = "How to Stay Productive During Online Classes",
                            UpvoteCount = 20
                        },
                        new
                        {
                            Id = 3,
                            Author = "time.manager@example.com",
                            Content = "Time management is crucial for balancing school work and personal life. In this post, I share five effective techniques, such as the Pomodoro method and daily planning, that can help students manage their time better.",
                            CreatedDate = new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(76),
                            ImageUrl = "images/post3.webp",
                            Tag = "time management, productivity, student life",
                            Title = "5 Effective Time Management Techniques for Students",
                            UpvoteCount = 40
                        },
                        new
                        {
                            Id = 4,
                            Author = "struggling.student@example.com",
                            Content = "I'm really struggling with the latest calculus assignment, specifically understanding how to solve integrals involving trigonometric functions. Does anyone have any tips or resources that could help me out?",
                            CreatedDate = new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(77),
                            Tag = "calculus, help, assignment",
                            Title = "Need Help with Calculus Assignment",
                            UpvoteCount = 15
                        },
                        new
                        {
                            Id = 5,
                            Author = "note.guru@example.com",
                            Content = "Taking effective notes is a skill every student should master. This post will guide you through the best note-taking techniques, including the Cornell method and mind mapping, to help you retain more information from lectures.",
                            CreatedDate = new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(78),
                            ImageUrl = "images/post2.webp",
                            Tag = "note-taking, study skills, college",
                            Title = "Best Note-Taking Methods for College Students",
                            UpvoteCount = 28
                        },
                        new
                        {
                            Id = 6,
                            Author = "bio.student@example.com",
                            Content = "Hey everyone! I'm looking for some good study notes for the upcoming biology exam, specifically about cell structure and function. If anyone has notes they could share, it would be a great help!",
                            CreatedDate = new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(79),
                            Tag = "biology, study notes, resources",
                            Title = "Anyone Have Good Biology Study Notes?",
                            UpvoteCount = 22
                        },
                        new
                        {
                            Id = 7,
                            Author = "no.more.procrastination@example.com",
                            Content = "Procrastination is every student's enemy. In this post, I'll share practical tips to overcome procrastination, build motivation, and develop effective study habits.",
                            CreatedDate = new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(80),
                            ImageUrl = "images/post1.webp",
                            Tag = "procrastination, study habits, motivation",
                            Title = "How to Avoid Procrastination as a Student",
                            UpvoteCount = 30
                        },
                        new
                        {
                            Id = 8,
                            Author = "teamwork.challenges@example.com",
                            Content = "Our group is having trouble meeting deadlines for our group project. Does anyone have advice on how to improve communication and divide tasks more effectively? Any tips would be greatly appreciated!",
                            CreatedDate = new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(81),
                            Tag = "group projects, teamwork, help",
                            Title = "Struggling with Group Project Deadlines",
                            UpvoteCount = 18
                        },
                        new
                        {
                            Id = 9,
                            Author = "study.setup@example.com",
                            Content = "A good study environment can significantly improve your focus and productivity. Here are some tips on how to create the perfect study space at home, including lighting, desk setup, and minimizing distractions.",
                            CreatedDate = new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(82),
                            ImageUrl = "images/post3.webp",
                            Tag = "study space, productivity, focus",
                            Title = "Creating the Perfect Study Space at Home",
                            UpvoteCount = 22
                        },
                        new
                        {
                            Id = 10,
                            Author = "physics.need.help@example.com",
                            Content = "I'm preparing for the upcoming physics exam and it would really help if I could go through the solutions from last year's exam. Does anyone have a copy or know where I can find it?",
                            CreatedDate = new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(83),
                            Tag = "physics, exam, help",
                            Title = "Does Anyone Have Solutions for Last Year's Physics Exam?",
                            UpvoteCount = 10
                        },
                        new
                        {
                            Id = 11,
                            Author = "exam.ready@example.com",
                            Content = "Exams can be stressful, but a solid preparation plan can make all the difference. In this post, I'll share a checklist for stress-free studying that will help you stay organized and confident during exam season.",
                            CreatedDate = new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(84),
                            ImageUrl = "images/post1.webp",
                            Tag = "exam prep, study guide, stress management",
                            Title = "Exam Preparation Checklist for Stress-Free Studying",
                            UpvoteCount = 25
                        },
                        new
                        {
                            Id = 12,
                            Author = "chemistry.lab.struggle@example.com",
                            Content = "Does anyone have any tips on writing good lab reports? I'm struggling with understanding what details to include in my chemistry lab reports and how to structure them properly.",
                            CreatedDate = new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(85),
                            Tag = "chemistry, lab reports, help",
                            Title = "Struggling with Chemistry Lab Reports",
                            UpvoteCount = 14
                        },
                        new
                        {
                            Id = 13,
                            Author = "math.group@example.com",
                            Content = "I'm looking to form a study group for Calculus II. If anyone is interested in meeting up once a week to go through problems and help each other out, please let me know!",
                            CreatedDate = new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(86),
                            Tag = "study group, calculus, math",
                            Title = "Math Study Group for Calculus II",
                            UpvoteCount = 23
                        },
                        new
                        {
                            Id = 14,
                            Author = "bio.flashcards@example.com",
                            Content = "I've made some flashcards for key biology terms that I'm using to prepare for the upcoming exam. If anyone wants to collaborate or needs access to these flashcards, let me know!",
                            CreatedDate = new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(87),
                            Tag = "flashcards, biology, study tools",
                            Title = "Flashcards for Biology Terminology",
                            UpvoteCount = 19
                        },
                        new
                        {
                            Id = 15,
                            Author = "history.buff@example.com",
                            Content = "I'm finding it difficult to memorize all the dates for my history exam. Does anyone have effective memorization techniques or tools that could help with learning historical timelines?",
                            CreatedDate = new DateTime(2024, 11, 29, 14, 49, 57, 893, DateTimeKind.Utc).AddTicks(88),
                            Tag = "history, memorization, study tips",
                            Title = "Effective Ways to Memorize Historical Dates",
                            UpvoteCount = 16
                        });
                });

            modelBuilder.Entity("api.Models.Upvote", b =>
                {
                    b.Property<int>("UpvoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PostId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("UpvoteId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Upvotes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Models.Comment", b =>
                {
                    b.HasOne("api.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("api.Models.Upvote", b =>
                {
                    b.HasOne("api.Models.Post", "Post")
                        .WithMany("Upvotes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("api.Models.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Upvotes");
                });
#pragma warning restore 612, 618
        }
    }
}
