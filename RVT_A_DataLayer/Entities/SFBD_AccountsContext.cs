using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RVT_A_DataLayer.Entities
{
    public partial class SFBD_AccountsContext : DbContext
    {
        public SFBD_AccountsContext()
        {
        }

        public SFBD_AccountsContext(DbContextOptions<SFBD_AccountsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ConfirmAcc> ConfirmAcc { get; set; }
        public virtual DbSet<FiscData> FiscData { get; set; }
        public virtual DbSet<IdvnAccounts> IdvnAccounts { get; set; }
        public virtual DbSet<Parties> Parties { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }
        public virtual DbSet<VoteStatus> VoteStatus { get; set; }
        public virtual DbSet<Votes> Votes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=WINDOWS-NTDD448;Database=SFBD_Accounts;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConfirmAcc>(entity =>
            {
                entity.HasKey(e => e.Idvn)
                    .HasName("PK__confirm___9DBB34166C8F4440");

                entity.ToTable("confirm_acc");

                entity.Property(e => e.Idvn)
                    .HasColumnName("idvn")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ConfirmKey)
                    .HasColumnName("confirm_key")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DateConfirm)
                    .HasColumnName("date_confirm")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateExpirationKey)
                    .HasColumnName("date_expiration_key")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateRegKey)
                    .HasColumnName("date_reg_key")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<FiscData>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Fisc_Data");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("Birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.Gender)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Idnp)
                    .HasColumnName("IDNP")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Region)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IdvnAccounts>(entity =>
            {
                entity.HasKey(e => e.Idvn)
                    .HasName("PK__idvn_acc__B87C0A442D77B05D");

                entity.ToTable("idvn_accounts");

                entity.Property(e => e.Idvn)
                    .HasColumnName("IDVN")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IpAddress)
                    .HasColumnName("IP_address")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("Phone_Number")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterDate)
                    .HasColumnName("Register_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.StatusNumber)
                    .HasColumnName("Status_Number")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.VnPassword)
                    .HasColumnName("vn_password")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Parties>(entity =>
            {
                entity.HasKey(e => e.Idpart);

                entity.Property(e => e.Idpart).HasColumnName("IDPart");

                entity.Property(e => e.Party)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Regions>(entity =>
            {
                entity.HasKey(e => e.Idreg);

                entity.Property(e => e.Idreg).HasColumnName("IDReg");
            });

            modelBuilder.Entity<VoteStatus>(entity =>
            {
                entity.HasKey(e => e.Idvn)
                    .HasName("PK__VoteStat__B87C0A443DAA6419");

                entity.Property(e => e.Idvn)
                    .HasColumnName("IDVN")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.VoteState)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Votes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PartyChoosed).HasColumnName("Party_Choosed");

                entity.HasOne(d => d.PartyChoosedNavigation)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.PartyChoosed)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Votes_Parties");

                entity.HasOne(d => d.RegionNavigation)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.Region)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Votes_Regions");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
