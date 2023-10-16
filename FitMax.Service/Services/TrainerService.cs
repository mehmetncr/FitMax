using AutoMapper;
using FitMax.DataAccess.Identity;
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
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrainerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<HomeTrainerViewModel>> GetAllTrainer()
        {
            var trainers = await _unitOfWork.GetRepository<AppUser>().GetAll(x => x.UserType == "Eğitmen");
            List<HomeTrainerViewModel> model = new List<HomeTrainerViewModel>();

            foreach (var item in trainers)
            {
                if (item.ImgUrl != null)
                {



                    HomeTrainerViewModel trainer = new HomeTrainerViewModel()
                    {
                        Id = item.Id,
                        Name = item.FirstName + " " + item.Lastname,
                        Description = item.Description,
                        Facebook = item.Facebook,
                        Twitter = item.Twitter,
                        Youtube = item.Youtube,
                        Instagram = item.Instagram,
                        ImgUrl = item.ImgUrl

                    };
                    model.Add(trainer);
                }
            }

            return model;
        }

        public async Task<List<HomeTrainerViewModel>> GetRndmTrainer()
        {
            var trainers = await _unitOfWork.GetRepository<AppUser>().GetAll(x => x.UserType == "Eğitmen");
            List<HomeTrainerViewModel> model = new List<HomeTrainerViewModel>();
            if (trainers.Count() <= 4)
            {
                foreach (var item in trainers)
                {
                    if (item.ImgUrl != null)
                    {

                        HomeTrainerViewModel trainer = new HomeTrainerViewModel()
                        {
                            Id = item.Id,
                            Name = item.FirstName + " " + item.Lastname,
                            Description = item.Description,
                            Facebook = item.Facebook,
                            Twitter = item.Twitter,
                            Youtube = item.Youtube,
                            Instagram = item.Instagram,
                            ImgUrl = item.ImgUrl

                        };
                        model.Add(trainer);
                    }
                }
            }
            else
            {
                Random random = new Random();
                var randomTrainers = trainers.OrderBy(x => random.Next()).Take(4).ToList();
                foreach (var item in randomTrainers)
                {
                    if (item.ImgUrl != null)
                    {
                        HomeTrainerViewModel trainer = new HomeTrainerViewModel()
                        {
                            Id = item.Id,
                            Name = item.FirstName + " " + item.Lastname,
                            Description = item.Description,
                            ImgUrl = item.ImgUrl,
                            Facebook = item.Facebook,
                            Twitter = item.Twitter,
                            Youtube = item.Youtube,
                            Instagram = item.Instagram,
                        };
                        model.Add(trainer);
                    }
                }
            }
            return model;


        }
    }
}
