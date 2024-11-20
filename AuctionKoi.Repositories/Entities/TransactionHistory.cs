using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AuctionKoi.Repositories.Entities;

[Table("TransactionHistory")]
public partial class TransactionHistory
{
    [Key]
    public int TransactionID { get; set; }

    public int? AuctionID { get; set; }

    public int? BuyerID { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? TotalAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TransactionDate { get; set; }

    [StringLength(50)]
    public string? AuctionType { get; set; }

    [ForeignKey("AuctionID")]
    [InverseProperty("TransactionHistories")]
    public virtual Auction? Auction { get; set; }

    [ForeignKey("BuyerID")]
    [InverseProperty("TransactionHistories")]
    public virtual User? Buyer { get; set; }
}
