using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı Alanı Boş Bırakılamaz")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Sifre Boş Bırakılamaz")]
        [Display(Name = "Şifre")]  //ekranda asp-for olan label da yazacak olan isim
        [DataType(DataType.Password)] //girilen karakterleri göstermemesi için
        public string Password { get; set; }
        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }

    }
}
