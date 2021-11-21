using BlinkCash.Core.Models;
using BlinkCash.Data.Entities; 
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Plan> Plan { get; set; } 
        public virtual DbSet<PlanHistory> PlanHistory { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Wallet> Wallet { get; set; }
        public virtual DbSet<WalletBalance> WalletBalance { get; set; }
        public virtual DbSet<WithdrawalAccount> WithdrawalAccount { get; set; }
        public virtual DbSet<TransactionLog> TransactionLogs { get; set; }   
        public virtual DbSet<UserBank> UserBank { get; set; } 
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<CardRequest> CardRequest { get; set; }
        public virtual DbSet<WithDrawalSetting> WithDrawalSetting { get; set; } 
        public virtual DbSet<StandingOrder> StandingOrder { get; set; }  
        
        public virtual DbSet<SavingsConfiguration> SavingsConfiguration { get; set; } 
        public virtual DbSet<LoanConfiguration> LoanConfiguration { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Wallet>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Wallet>().Property(p => p.RowVersion).IsConcurrencyToken(); 
            modelBuilder.Entity<WalletBalance>().HasIndex(p => new { p.WalletId, p.BalanceType }).IsUnique();
        }
    }
}
