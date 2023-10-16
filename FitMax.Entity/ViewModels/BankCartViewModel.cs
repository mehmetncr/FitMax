using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.ViewModels
{
    public class BankCartViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Kart İsmi Boş Bırakılamaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Kart No Boş Bırakılamaz")]

        public string CartNo { get; set; }
        [Required(ErrorMessage = "Kartın Son Kullanım Tarihi Boş Bırakılamaz")]

        public string Month { get; set; }
        [Required(ErrorMessage = "Kartın Son Kullanım Tarihi Boş Bırakılamaz")]

        public string Year { get; set; }
        [Required(ErrorMessage = "Kartın CVV'si Boş Bırakılamaz")]

        public string CVV { get; set; }
        [Required(ErrorMessage = "Miktar Boş Bırakılamaz")]
        public decimal Amount { get; set; }
    }
}
