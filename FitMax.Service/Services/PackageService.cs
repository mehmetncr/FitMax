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
    public class PackageService : IPackageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PackageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        //tüm paketleri getirme
        public async Task<IEnumerable<PackageViewModel>> GetPackages()
        {
          return (_mapper.Map<IEnumerable<PackageViewModel>> (await  _unitOfWork.GetRepository<Package>().GetAll()));
        }
        //tek bir paket getirme
        public PackageViewModel GetPackagesById(int id)
        {
            return (_mapper.Map<PackageViewModel>(_unitOfWork.GetRepository<Package>().GetById(id)));
        }


        // paketleri güncelleme
        public  void UpdatePackage(PackageViewModel model)
        {            
           
            _unitOfWork.GetRepository<Package>().Update(_mapper.Map<Package>(model));
             _unitOfWork.Commit();
        }
        //yeni paket ekleme
        public void AddPackage(PackageViewModel model)
        {
            model.Name = model.Name + " " + "Ay";
            _unitOfWork.GetRepository<Package>().Add(_mapper.Map<Package>(model));
            _unitOfWork.Commit();
        }
        //paketi silme
        public void DeletePackage(int id)
        {

            _unitOfWork.GetRepository<Package>().Delete(id);
            _unitOfWork.Commit();
        }
        //paketleri getirme
        public async Task<PackageViewModel> GetByPackage(int id)
        {
            var package = await _unitOfWork.GetRepository<Package>().GetByIdAsync(id);
            if (package==null)
            {
                PackageViewModel model = new PackageViewModel();
                return _mapper.Map<PackageViewModel>(model);
            }

            return _mapper.Map<PackageViewModel>(package);
        }
    }
}
