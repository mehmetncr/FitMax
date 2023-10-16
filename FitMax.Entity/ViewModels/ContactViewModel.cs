using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "İsim Alanı Boş Bırakılamaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email Alanı Boş Bırakılamaz")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mesaj Alanı Boş Bırakılamaz")]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool IsReaded { get; set; }
    }
}
