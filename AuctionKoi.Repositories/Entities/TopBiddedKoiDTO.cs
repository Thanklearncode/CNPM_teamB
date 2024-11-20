using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionKoi.Repositories.Entities
{
    public class TopBiddedKoiDTO
    {
        public int AuctionID { get; set; }
        public int BidCount { get; set; }
    }

}
