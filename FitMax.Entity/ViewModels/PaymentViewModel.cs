using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.ViewModels
{
    public class PaymentViewModel
    {
        public decimal Balance { get; set; }

        public int WalletId { get; set; }
        public int UserId { get; set; }
        public int WeekNo { get; set; }
        public string Name { get; set; }
        public DateTime Day { get; set; }
        public int PackageId { get; set; }
        public int TrainerId { get; set; }
        public int ProductId { get; set; }
        public string Type { get; set; }

        public decimal Price { get; set; }

    }
}
