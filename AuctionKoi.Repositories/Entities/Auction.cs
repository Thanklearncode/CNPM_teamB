using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AuctionKoi.Repositories.Entities;

public partial class Auction
{
    [Key]
    public int AuctionID { get; set; }

    public int? KoiID { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? StartPrice { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndTime { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? CurrentPrice { get; set; }

    public int? WinnerId { get; set; }

    [StringLength(50)]
    public string? Method { get; set; }

    [InverseProperty("Auction")]
    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    [ForeignKey("KoiID")]
    [InverseProperty("Auctions")]
    public virtual KoiFish? Koi { get; set; }

    [InverseProperty("Auction")]
    public virtual ICollection<TransactionHistory> TransactionHistories { get; set; } = new List<TransactionHistory>();

    [ForeignKey("WinnerId")]
    [InverseProperty("Auctions")]
    public virtual User? Winner { get; set; }
}
