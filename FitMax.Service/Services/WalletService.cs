using AutoMapper;
using FitMax.Entity.Entities;
using FitMax.Entity.Interfaces;
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
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WalletService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



		public async Task GetCreateWallet(WalletViewModel model)
        {
            Wallet walletmodel = _mapper.Map<Wallet>(model);   //UI dan gelen viewmodel entity'e çevrilir ve katıy edilmek üzere gönderilir
            await _unitOfWork.GetRepository<Wallet>().Add(walletmodel);
            _unitOfWork.Commit();
        }

        public async Task<WalletViewModel> GetUserWallet(int id)
        {
            var wallet = await _unitOfWork.GetRepository<Wallet>().GetByIdAsync(x => x.UserId == id);  //User Id ye göre cüzdan bulunur ve viewmodele mapplenerek döndürülür
            return _mapper.Map<WalletViewModel>(wallet);
        }
		public async Task UpdateBalancerWallet(int walletId, decimal amount,string ActiviyType)  //cüzdan hareketi yapıldıktan sonra bu cüzdan bakiyesini güncellemesi için çağrılır
		{

			var wallet = await _unitOfWork.GetRepository<Wallet>().GetByIdAsync(x => x.Id == walletId);  //cüzdan bulunur
            if (ActiviyType=="Yatırma")  //işlem tipine göre bakiye güncellenir
            {
                wallet.Balance += amount; 

            }
            else
            {
				wallet.Balance-=amount;

			}
           
			  _unitOfWork.GetRepository<Wallet>().Update(wallet); //cüzdan güncellenir
            _unitOfWork.Commit(); //detaylarla birlikte veritabanı kaydı yapılır
			
		}
	}
}
