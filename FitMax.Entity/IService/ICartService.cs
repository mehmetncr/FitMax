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
    public interface ICartService
    {
        Task<int> CreateCart(int id, decimal price);
        Task<DeliveryListViewModel> GetAllCartAsync();
        Task UpdateCartStatusById(int id);

        Task<DeliveryListViewModel> GetCartById(int id);



    }
}
