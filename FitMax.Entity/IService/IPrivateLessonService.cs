using FitMax.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.IService
{
    public interface IPrivateLessonService
    {
        Task<PrivateLessonViewModel> GetLessonByTrainer(int id,DateTime date);
        Task UpdateLessonByTrainer(PaymentViewModel model);
        Task AdminUpdateLessonByTrainer(int id, DateTime date);
        Task<List<PrivateLessonViewModel>> GetAllLessonsByTrainerWithUsers(int id);
        Task<IEnumerable<PrivateLessonViewModel>> GetAllLessonsByTrainer(int id);
        Task<List<TrainerTotalBalanceViewModel>> TotalBalance(int id);
        Task NewWeek();
        Task <UserLessonListViewModel> GetUserLessons(int id);
        Task<IEnumerable<PrivateLessonViewModel>> GetAllPrivateLesson();
        Task<UserLessonListViewModel> GetTrainerLessons(int id);
        Task<PrivateLessonViewModel> GetLessonDetails(int id);
        void UpdateLessonDetails(PrivateLessonViewModel model);


    }
}
