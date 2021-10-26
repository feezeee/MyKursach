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
            new Worker { FirstName = "тестовая должность Максим" },
            new Worker { FirstName = "тестовая должность Денис" },
            new Worker { FirstName = "тестовая должность Никита" }
        }.AsQueryable();
    }
}
