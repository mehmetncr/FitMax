using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.ViewModels
{
    public class PackageViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Paket No Alanı Boş Bırakılamaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Açıklma Alanı Boş Bırakılamaz")]
        public string Description { get; set; }
        [Required(ErrorMessage = "AçıklmaAlanı Boş Bırakılamaz")]
        public string Description2 { get; set; }
        [Required(ErrorMessage = "Açıklma Alanı Boş Bırakılamaz")]
        public string Description3 { get; set; }
        [Required(ErrorMessage = "Fiyat Alanı Boş Bırakılamaz")]
        public decimal Price { get; set; }
    }
}
