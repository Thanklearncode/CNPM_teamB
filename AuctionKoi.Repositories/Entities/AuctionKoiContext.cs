using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AuctionKoi.Repositories.Entities;

public partial class AuctionKoiContext : DbContext
{
    public AuctionKoiContext()
    {
    }

    public AuctionKoiContext(DbContextOptions<AuctionKoiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auction> Auctions { get; set; }

    public virtual DbSet<Bid> Bids { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<KoiFish> KoiFishes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TransactionHistory> TransactionHistories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MSI\\THANH;Initial Catalog=AuctionKoi;Persist Security Info=True;User ID=sa;Password=123456;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auction>(entity =>
        {
            entity.HasKey(e => e.AuctionID).HasName("PK__Auctions__51004A2C17C17F65");

            entity.Property(e => e.AuctionID).HasColumnName("AuctionID");
            entity.Property(e => e.CurrentPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.KoiID).HasColumnName("KoiID");
            entity.Property(e => e.Method).HasMaxLength(50);
            entity.Property(e => e.StartPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Koi).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.KoiID)
                .HasConstraintName("FK__Auctions__KoiID__403A8C7D");

            entity.HasOne(d => d.Winner).WithMany(p => p.Auctions)
                .HasForeignKey(d => d.WinnerId)
                .HasConstraintName("FK_Auction_WinnerId");
        });

        modelBuilder.Entity<Bid>(entity =>
        {
            entity.HasKey(e => e.BidID).HasName("PK__Bids__4A733DB28859B361");

            entity.Property(e => e.BidID).HasColumnName("BidID");
            entity.Property(e => e.AuctionID).HasColumnName("AuctionID");
            entity.Property(e => e.BidAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BidTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserID).HasColumnName("UserID");

            entity.HasOne(d => d.Auction).WithMany(p => p.Bids)
                .HasForeignKey(d => d.AuctionID)
                .HasConstraintName("FK__Bids__AuctionID__4316F928");

            entity.HasOne(d => d.User).WithMany(p => p.Bids)
                .HasForeignKey(d => d.UserID)
                .HasConstraintName("FK__Bids__UserID__440B1D61");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeID).HasName("PK__Employee__7AD04FF195D35AA0");

            entity.Property(e => e.EmployeeID).HasColumnName("EmployeeID");
            entity.Property(e => e.HireDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Position).HasMaxLength(50);
            entity.Property(e => e.UserID).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Employees)
                .HasForeignKey(d => d.UserID)
                .HasConstraintName("FK__Employees__UserI__47DBAE45");
        });

        modelBuilder.Entity<KoiFish>(entity =>
        {
            entity.HasKey(e => e.KoiID).HasName("PK__KoiFish__E03435B8C9E90E43");

            entity.ToTable("KoiFish");

            entity.Property(e => e.KoiID).HasColumnName("KoiID");
            entity.Property(e => e.Breed).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.KoiName).HasMaxLength(100);
            entity.Property(e => e.KoiSex).HasMaxLength(10);
            entity.Property(e => e.Length).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.OwnerID).HasColumnName("OwnerID");

            entity.HasOne(d => d.Owner).WithMany(p => p.KoiFishes)
                .HasForeignKey(d => d.OwnerID)
                .HasConstraintName("FK__KoiFish__OwnerID__3D5E1FD2");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleID).HasName("PK__Roles__8AFACE3AA6503C2C");

            entity.Property(e => e.RoleID).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<TransactionHistory>(entity =>
        {
            entity.HasKey(e => e.TransactionID).HasName("PK__Transact__55433A4B1335F116");

            entity.ToTable("TransactionHistory");

            entity.Property(e => e.TransactionID).HasColumnName("TransactionID");
            entity.Property(e => e.AuctionID).HasColumnName("AuctionID");
            entity.Property(e => e.AuctionType).HasMaxLength(50);
            entity.Property(e => e.BuyerID).HasColumnName("BuyerID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Auction).WithMany(p => p.TransactionHistories)
                .HasForeignKey(d => d.AuctionID)
                .HasConstraintName("FK__Transacti__Aucti__4BAC3F29");

            entity.HasOne(d => d.Buyer).WithMany(p => p.TransactionHistories)
                .HasForeignKey(d => d.BuyerID)
                .HasConstraintName("FK__Transacti__Buyer__4CA06362");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserID).HasName("PK__Users__1788CCAC037E37F6");

            entity.Property(e => e.UserID).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.RoleID).HasColumnName("RoleID");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleID)
                .HasConstraintName("FK__Users__RoleID__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
