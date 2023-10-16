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
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accountService = accountService;
        }
        //yeni sepet oluşturma
        public async Task<int> CreateCart(int id, decimal price) 
        {
            Cart model = new Cart()
            {
                UserId = id,
                TotalPrice = price,
                OrderDate = DateTime.Now
            };
            _unitOfWork.GetRepository<Cart>().AddNoAsync(model);
            _unitOfWork.Commit();
            return model.Id;

        }
        //tüm sepeti alma
        public async Task<DeliveryListViewModel> GetAllCartAsync()
        {
            DeliveryListViewModel listModel = new DeliveryListViewModel();
            IEnumerable<Cart> deliveries = await _unitOfWork.GetRepository<Cart>().GetAll();

            IEnumerable<Cart> isFalse = deliveries.Where(x => x.Status == false);
       
            IEnumerable<CartViewModel> falsemodel = _mapper.Map<IEnumerable<CartViewModel>>(isFalse);
            foreach (var item in falsemodel)
            {
                var user = _accountService.GetUserById(item.UserId);
                item.CustomerName = user.FirstName + " " + user.Lastname;
                item.CustomerEmail = user.Email;
            }
            listModel.IsFalse = falsemodel.Reverse();

            IEnumerable<Cart> isTrue = deliveries.Where(x => x.Status == true);
          
            IEnumerable<CartViewModel> tuemodel = _mapper.Map<IEnumerable<CartViewModel>>(isTrue);
            foreach (var item in tuemodel)
            {
                var user = _accountService.GetUserById(item.UserId);
                item.CustomerName = user.FirstName + " " + user.Lastname;
                item.CustomerEmail = user.Email;
            }
            listModel.IsTrue = tuemodel.Reverse();

            return listModel;

        }
        //sepet durumunu güncelleme teslimat için
        public async Task UpdateCartStatusById(int id)
        {
            Cart cart= await _unitOfWork.GetRepository<Cart>().GetByIdAsync(x=>x.Id==id);
            cart.Status = true;
            cart.DeliveryDate = DateTime.Now;
            _unitOfWork.GetRepository<Cart>().Update(cart);
            _unitOfWork.Commit();
        }
        //teslimat için sepeti alma
        public async Task<DeliveryListViewModel> GetCartById(int id)
        {
            IEnumerable<Cart> carts = await _unitOfWork.GetRepository<Cart>().GetAll(x => x.UserId==id);
            DeliveryListViewModel listModel = new DeliveryListViewModel();
           

            IEnumerable<Cart> isFalse = carts.Where(x => x.Status == false);

            IEnumerable<CartViewModel> falsemodel = _mapper.Map<IEnumerable<CartViewModel>>(isFalse);
            
            listModel.IsFalse = falsemodel.Reverse();

            IEnumerable<Cart> isTrue = carts.Where(x => x.Status == true);

            IEnumerable<CartViewModel> truemodel = _mapper.Map<IEnumerable<CartViewModel>>(isTrue);
            
            listModel.IsTrue = truemodel.Reverse();

            return listModel;

        }
    }
}
