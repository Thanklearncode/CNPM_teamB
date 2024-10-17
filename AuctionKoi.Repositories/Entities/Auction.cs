using System;
using System.Collections.Generic;

namespace AuctionKoi.Repositories.Entities;

public partial class Auction
{
    public int AuctionId { get; set; }

    public int? KoiId { get; set; }

    public decimal? StartPrice { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public virtual KoiFish? Koi { get; set; }

    public virtual ICollection<TransactionHistory> TransactionHistories { get; set; } = new List<TransactionHistory>();
}
