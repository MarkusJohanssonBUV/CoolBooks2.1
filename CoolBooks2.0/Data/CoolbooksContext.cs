﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CoolBooks.Models;

namespace CoolBooks.Data
{
    public partial class CoolbooksContext : DbContext
    {
        public CoolbooksContext()
        {
        }

        public CoolbooksContext(DbContextOptions<CoolbooksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<BooksUsers> BooksUsers { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=MARKUS\\MARKUSSQLEXPRESS;Initial Catalog=Coolbooks4;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authors>(entity =>
            {
                entity.HasKey(e => e.AuthorID)
                    .HasName("Pk_AuthorID");
            });

            modelBuilder.Entity<Books>(entity =>
            {
                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_AuthorID");

                entity.HasOne(d => d.Gener)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.GenerID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Genres");
            });

            modelBuilder.Entity<BooksUsers>(entity =>
            {
                entity.HasKey(e => new { e.BooksID, e.UserID });

                entity.HasOne(d => d.Books)
                    .WithMany(p => p.BooksUsers)
                    .HasForeignKey(d => d.BooksID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BooksUsers_Books");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BooksUsers)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BooksUsers_UserID");
            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.HasKey(e => e.GenerID)
                    .HasName("Pk_GenerID");
            });

            modelBuilder.Entity<Reviews>(entity =>
            {
                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.BookID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Books_Reviews");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_User_Reviews");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}