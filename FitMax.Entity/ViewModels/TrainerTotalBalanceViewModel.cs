using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.ViewModels
{
    public class TrainerTotalBalanceViewModel
    {
        public string TrainerName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
