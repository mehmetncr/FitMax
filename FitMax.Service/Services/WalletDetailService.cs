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
	public class WalletDetailService : IWalletDetailService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWalletService _service;
		private readonly IMapper _mapper;

        public WalletDetailService(IUnitOfWork unitOfWork, IMapper mapper, IWalletService service)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _service = service;
        }

        public async Task AddDetailToWallet(WalletDetailViewModel model)
		{
			
			await _unitOfWork.GetRepository<WalletDetail>().Add(_mapper.Map<WalletDetail>(model));  //gelen cüzdan hareketi mapp adilerek kayıt edilir commit işlemi cüzdan bakiyesi güncellenince olur
            await _service.UpdateBalancerWallet(model.WalletId, model.Amount, model.ActivityType);   //cüzdan güncellenir

        }
        public async Task<IEnumerable<WalletDetailViewModel>> GetDetailsToWallet(int id)
        {

			var details = await _unitOfWork.GetRepository<WalletDetail>().GetAll(x => x.WalletId == id);
            return _mapper.Map<IEnumerable<WalletDetailViewModel>>(details.Reverse());

        }
    }
}
