using AutoMapper;
using FitMax.Entity.Entities;
using FitMax.Entity.IService;
using FitMax.Entity.UnitOfWorks;
using FitMax.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllTrue()
        {
            var list = await _unitOfWork.GetRepository<Product>().GetAll(x=>x.Status==true);
            return _mapper.Map<List<ProductViewModel>>(list);
        }
        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            var list = await _unitOfWork.GetRepository<Product>().GetAll();
            return _mapper.Map<List<ProductViewModel>>(list);
        }

        public ProductViewModel GetById(int id)
        {
            var product = _unitOfWork.GetRepository<Product>().GetById(id);
           
            return _mapper.Map<ProductViewModel>(product);
        }
        public async Task<IEnumerable<ProductViewModel>> GetAllByCategory(string filter)
        {
            IEnumerable<Product> list = new List<Product>();
            if (filter == "Spor" || filter == "Besin")
            {
                list = await _unitOfWork.GetRepository<Product>().GetAll(x => x.ProductType == filter);
                return _mapper.Map<List<ProductViewModel>>(list);
            }
            else
            {
                list = await _unitOfWork.GetRepository<Product>().GetAll();

            }
            return _mapper.Map<List<ProductViewModel>>(list);

        }

        public async Task<IEnumerable<ProductViewModel>> Search(string seach)
        {
            IEnumerable<Product> list = new List<Product>();
            if (seach != "Tüm")
            {
                list = await _unitOfWork.GetRepository<Product>().GetAll(x => x.Name.ToLower().Contains(seach.ToLower()));
            }
            else
            {
                list = await _unitOfWork.GetRepository<Product>().GetAll();
            }

            return _mapper.Map<List<ProductViewModel>>(list);
        }

        public async Task UpdateProduct(ProductViewModel model)
        {
            var product = _mapper.Map<Product>(model);
            if (product != null)
                _unitOfWork.GetRepository<Product>().Update(product);
            _unitOfWork.Commit();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(id);

            if (product != null)
            {
                _unitOfWork.GetRepository<Product>().Delete(product);
                _unitOfWork.Commit();
            }
        }

        public async Task AddProduct(ProductViewModel model)
        {
            var products = _mapper.Map<Product>(model);
            await _unitOfWork.GetRepository<Product>().Add(products);
            _unitOfWork.Commit();
        }

        public async Task UpdateStock(List<CartLineViewModel> model)
        {
            foreach (var item in model)
            {
               Product product= await _unitOfWork.GetRepository<Product>().GetByIdAsync(x => x.Id == item.ProductId);
                product.Stock -= item.Quantity;
                if (product.Stock<1)
                {
                    product.Status = false;
                }
                _unitOfWork.GetRepository<Product>().Update(product);
            }
            _unitOfWork.Commit();
        }
        public async Task<IEnumerable<ProductViewModel>> GetAllStocks()
        {
            IEnumerable<Product> products = await _unitOfWork.GetRepository<Product>().GetAll(x => x.Stock != null);

            return _mapper.Map<IEnumerable<ProductViewModel>>(products);

        }
    }
}




