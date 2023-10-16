using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.ViewModels
{
    public class DeliveryListViewModel
    {
        public IEnumerable<CartViewModel> IsTrue { get; set; }
        public IEnumerable<CartViewModel> IsFalse { get; set; }
    }
}
