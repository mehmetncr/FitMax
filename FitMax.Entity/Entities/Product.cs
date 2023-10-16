using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.Entities
{
    public  class Product
    {
        public int Id { get; set; }
        public string Name  { get; set; }
        public string ProductType  { get; set; }
        public string Description { get; set; }
        public decimal Purchaseprice { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public int Stock { get; set; }
        public string ImgUrl { get; set; }


    }
}
