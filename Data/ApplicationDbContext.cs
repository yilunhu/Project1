using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using abcBank.Models;

namespace abcBank.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Translog> Translogs { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // configures one-to-many relationship
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Account>()                
        //        .HasRequired<Customer>(s => s.CustID)
        //        .WithMany(g => g.Accounts)
        //        .HasForeignKey<int>(s => s.);
        //}

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<Account>()
        //     .HasOne(c => c.CustID)
        //     .WithMany(x => x.AccOwned)
        //     .HasForeignKey(f => f.CustomerID)
        //     .HasConstraintName("AccountId")
        //     .OnDelete(DeleteBehavior.Cascade)
        //      .IsRequired();

        //    base.OnModelCreating(builder);
        //    builder.Entity<Translog>()
        //     .HasOne(e => e.AccId)
        //     .WithMany(x => x.Transaction)
        //     .HasForeignKey(f => f.AccountID)
        //     .HasConstraintName("AccID")
        //     .OnDelete(DeleteBehavior.Cascade)
        //      .IsRequired();
        //}

    }
}
