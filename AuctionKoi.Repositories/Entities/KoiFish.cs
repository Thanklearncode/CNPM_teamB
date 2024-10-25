using System;
using System.Collections.Generic;

namespace AuctionKoi.Repositories.Entities;

public partial class KoiFish
{
    public int KoiId { get; set; }

    public string KoiName { get; set; } = null!;

    public int? Age { get; set; }

    public string? Breed { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public int? OwnerId { get; set; }

    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();

    public virtual User? Owner { get; set; }
}
