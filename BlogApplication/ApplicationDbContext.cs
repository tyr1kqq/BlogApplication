using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlogApplication.Models;
using System.Data;

namespace BlogApplication
{
    public class ApplicationDbContext : DbContext
    {
        // Свойства, которые представляют таблицы в базе данных
        public DbSet<UserViewModel> Users { get; set; }
        public DbSet<ArticleViewModel> Articles { get; set; }
        public DbSet<TagViewModel> Tags { get; set; }
        public DbSet<CommentViewModel> Comments { get; set; }
        public DbSet<ArticleTagViewModel> ArticleTags { get; set; }
        public DbSet<RoleViewModel> Roles { get; set; }
        public DbSet<UserRoleViewModel> UserRoles { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        {
        }

        // Метод для настройки модели
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleTagViewModel>()
                .HasKey(at => new { at.ArticleID, at.TagID });

            modelBuilder.Entity<ArticleTagViewModel>()
                .HasOne(at => at.Article)
                .WithMany(a => a.ArticleTags)
                .HasForeignKey(at => at.ArticleID);

            modelBuilder.Entity<ArticleTagViewModel>()
                .HasOne(at => at.Tag)
                .WithMany(t => t.ArticleTags)
                .HasForeignKey(at => at.TagID);

            // Настройка составного ключа для UserRole
            modelBuilder.Entity<UserRoleViewModel>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            // Настройка отношений
            modelBuilder.Entity<UserViewModel>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<RoleViewModel>()
                .HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId);

            // Убедитесь, что все другие сущности правильно настроены
            modelBuilder.Entity<UserViewModel>()
                .HasKey(u => u.UserID);

            modelBuilder.Entity<ArticleViewModel>()
                .HasKey(a => a.ArticleID);

            modelBuilder.Entity<CommentViewModel>()
                .HasKey(c => c.CommentID);

            modelBuilder.Entity<TagViewModel>()
                .HasKey(t => t.TagID);

            base.OnModelCreating(modelBuilder);
        }
    }
}