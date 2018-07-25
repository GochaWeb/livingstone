namespace livingstone.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Livingstone : DbContext
    {
        public Livingstone()
            : base("name=livingstone")
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CoverPhoto> CoverPhotoes { get; set; }
        public virtual DbSet<ProfilePhoto> ProfilePhotoes { get; set; }
        public virtual DbSet<Reply> Replies { get; set; }
        public virtual DbSet<RetweetReply> RetweetReplies { get; set; }
        public virtual DbSet<Retweet> Retweets { get; set; }
        public virtual DbSet<Following> Followings { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasMany(e => e.Replies)
                .WithRequired(e => e.Comment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoverPhoto>()
                .Property(e => e.Photo)
                .IsUnicode(false);

            modelBuilder.Entity<ProfilePhoto>()
                .Property(e => e.Photo)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Confirmation)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.CoverPhotoes)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ProfilePhotoes)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Replies)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                  .HasMany(e => e.Retweets)
                  .WithRequired(e => e.User)
                  .WillCascadeOnDelete(false);
            modelBuilder.Entity<Comment>()
                .HasMany(e => e.Retweets)
                .WithRequired(e => e.Comment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Retweet>()
                .HasMany(e => e.RetweetReplies)
                .WithRequired(e => e.Retweet)
                .WillCascadeOnDelete(false);
                
        }
    }
}
