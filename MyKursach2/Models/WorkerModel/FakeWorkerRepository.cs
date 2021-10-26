using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public class FakeWorkerRepository : IWorkerRepository
    {        
        public IQueryable<Worker> Workers => new List<Worker>
        {
            new Worker { first_name = "тестовая должность Максим" },
            new Worker { first_name = "тестовая должность Денис" },
            new Worker { first_name = "тестовая должность Никита" }
        }.AsQueryable();
    }
}
