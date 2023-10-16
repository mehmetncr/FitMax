using FitMax.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.ViewModels
{
    public class DeliveryViewModel
    {
        public int Id { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public string CustomerFullName { get; set; }
        public bool Status { get; set; }

        public Product Product { get; set; }
    }
}
