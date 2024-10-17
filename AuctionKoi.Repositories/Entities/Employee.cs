using System;
using System.Collections.Generic;

namespace AuctionKoi.Repositories.Entities;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int? UserId { get; set; }

    public string? Position { get; set; }

    public DateTime? HireDate { get; set; }

    public virtual User? User { get; set; }
}
