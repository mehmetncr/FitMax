    using AutoMapper;
using FitMax.DataAccess.Identity;
using FitMax.Entity.Entities;
using FitMax.Entity.IService;
using FitMax.Entity.UnitOfWorks;
using FitMax.Entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Service.Services
{
    public class PrivateLessonService : IPrivateLessonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAccountService _accountService;

        public PrivateLessonService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _accountService = accountService;
        }

        //verilen TrainerId ve tarih bilgisindeki ders kaydı döndürülür
        public async Task<PrivateLessonViewModel> GetLessonByTrainer(int id, DateTime date)
        {                                                                                  //.Date tarihi saat bazıda eişitler ve gün ay yıl eşleşmesi yapılır
            PrivateLesson lesson = await _unitOfWork.GetRepository<PrivateLesson>().GetByIdAsync(x => x.TrainerId == id && x.Date.Date == date.Date);
 

            if (lesson != null)
            {
                return _mapper.Map<PrivateLessonViewModel>(lesson);
            }
            PrivateLessonViewModel model = new PrivateLessonViewModel();
            return model;
        }
        // verilen TrainerId ye ait tüm dersler döndürülür
        public async Task<IEnumerable<PrivateLessonViewModel>> GetAllLessonsByTrainer(int id)
        {
            IEnumerable<PrivateLesson> lesson = await _unitOfWork.GetRepository<PrivateLesson>().GetAll(x => x.TrainerId == id && x.Date > DateTime.Now);  //eğitmenin ders satırları alınır
            if (lesson == null)
            {

                return null;
            }


            return _mapper.Map<IEnumerable<PrivateLessonViewModel>>(lesson);
        }

        //bir kullanıcı özel ders aldığında TrainerId UserId ve Tarih bilgisi gelir o satırdaki lesson kolonuna userId eklenir
        public async Task UpdateLessonByTrainer(PaymentViewModel model)
        {
            PrivateLesson lesson = await _unitOfWork.GetRepository<PrivateLesson>().GetByIdAsync(x => x.TrainerId == model.TrainerId && x.Date.Date == model.Day.Date);
            lesson.Lesson = model.UserId.ToString();
            _unitOfWork.GetRepository<PrivateLesson>().Update(lesson);
            _unitOfWork.Commit();

        }
        //Admin tarafından bir ders iptal edilebilir yada tekrar açılabilir kullanıcı ders iptali yapmak isterse buradan admin tarfından gerçekleştirilir
        public async Task AdminUpdateLessonByTrainer(int id, DateTime date)
        {
            PrivateLesson lesson = await _unitOfWork.GetRepository<PrivateLesson>().GetByIdAsync(x => x.TrainerId == id && x.Date.Date == date.Date);
            if (lesson != null)
            {

                if (lesson.Lesson == "empty")  
                {
                    lesson.Lesson = "cancel";
                }
                else if (lesson.Lesson == "cancel")
                {
                    lesson.Lesson = "empty";
                }
                else
                {
                    lesson.Lesson = "empty";
                }
                _unitOfWork.GetRepository<PrivateLesson>().Update(lesson);
                _unitOfWork.Commit();
            }
        }


        //admin tarafından eğitmenin tüm dersleri çekilir ve UserId yerine isimler eklenir
        public async Task<List<PrivateLessonViewModel>> GetAllLessonsByTrainerWithUsers(int id)
        {
            IEnumerable<PrivateLesson> lesson = await _unitOfWork.GetRepository<PrivateLesson>().GetAll(x => x.TrainerId == id);
            foreach (var item in lesson)
            {
                if (item.Lesson != "empty" && item.Lesson != "cancel")
                {
                    var user = await _userManager.FindByIdAsync(item.Lesson);
                    item.Lesson = user.FirstName + " " + user.Lastname;
                }
                else if (item.Lesson == "empty")
                {
                    item.Lesson = "BOŞ";
                }
                else
                {
                    item.Lesson = "İPTAL";
                }

            }
            return _mapper.Map<List<PrivateLessonViewModel>>(lesson);
        }
        //admin tarafından eğitmenin yıl ve ay bazında ne kadar özel ders parası kazandığı belirlenir
        public async Task<List<TrainerTotalBalanceViewModel>> TotalBalance(int id)
        {

            IEnumerable<PrivateLesson> lessons = await _unitOfWork.GetRepository<PrivateLesson>().GetAll(x => x.TrainerId == id); //tüm kayıtlar çekilir

            var monthlyTotals = lessons
                .Where(x => x.Lesson != "empty" && x.Lesson != "cancel")   //iptal yada boş olan derslers seçilmez
                .GroupBy(x => new { x.Date.Year, x.Date.Month }) // Yıl ve ay bazında grupla
                .Select(group => new  //tarihe göre gruplanır
                {
                    Year = group.Key.Year,  //yıla göre
                    Month = group.Key.Month,  //aya göre
                    TotalPrice = group.Sum(x => x.TrainerPrice)  //grubun toplam tutarı
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToList();

            //veriler view modele aktarılır
            var TrainerTotalBalanceViewModel = monthlyTotals.Select(total => new TrainerTotalBalanceViewModel
            {
                TrainerName = lessons.FirstOrDefault().TrainerName,
                Year = total.Year,
                Month = total.Month,
                TotalPrice = total.TotalPrice
            }).ToList();


            return TrainerTotalBalanceViewModel;
        }
        //admin tarafondan takvim güncellemesi yapıldığında her eğitmen için bugünden itibaren 14 gün dersi olacak şekilde tablolaro doldurulur 
        public async Task NewWeek()
        {

            var list = _userManager.Users.Where(x => x.UserType == "Eğitmen").ToList();  //eğitmen listesi alınır

            foreach (var item in list)  //her eğitmen için
            {
                IEnumerable<PrivateLesson> lessons = await _unitOfWork.GetRepository<PrivateLesson>().GetAll(x => x.TrainerId == item.Id, orderby: x => x.OrderByDescending(x => x.Date));  //eğitmenin kayıtlı dersleri ders tarihleri tersten olacak şekilde sıralanarak alınır

                if (lessons.Count() == 0)  //eğer egitmenin hiç ders kaydı yoksa 14 günlük kayıt açılır
                {
                    for (int i = 1; i <= 14; i++)
                    {
                        PrivateLesson model = new PrivateLesson()
                        {
                            TrainerPrice = item.TrainerPrice,
                            TrainerId = item.Id,
                            TrainerName = item.FirstName + " " + item.Lastname,
                            Date = DateTime.Now.AddDays(i),
                            Status = true,
                            Lesson = "empty"
                        };

                        await _unitOfWork.GetRepository<PrivateLesson>().Add(model);
                    }
                }
                else//eğer eğitmenin en az bir kaydı varsa
                {

                    PrivateLesson lesson = lessons.FirstOrDefault();  //son kayıtlı dersi bulunur
                    int addDays = 0;
                    if (lesson.Date < DateTime.Now.AddDays(14))  //eğer son kayıtlı ders bugünden itibaren 14 günden önce ise if e girer
                    {
                        addDays = (int)(DateTime.Now.AddDays(14) - lesson.Date).TotalDays;  //tabloyu 14 güne tamamlamak için kaç gün grektiği bulunur
                    }
                    for (int i = 1; i <= addDays; i++) //eksik gün kadar tabloya yeni ders eklenir
                    {
                        PrivateLesson model = new PrivateLesson()
                        {
                            TrainerPrice = item.TrainerPrice,
                            TrainerId = item.Id,
                            TrainerName = item.FirstName + " " + item.Lastname,
                            Date = lesson.Date.AddDays(i),
                            Status = true,
                            Lesson = "empty"
                        };

                        await _unitOfWork.GetRepository<PrivateLesson>().Add(model);
                    }

                }
                IEnumerable<PrivateLesson> Endlessons = await _unitOfWork.GetRepository<PrivateLesson>().GetAll(x => x.TrainerId == item.Id && x.Date.Date <= DateTime.Now.Date);
                foreach (var items in Endlessons)
                {
                    items.Status = false;
                    _unitOfWork.GetRepository<PrivateLesson>().Update(items);
                }
               
            }
            _unitOfWork.Commit();  //veri tabanına işlenir

        }

        public async Task<UserLessonListViewModel> GetUserLessons(int id)
        {
          IEnumerable<PrivateLesson> userLessons=await _unitOfWork.GetRepository<PrivateLesson>().GetAll(x=> x.Lesson==id.ToString()&& x.Date.Date >= DateTime.Now.Date);
            UserLessonListViewModel model = new UserLessonListViewModel();
            model.IsTrue =_mapper.Map<IEnumerable<PrivateLessonViewModel>> (userLessons.Where(x => x.Status == true));
            model.IsFalse = _mapper.Map<IEnumerable<PrivateLessonViewModel>>(userLessons.Where(x => x.Status == false));
            return model;
        }

        public async Task<UserLessonListViewModel> GetTrainerLessons(int id)
        {
            IEnumerable<PrivateLesson> trainerLessons = await _unitOfWork.GetRepository<PrivateLesson>().GetAll(x => x.TrainerId == id);
            UserLessonListViewModel model = new UserLessonListViewModel(); 
            foreach (var lesson in trainerLessons)
            {
                if (lesson.Lesson != "empty" && lesson.Lesson != "cancel")
                {
                    AppUser user = await _userManager.FindByIdAsync(lesson.Lesson);
                    lesson.Lesson = user.FirstName + " " + user.Lastname;
                }
            }
            model.IsTrue = _mapper.Map<IEnumerable<PrivateLessonViewModel>>(trainerLessons.Where(x => x.Status == true));


            model.IsFalse = _mapper.Map<IEnumerable<PrivateLessonViewModel>>(trainerLessons.Where(x => x.Status == false));
            return model;
        }
        public async Task<IEnumerable<PrivateLessonViewModel>> GetAllPrivateLesson()
        {
            IEnumerable<PrivateLesson> pl = await _unitOfWork.GetRepository<PrivateLesson>().GetAll();
            return _mapper.Map<IEnumerable<PrivateLessonViewModel>>(pl);
        }
        public void UpdateLessonDetails(PrivateLessonViewModel model)
        {
            var lesson = _unitOfWork.GetRepository<PrivateLesson>().GetById(model.Id);
            lesson.Description = model.Description;
            _unitOfWork.GetRepository<PrivateLesson>().Update(lesson);
            _unitOfWork.Commit();
        }
        public async Task<PrivateLessonViewModel> GetLessonDetails(int id)
        {
            var lesson = await _unitOfWork.GetRepository<PrivateLesson>().GetByIdAsync(id);
            UserViewModel user = _accountService.GetUserById(Convert.ToInt32(lesson.Lesson));
            lesson.Lesson = user.FirstName + " " + user.Lastname;
            return _mapper.Map<PrivateLessonViewModel>(lesson);
        }

    }
}