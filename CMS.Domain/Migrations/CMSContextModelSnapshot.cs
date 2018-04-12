﻿// <auto-generated />
using CMS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CMS.Domain.Migrations
{
    [DbContext(typeof(CMSContext))]
    partial class CMSContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CMS.Domain.Models.Comment", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CommentTime");

                    b.Property<string>("CommentedBy");

                    b.Property<string>("Content");

                    b.Property<int>("IsApproved");

                    b.Property<int>("ParentID");

                    b.Property<int>("PostID");

                    b.HasKey("CommentID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("CMS.Domain.Models.Notification", b =>
                {
                    b.Property<int>("NotificationID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CommentID");

                    b.Property<DateTime>("Notificationtime");

                    b.Property<int>("PostID");

                    b.Property<int>("Status");

                    b.Property<string>("UserID");

                    b.HasKey("NotificationID");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("CMS.Domain.Models.Post", b =>
                {
                    b.Property<int>("PostID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("FeaturedImageUrl");

                    b.Property<int>("IsDeleted");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("Url");

                    b.HasKey("PostID");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("CMS.Domain.Models.PostStatus", b =>
                {
                    b.Property<int>("PostStatusID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PostID");

                    b.Property<int>("ViewCount");

                    b.HasKey("PostStatusID");

                    b.HasIndex("PostID")
                        .IsUnique();

                    b.ToTable("PostStatus");
                });

            modelBuilder.Entity("CMS.Domain.Models.PostTerm", b =>
                {
                    b.Property<int>("PostTermID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PostID");

                    b.Property<int>("TermID");

                    b.HasKey("PostTermID");

                    b.HasIndex("PostID");

                    b.HasIndex("TermID");

                    b.ToTable("PostTerm");
                });

            modelBuilder.Entity("CMS.Domain.Models.Term", b =>
                {
                    b.Property<int>("TermID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("Type");

                    b.HasKey("TermID");

                    b.ToTable("Term");
                });

            modelBuilder.Entity("CMS.Domain.Models.PostStatus", b =>
                {
                    b.HasOne("CMS.Domain.Models.Post", "Post")
                        .WithOne("PostViewStatus")
                        .HasForeignKey("CMS.Domain.Models.PostStatus", "PostID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CMS.Domain.Models.PostTerm", b =>
                {
                    b.HasOne("CMS.Domain.Models.Post", "Post")
                        .WithMany("PostTerms")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CMS.Domain.Models.Term", "Term")
                        .WithMany("PostTerms")
                        .HasForeignKey("TermID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
