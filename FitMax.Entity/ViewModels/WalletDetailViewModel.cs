using FitMax.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.ViewModels
{
    public class WalletDetailViewModel
    {
        public int Id { get; set; }
        public DateTime ActivityDate { get; set; }
        public string ActivityType { get; set; }
        public decimal Amount { get; set; }
        public int WalletId { get; set; }


        public virtual Wallet Wallet { get; set; }
    }
}
