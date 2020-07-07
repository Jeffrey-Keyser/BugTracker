using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Areas.Identity.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BugTracker.Data
{
    public class BugTrackerContext : IdentityDbContext<BugTrackerUser>
    {
        // For reading user information
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BugTrackerContext(DbContextOptions<BugTrackerContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);


            // UserProject Config (Many-to-Many)

            builder.Entity<UserProject>()
                .HasKey(bc => new { bc.projectId, bc.Id });

            builder.Entity<UserProject>()
                .HasOne(bc => bc.Project)
                .WithMany(b => b.UserProjects)
                .HasForeignKey(bc => bc.projectId);

            builder.Entity<UserProject>()
                .HasOne(bc => bc.BugTrackerUser)
                .WithMany(b => b.UserProjects)
                .HasForeignKey(bc => bc.Id);


            // UserFriend Config

            builder.Entity<UserFriend>()
                .HasKey(bc => new { bc.senderId, bc.recieverId });


            /*  builder.Entity<UserFriend>()
                  .HasOne(bc => bc.sender)
                  .WithMany(b => b.UserFriends)
                  .HasForeignKey(bc => bc.senderId); */

            builder.Entity<UserFriend>()
                .HasOne(bc => bc.reciever)
                .WithMany(b => b.UserFriends)
                .HasForeignKey(bc => bc.recieverId)
                .OnDelete(DeleteBehavior.Restrict);


            // UserNotification Config

            builder.Entity<UserNotification>()
                .HasKey(bc => new { bc.AffectedId, bc.AuthorId });

            /*
            builder.Entity<UserNotification>()
                .HasOne(bc => bc.AffectedUser)
                .WithMany(b => b.UserNotifications) */


        }

        public DbSet<Projects> Projects { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
       
        public DbSet<UserModel> UserModels { get; set; }

        public DbSet<UserProject> UserProjects { get; set; }


        public DbSet<UserFriend> UserFriends { get; set; }

        public DbSet<UserNotification> UserNotifications { get; set; }


    }
}
