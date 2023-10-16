using FitMax.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.IService
{
    public interface IContactService
    {
        Task SendMail(ContactViewModel model);
        Task<IEnumerable<ContactViewModel>> GetAllMail();
        void UpdateMail(ContactViewModel model);
        Task<ContactViewModel> GEtMailById(int id);
    }
}
