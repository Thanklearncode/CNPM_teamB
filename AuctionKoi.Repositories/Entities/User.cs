using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AuctionKoi.Repositories.Entities;

public partial class User
{
    [Key]
    public int UserID { get; set; }

    [StringLength(50)]
    public string Username { get; set; } = null!;

    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(100)]
    public string? FullName { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    public int? RoleID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Winner")]
    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();

    [InverseProperty("User")]
    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    [InverseProperty("User")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [InverseProperty("Owner")]
    public virtual ICollection<KoiFish> KoiFishes { get; set; } = new List<KoiFish>();

    [ForeignKey("RoleID")]
    [InverseProperty("Users")]
    public virtual Role? Role { get; set; }

    [InverseProperty("Buyer")]
    public virtual ICollection<TransactionHistory> TransactionHistories { get; set; } = new List<TransactionHistory>();
}
