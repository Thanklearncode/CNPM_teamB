using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AuctionKoi.Repositories.Entities;

[Table("KoiFish")]
public partial class KoiFish
{
    [Key]
    public int KoiID { get; set; }

    [StringLength(100)]
    public string KoiName { get; set; } = null!;

    public int? Age { get; set; }

    [StringLength(100)]
    public string? Breed { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [StringLength(255)]
    public string? ImageUrl { get; set; }

    public int? OwnerID { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Length { get; set; }

    [StringLength(10)]
    public string? KoiSex { get; set; }

    [InverseProperty("Koi")]
    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();

    [ForeignKey("OwnerID")]
    [InverseProperty("KoiFishes")]
    public virtual User? Owner { get; set; }
}
