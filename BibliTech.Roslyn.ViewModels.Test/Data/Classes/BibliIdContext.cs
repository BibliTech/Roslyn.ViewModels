using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BibliId.Web.Models.Entities
{
    public partial class BibliIdContext : DbContext
    {
        public BibliIdContext()
        {
        }

        public BibliIdContext(DbContextOptions<BibliIdContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountClaim> AccountClaim { get; set; }
        public virtual DbSet<AccountEmail> AccountEmail { get; set; }
        public virtual DbSet<LoginClient> LoginClient { get; set; }
        public virtual DbSet<LoginService> LoginService { get; set; }
        public virtual DbSet<TokenRevocation> TokenRevocation { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasIndex(e => e.Name);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<AccountClaim>(entity =>
            {
                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_AccountClaim_ByAccount");

                entity.HasIndex(e => e.Type)
                    .HasName("IX_AccountClaim_ByType");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountClaim)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountClaim_Account");
            });

            modelBuilder.Entity<AccountEmail>(entity =>
            {
                entity.HasIndex(e => e.AccountId)
                    .HasName("IX_AccountEmail_ByAccount");

                entity.HasIndex(e => e.Email);

                entity.HasIndex(e => new { e.LoginServiceId, e.ExternalAccountId })
                    .HasName("IX_AccountEmail_ByExternalId");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ExternalAccountId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountEmail)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountEmail_Account");

                entity.HasOne(d => d.LoginService)
                    .WithMany(p => p.AccountEmail)
                    .HasForeignKey(d => d.LoginServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountEmail_LoginService");
            });

            modelBuilder.Entity<LoginClient>(entity =>
            {
                entity.HasIndex(e => e.PublicKey)
                    .HasName("IX_LoginApp_PublicKey");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PublicKey)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.RedirectUri)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LoginService>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TokenRevocation>(entity =>
            {
                entity.HasIndex(e => new { e.AccountId, e.TokenHash })
                    .HasName("IX_TokenRevocation_ByAccountToken");

                entity.Property(e => e.NaturalExpirationTime).HasColumnType("datetime");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.RevokeTime).HasColumnType("datetime");

                entity.Property(e => e.TokenHash)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TokenRevocation)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TokenRevocation_Account");
            });
        }
    }
}
