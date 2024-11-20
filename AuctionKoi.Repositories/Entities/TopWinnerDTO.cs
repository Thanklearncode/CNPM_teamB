using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionKoi.Repositories.Entities
{
    public class TopWinnerDTO
    {
        public int AuctionID { get; set; }
        public int WinnerID { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
