using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.Entities
{
    public  class Wallet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Balance { get; set; }

        public virtual List<WalletDetail> WalletDetails { get; set; }

    }
}
