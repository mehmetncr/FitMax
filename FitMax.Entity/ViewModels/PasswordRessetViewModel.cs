using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.ViewModels
{
	public class PasswordRessetViewModel 
	{
        public int Id { get; set; }
        [Required(ErrorMessage = "Email Boş Bırakılamaz")]
		public string EMail { get; set; }
		[Required(ErrorMessage = "Kullanıcı Adı Boş Bırakılamaz")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Şifre Boş Bırakılamaz")]
		[Display(Name = "Şifre")]  //ekranda asp-for olan label da yazacak olan isim
		[DataType(DataType.Password)] //girilen karakterleri göstermemesi için
		public string Password { get; set; }
		[Required(ErrorMessage = "Şifre Onay Boş Bırakılamaz")]
		[Compare("Password", ErrorMessage = "Şifreler Uyuşmuyor")]   // password ile karşılaştır
		[Display(Name = "Şifre Tekrar")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
		[Required(ErrorMessage = "Mevcut Şifre Boş Bırakılamaz")]
		[Display(Name = "Mevcut Şifre")]
		public string OldPassword { get; set; }
		public string Token { get; set; }
	}
}
