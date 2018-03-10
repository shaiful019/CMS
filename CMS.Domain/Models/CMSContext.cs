using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using CMS.Domain.ViewModels;

namespace CMS.Domain.Models
{
    public class CMSContext : DbContext
    {

        public CMSContext(DbContextOptions<CMSContext> options) : base(options)
        {

        }

        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Term> Term { get; set; }
        public DbSet<PostTerm> PostTerm { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{

        //    builder.Entity<PostTerm>().HasKey(k => new { k.PostID, k.TermID });

        //    builder.Entity<PostTerm>()
        //        .HasOne(x => x.Post)
        //        .WithMany(x => x.PostTerms)
        //        .HasForeignKey(x => x.PostID);

        //    builder.Entity<PostTerm>()
        //       .HasOne(x => x.Term)
        //       .WithMany(x => x.PostTerms)
        //       .HasForeignKey(x => x.TermID);

        //    base.OnModelCreating(builder);
        //}



    }
}
