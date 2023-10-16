using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.Entities
{
    public class PrivateLesson
    {
        public int Id { get; set; }
        public decimal TrainerPrice  { get; set; }
        public int TrainerId { get; set; }
        public string TrainerName  { get; set; }
        public DateTime  Date  { get; set; }
        public bool Status  { get; set; }
        public string Lesson  { get; set; }
        public string? Description  { get; set; }


     


    }
}
