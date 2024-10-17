using System;
using System.Collections.Generic;

namespace AuctionKoi.Repositories.Entities;

public partial class Bid
{
    public int BidId { get; set; }

    public int? AuctionId { get; set; }

    public int? UserId { get; set; }

    public decimal? BidAmount { get; set; }

    public DateTime? BidTime { get; set; }

    public virtual Auction? Auction { get; set; }

    public virtual User? User { get; set; }
}
