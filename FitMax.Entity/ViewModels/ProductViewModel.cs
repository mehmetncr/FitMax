using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ürün İsmi Boş Bırakılamaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Ürün Türü Boş Bırakılamaz")]
        public string ProductType { get; set; }
        [Required(ErrorMessage = "Ürün Açıklaması Boş Bırakılamaz")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Alış Fiyatı Boş Bırakılamaz")]
        public decimal Purchaseprice { get; set; }
        [Required(ErrorMessage = "Satış İsmi Boş Bırakılamaz")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Ürün Durumu Boş Bırakılamaz")]
        public bool Status { get; set; }
        [Required(ErrorMessage = "Ürün Stoğu Boş Bırakılamaz")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Ürün Resmi Boş Bırakılamaz")]
        public string ImgUrl { get; set; }
    }
}
