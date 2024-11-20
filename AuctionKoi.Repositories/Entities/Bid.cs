using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AuctionKoi.Repositories.Entities;

public partial class Bid
{
    [Key]
    public int BidID { get; set; }

    public int? AuctionID { get; set; }

    public int? UserID { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? BidAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? BidTime { get; set; }

    [ForeignKey("AuctionID")]
    [InverseProperty("Bids")]
    public virtual Auction? Auction { get; set; }

    [ForeignKey("UserID")]
    [InverseProperty("Bids")]
    public virtual User? User { get; set; }
}
