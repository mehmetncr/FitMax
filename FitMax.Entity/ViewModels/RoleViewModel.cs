using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "İsim Alanı Boş Bırakılamaz")]
        public string Name { get; set; }
    }
}
