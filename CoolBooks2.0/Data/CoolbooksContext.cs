﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CoolBooks.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CoolBooks.Areas.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoolBooks.Data
{
    public partial class CoolbooksContext : IdentityDbContext<ApplicationUser>
    {
        
        public CoolbooksContext(DbContextOptions<CoolbooksContext> options): base(options)
        {
        }

        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<BooksAuthors> BooksAuthors { get; set; }
        public virtual DbSet<BooksGenres> BooksGenres { get; set; }
        public virtual DbSet<BooksUsers> BooksUsers { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<Quotes> Quotes { get; set; }
        public virtual DbSet<ReviewComents> ReviewComents { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.


                optionsBuilder.UseSqlServer("Data Source=MARKUS\\MARKUSSQLEXPRESS;Initial Catalog=CoolBooks;Integrated Security=True;Connect Timeout=50;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


                //optionsBuilder.UseSqlServer("Data Source=LAPTOP-K1146D8H\\SQLEXPRESS;Initial Catalog=CoolBooks;Integrated Security=True;Connect Timeout=50;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

                //optionsBuilder.UseSqlServer("Data Source=DESKTOP-4JRD5JN\\SQLEXPRESS;Initial Catalog=CoolBooks;Integrated Security=True;Connect Timeout=50;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");



            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      
            modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BooksAuthors>(entity =>
            {
                entity.HasKey(e => new { e.BooksID, e.AuthorID })
                    .HasName("PK_BooksAuthor");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.BooksFromAutors)
                    .HasForeignKey(d => d.AuthorID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BooksAuthor_Author");

                entity.HasOne(d => d.Books)
                    .WithMany(p => p.AuthorsFromBooks)
                    .HasForeignKey(d => d.BooksID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BooksAuthor_Books");
            });

            modelBuilder.Entity<BooksGenres>(entity =>
            {
                entity.HasKey(e => new { e.BooksID, e.GenreID });

                entity.HasOne(d => d.Books)
                    .WithMany(p => p.GenresFromBooks)
                    .HasForeignKey(d => d.BooksID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BooksGenres_Books");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.BooksFromGenres)
                    .HasForeignKey(d => d.GenreID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BooksGenres_Author");
            });

            modelBuilder.Entity<BooksUsers>(entity =>
            {
                entity.HasKey(e => new { e.BooksID, e.ClientId });

            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.HasKey(e => e.GenreID)
                    .HasName("Pk_GenerID");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

        }
    }

    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u =>u.FirstName).HasMaxLength(255);
            builder.Property(u => u.LastName).HasMaxLength(255);
            builder.Property(u => u.Created).HasColumnType("datetime2");
        }
    }
}