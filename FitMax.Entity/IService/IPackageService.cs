using FitMax.Entity.Entities;
using FitMax.Entity.Interfaces;
using FitMax.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.IService
{
    public interface IPackageService
    {
        Task<IEnumerable<PackageViewModel>> GetPackages();
        PackageViewModel GetPackagesById(int id);
        void UpdatePackage(PackageViewModel model);
        void AddPackage(PackageViewModel model);
        void DeletePackage(int id);
        Task<PackageViewModel> GetByPackage(int Id);
    }
}
