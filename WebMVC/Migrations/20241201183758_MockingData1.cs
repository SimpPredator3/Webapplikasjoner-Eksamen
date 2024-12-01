using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebMVC.Migrations
{
    /// <inheritdoc />
    public partial class MockingData1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Author", "Content", "CreatedDate", "ImageUrl", "Tag", "Title", "Upvotes" },
                values: new object[,]
                {
                    { 1, "student.expert@example.com", "Final exams can be overwhelming, but with proper preparation, you can ace them. Here are the top 10 study tips that helped me improve my grades and stay focused during finals.", new DateTime(2024, 12, 1, 18, 37, 57, 589, DateTimeKind.Utc).AddTicks(4687), "images/post1.webp", "study, finals, productivity", "Top 10 Study Tips for Finals", 0 },
                    { 2, "study.pro@example.com", "Online classes require discipline and focus. In this post, I will share some strategies to stay productive during virtual lectures and get the most out of your online education.", new DateTime(2024, 12, 1, 18, 37, 57, 589, DateTimeKind.Utc).AddTicks(4689), "images/post2.webp", "productivity, online classes, study", "How to Stay Productive During Online Classes", 0 },
                    { 3, "time.manager@example.com", "Time management is crucial for balancing school work and personal life. In this post, I share five effective techniques, such as the Pomodoro method and daily planning, that can help students manage their time better.", new DateTime(2024, 12, 1, 18, 37, 57, 589, DateTimeKind.Utc).AddTicks(4689), "images/post3.webp", "time management, productivity, student life", "5 Effective Time Management Techniques for Students", 0 },
                    { 4, "struggling.student@example.com", "I'm really struggling with the latest calculus assignment, specifically understanding how to solve integrals involving trigonometric functions. Does anyone have any tips or resources that could help me out?", new DateTime(2024, 12, 1, 18, 37, 57, 589, DateTimeKind.Utc).AddTicks(4690), null, "calculus, help, assignment", "Need Help with Calculus Assignment", 0 },
                    { 5, "note.guru@example.com", "Taking effective notes is a skill every student should master. This post will guide you through the best note-taking techniques, including the Cornell method and mind mapping, to help you retain more information from lectures.", new DateTime(2024, 12, 1, 18, 37, 57, 589, DateTimeKind.Utc).AddTicks(4691), "images/post2.webp", "note-taking, study skills, college", "Best Note-Taking Methods for College Students", 0 },
                    { 6, "bio.student@example.com", "Hey everyone! I'm looking for some good study notes for the upcoming biology exam, specifically about cell structure and function. If anyone has notes they could share, it would be a great help!", new DateTime(2024, 12, 1, 18, 37, 57, 589, DateTimeKind.Utc).AddTicks(4692), null, "biology, study notes, resources", "Anyone Have Good Biology Study Notes?", 0 },
                    { 7, "no.more.procrastination@example.com", "Procrastination is every student's enemy. In this post, I'll share practical tips to overcome procrastination, build motivation, and develop effective study habits.", new DateTime(2024, 12, 1, 18, 37, 57, 589, DateTimeKind.Utc).AddTicks(4693), "images/post1.webp", "procrastination, study habits, motivation", "How to Avoid Procrastination as a Student", 0 },
                    { 8, "teamwork.challenges@example.com", "Our group is having trouble meeting deadlines for our group project. Does anyone have advice on how to improve communication and divide tasks more effectively? Any tips would be greatly appreciated!", new DateTime(2024, 12, 1, 18, 37, 57, 589, DateTimeKind.Utc).AddTicks(4694), null, "group projects, teamwork, help", "Struggling with Group Project Deadlines", 0 },
                    { 9, "study.setup@example.com", "A good study environment can significantly improve your focus and productivity. Here are some tips on how to create the perfect study space at home, including lighting, desk setup, and minimizing distractions.", new DateTime(2024, 12, 1, 18, 37, 57, 589, DateTimeKind.Utc).AddTicks(4695), "images/post3.webp", "study space, productivity, focus", "Creating the Perfect Study Space at Home", 0 },
                    { 10, "physics.need.help@example.com", "I'm preparing for the upcoming physics exam and it would really help if I could go through the solutions from last year's exam. Does anyone have a copy or know where I can find it?", new DateTime(2024, 12, 1, 18, 37, 57, 589, DateTimeKind.Utc).AddTicks(4696), null, "physics, exam, help", "Does Anyone Have Solutions for Last Year's Physics Exam?", 0 },
                    { 11, "exam.ready@example.com", "Exams can be stressful, but a solid preparation plan can make all the difference. In this post, I'll share a checklist for stress-free studying that will help you stay organized and confident during exam season.", new DateTime(2024, 12, 1, 18, 37, 57, 589, DateTimeKind.Utc).AddTicks(4696), "images/post1.webp", "exam prep, study guide, stress management", "Exam Preparation Checklist for Stress-Free Studying", 0 },
                    { 12, "chemistry.lab.struggle@example.com", "Does anyone have any tips on writing good lab reports? I'm struggling with understanding what details to include in my chemistry lab reports and how to structure them properly.", new DateTime(2024, 12, 1, 18, 37, 57, 589, DateTimeKind.Utc).AddTicks(4697), null, "chemistry, lab reports, help", "Struggling with Chemistry Lab Reports", 0 },
                    { 13, "math.group@example.com", "I'm looking to form a study group for Calculus II. If anyone is interested in meeting up once a week to go through problems and help each other out, please let me know!", new DateTime(2024, 12, 1, 18, 37, 57, 589, DateTimeKind.Utc).AddTicks(4698), null, "study group, calculus, math", "Math Study Group for Calculus II", 0 },
                    { 14, "bio.flashcards@example.com", "I've made some flashcards for key biology terms that I'm using to prepare for the upcoming exam. If anyone wants to collaborate or needs access to these flashcards, let me know!", new DateTime(2024, 12, 1, 18, 37, 57, 589, DateTimeKind.Utc).AddTicks(4699), null, "flashcards, biology, study tools", "Flashcards for Biology Terminology", 0 },
                    { 15, "history.buff@example.com", "I'm finding it difficult to memorize all the dates for my history exam. Does anyone have effective memorization techniques or tools that could help with learning historical timelines?", new DateTime(2024, 12, 1, 18, 37, 57, 589, DateTimeKind.Utc).AddTicks(4700), null, "history, memorization, study tips", "Effective Ways to Memorize Historical Dates", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
