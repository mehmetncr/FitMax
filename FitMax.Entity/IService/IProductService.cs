using FitMax.Entity.Entities;
using FitMax.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllTrue();
        Task<IEnumerable<ProductViewModel>> GetAll();
        ProductViewModel GetById(int id);
        Task<IEnumerable<ProductViewModel>> GetAllByCategory(string filter);
        Task<IEnumerable<ProductViewModel>> Search(string seach);
        Task UpdateProduct(ProductViewModel model);
        Task DeleteProduct(int id);
        Task AddProduct(ProductViewModel model);
        Task UpdateStock(List<CartLineViewModel> model);
        Task<IEnumerable<ProductViewModel>> GetAllStocks();






    }
}
