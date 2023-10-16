using AutoMapper;
using FitMax.Entity.Entities;
using FitMax.Entity.IService;
using FitMax.Entity.UnitOfWorks;
using FitMax.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Service.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        //admine tüm mailleri çekme
        public async Task<IEnumerable<ContactViewModel>> GetAllMail()  
        {
           return _mapper.Map<IEnumerable<ContactViewModel>>(await _unitOfWork.GetRepository<Contact>().GetAll());
        }
        //admin mail detayları görme
        public async Task<ContactViewModel> GEtMailById(int id)
        {
            Contact model = await _unitOfWork.GetRepository<Contact>().GetByIdAsync(id);
            model.IsReaded= true;
            _unitOfWork.GetRepository<Contact>().Update(model);
            _unitOfWork.Commit();
            return _mapper.Map<ContactViewModel>(model);
        }
        //kullanıcıdan mail alma
        public async Task SendMail(ContactViewModel model)
        {
            model.Date=DateTime.Now;
            await _unitOfWork.GetRepository<Contact>().Add(_mapper.Map<Contact>(model));
            _unitOfWork.Commit();

        }
        //mail okundu bilgisi güncelleme
        public void UpdateMail(ContactViewModel model)
        {
            model.IsReaded = true;
           _unitOfWork.GetRepository<Contact>().Update(_mapper.Map<Contact>(model));
            _unitOfWork.Commit();

        }
    }
}
