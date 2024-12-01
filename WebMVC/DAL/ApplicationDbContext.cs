using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebMVC.Models;

namespace WebMVC.DAL
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Likes> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Title = "Top 10 Study Tips for Finals",
                    Tag = "study, finals, productivity",
                    Author = "student.expert@example.com",
                    Content = "Final exams can be overwhelming, but with proper preparation, you can ace them. Here are the top 10 study tips that helped me improve my grades and stay focused during finals.",
                    ImageUrl = "images/post1.webp",
                    CreatedDate = DateTime.UtcNow,
                    Upvotes = 0
                },
                new Post
                {
                    Id = 2,
                    Title = "How to Stay Productive During Online Classes",
                    Tag = "productivity, online classes, study",
                    Author = "study.pro@example.com",
                    Content = "Online classes require discipline and focus. In this post, I will share some strategies to stay productive during virtual lectures and get the most out of your online education.",
                    ImageUrl = "images/post2.webp",
                    CreatedDate = DateTime.UtcNow,
                    Upvotes = 0
                },
                new Post
                {
                    Id = 3,
                    Title = "5 Effective Time Management Techniques for Students",
                    Tag = "time management, productivity, student life",
                    Author = "time.manager@example.com",
                    Content = "Time management is crucial for balancing school work and personal life. In this post, I share five effective techniques, such as the Pomodoro method and daily planning, that can help students manage their time better.",
                    ImageUrl = "images/post3.webp",
                    CreatedDate = DateTime.UtcNow,
                    Upvotes = 0
                },
                new Post
                {
                    Id = 4,
                    Title = "Need Help with Calculus Assignment",
                    Tag = "calculus, help, assignment",
                    Author = "struggling.student@example.com",
                    Content = "I'm really struggling with the latest calculus assignment, specifically understanding how to solve integrals involving trigonometric functions. Does anyone have any tips or resources that could help me out?",
                    ImageUrl = null,
                    CreatedDate = DateTime.UtcNow,
                    Upvotes = 0
                },
                new Post
                {
                    Id = 5,
                    Title = "Best Note-Taking Methods for College Students",
                    Tag = "note-taking, study skills, college",
                    Author = "note.guru@example.com",
                    Content = "Taking effective notes is a skill every student should master. This post will guide you through the best note-taking techniques, including the Cornell method and mind mapping, to help you retain more information from lectures.",
                    ImageUrl = "images/post2.webp",
                    CreatedDate = DateTime.UtcNow,
                    Upvotes = 0
                },
                new Post
                {
                    Id = 6,
                    Title = "Anyone Have Good Biology Study Notes?",
                    Tag = "biology, study notes, resources",
                    Author = "bio.student@example.com",
                    Content = "Hey everyone! I'm looking for some good study notes for the upcoming biology exam, specifically about cell structure and function. If anyone has notes they could share, it would be a great help!",
                    ImageUrl = null,
                    CreatedDate = DateTime.UtcNow,
                    Upvotes = 0
                },
                new Post
                {
                    Id = 7,
                    Title = "How to Avoid Procrastination as a Student",
                    Tag = "procrastination, study habits, motivation",
                    Author = "no.more.procrastination@example.com",
                    Content = "Procrastination is every student's enemy. In this post, I'll share practical tips to overcome procrastination, build motivation, and develop effective study habits.",
                    ImageUrl = "images/post1.webp",
                    CreatedDate = DateTime.UtcNow,
                    Upvotes = 0
                },
                new Post
                {
                    Id = 8,
                    Title = "Struggling with Group Project Deadlines",
                    Tag = "group projects, teamwork, help",
                    Author = "teamwork.challenges@example.com",
                    Content = "Our group is having trouble meeting deadlines for our group project. Does anyone have advice on how to improve communication and divide tasks more effectively? Any tips would be greatly appreciated!",
                    ImageUrl = null,
                    CreatedDate = DateTime.UtcNow,
                    Upvotes = 0
                },
                new Post
                {
                    Id = 9,
                    Title = "Creating the Perfect Study Space at Home",
                    Tag = "study space, productivity, focus",
                    Author = "study.setup@example.com",
                    Content = "A good study environment can significantly improve your focus and productivity. Here are some tips on how to create the perfect study space at home, including lighting, desk setup, and minimizing distractions.",
                    ImageUrl = "images/post3.webp",
                    CreatedDate = DateTime.UtcNow,
                    Upvotes = 0
                },
                new Post
                {
                    Id = 10,
                    Title = "Does Anyone Have Solutions for Last Year's Physics Exam?",
                    Tag = "physics, exam, help",
                    Author = "physics.need.help@example.com",
                    Content = "I'm preparing for the upcoming physics exam and it would really help if I could go through the solutions from last year's exam. Does anyone have a copy or know where I can find it?",
                    ImageUrl = null,
                    CreatedDate = DateTime.UtcNow,
                    Upvotes = 0
                },
                new Post
                {
                    Id = 11,
                    Title = "Exam Preparation Checklist for Stress-Free Studying",
                    Tag = "exam prep, study guide, stress management",
                    Author = "exam.ready@example.com",
                    Content = "Exams can be stressful, but a solid preparation plan can make all the difference. In this post, I'll share a checklist for stress-free studying that will help you stay organized and confident during exam season.",
                    ImageUrl = "images/post1.webp",
                    CreatedDate = DateTime.UtcNow,
                    Upvotes = 0
                },
                new Post
                {
                    Id = 12,
                    Title = "Struggling with Chemistry Lab Reports",
                    Tag = "chemistry, lab reports, help",
                    Author = "chemistry.lab.struggle@example.com",
                    Content = "Does anyone have any tips on writing good lab reports? I'm struggling with understanding what details to include in my chemistry lab reports and how to structure them properly.",
                    ImageUrl = null,
                    CreatedDate = DateTime.UtcNow,
                    Upvotes = 0
                },
                new Post
                {
                    Id = 13,
                    Title = "Math Study Group for Calculus II",
                    Tag = "study group, calculus, math",
                    Author = "math.group@example.com",
                    Content = "I'm looking to form a study group for Calculus II. If anyone is interested in meeting up once a week to go through problems and help each other out, please let me know!",
                    ImageUrl = null,
                    CreatedDate = DateTime.UtcNow,
                    Upvotes = 0
                },
                new Post
                {
                    Id = 14,
                    Title = "Flashcards for Biology Terminology",
                    Tag = "flashcards, biology, study tools",
                    Author = "bio.flashcards@example.com",
                    Content = "I've made some flashcards for key biology terms that I'm using to prepare for the upcoming exam. If anyone wants to collaborate or needs access to these flashcards, let me know!",
                    ImageUrl = null,
                    CreatedDate = DateTime.UtcNow,
                    Upvotes = 0
                },
                new Post
                {
                    Id = 15,
                    Title = "Effective Ways to Memorize Historical Dates",
                    Tag = "history, memorization, study tips",
                    Author = "history.buff@example.com",
                    Content = "I'm finding it difficult to memorize all the dates for my history exam. Does anyone have effective memorization techniques or tools that could help with learning historical timelines?",
                    ImageUrl = null,
                    CreatedDate = DateTime.UtcNow,
                    Upvotes = 0
                }
            );
        }
    }
}