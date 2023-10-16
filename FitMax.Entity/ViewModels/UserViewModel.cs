using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public string? ImgUrl { get; set; }
        public string? Height { get; set; }
        public string? Description { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Instagram { get; set; }
        public string? Youtube { get; set; }
        public string? Weight { get; set; }
        public bool Status { get; set; }
        public string? Package { get; set; }
        public string UserType { get; set; }
        public decimal TrainerPrice { get; set; }
        public bool IsDeleted { get; set; }
        public bool DataConfirm { get; set; }
        public DateTime? PackageStartDate { get; set; }
        public DateTime? PackageEndDate { get; set; }
        public string UserName { get; set; }

    }
}
