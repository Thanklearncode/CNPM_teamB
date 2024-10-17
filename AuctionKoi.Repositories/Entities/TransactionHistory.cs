using System;
using System.Collections.Generic;

namespace AuctionKoi.Repositories.Entities;

public partial class TransactionHistory
{
    public int TransactionId { get; set; }

    public int? AuctionId { get; set; }

    public int? BuyerId { get; set; }

    public decimal? TotalAmount { get; set; }

    public DateTime? TransactionDate { get; set; }

    public virtual Auction? Auction { get; set; }

    public virtual User? Buyer { get; set; }
}
