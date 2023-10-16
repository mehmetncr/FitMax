using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.ViewModels
{
    public class UserLessonListViewModel
    {
        public IEnumerable<PrivateLessonViewModel> IsTrue { get; set; }
        public IEnumerable<PrivateLessonViewModel> IsFalse { get; set; }
       
    }
}
