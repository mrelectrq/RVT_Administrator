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
        public virtual DbSet<IdRelations> IdRelations { get; set; }
        public virtual DbSet<IdvnAccounts> IdvnAccounts { get; set; }
        public virtual DbSet<RegionRelated> RegionRelated { get; set; }
        public virtual DbSet<VoteStatus> VoteStatus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-B3HCEB1;Database=SFBD_Accounts;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConfirmAcc>(entity =>
            {
                entity.HasKey(e => e.Idvn)
                    .HasName("PK__confirm___9DBB341602E7C75E");

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

            modelBuilder.Entity<IdRelations>(entity =>
            {
                entity.HasKey(e => e.Idbd)
                    .HasName("PK__ID_relat__B87DA8C3A36772B0");

                entity.ToTable("ID_relations");

                entity.Property(e => e.Idbd)
                    .HasColumnName("IDBD")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Idvn)
                    .IsRequired()
                    .HasColumnName("IDVN")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IdvnAccounts>(entity =>
            {
                entity.HasKey(e => e.Idvn)
                    .HasName("PK__idvn_acc__B87C0A4432663353");

                entity.ToTable("idvn_accounts");

                entity.Property(e => e.Idvn)
                    .HasColumnName("IDVN")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BirthYear).HasColumnName("Birth_Year");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IpAddress)
                    .HasColumnName("IP_address")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("Phone_Number")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PublicSector)
                    .HasColumnName("Public_Sector")
                    .HasMaxLength(250)
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

            modelBuilder.Entity<RegionRelated>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Region_Related");

                entity.Property(e => e.IntValue).HasColumnName("Int_value");

                entity.Property(e => e.RegionName)
                    .HasColumnName("Region_Name")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VoteStatus>(entity =>
            {
                entity.HasKey(e => e.Idvn)
                    .HasName("PK__VoteStat__B87C0A4487281983");

                entity.Property(e => e.Idvn)
                    .HasColumnName("IDVN")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.VoteState)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
