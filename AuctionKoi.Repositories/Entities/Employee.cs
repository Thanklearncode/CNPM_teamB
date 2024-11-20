using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AuctionKoi.Repositories.Entities;

public partial class Employee
{
    [Key]
    public int EmployeeID { get; set; }

    public int? UserID { get; set; }

    [StringLength(50)]
    public string? Position { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? HireDate { get; set; }

    public int? RoleID { get; set; }

    [ForeignKey("UserID")]
    [InverseProperty("Employees")]
    public virtual User? User { get; set; }
}
