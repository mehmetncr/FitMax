using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.ViewModels
{
    public class EditRoleViewModel
    {
        public string roleId { get; set; }
        public string roleName { get; set; }
        public string[] UserIdsToDelete { get; set; }  //çıkarılacaklar dizisi almak için
        public string[] UserOutsToAdd { get; set; }  //eklenecekler dizisi almak için
    }
}
