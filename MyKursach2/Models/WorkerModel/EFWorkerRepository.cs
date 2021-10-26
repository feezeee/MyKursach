using MyKursach2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public class EFWorkerRepository : IWorkerRepository
    {
        private ApplicationDbContext context;

        public EFWorkerRepository (ApplicationDbContext context)
        {
            this.context = context;
        }
        public IQueryable<Worker> Workers => context.Workers.Join(context.Positions,
            p => p.Position,
            t => t,
            (p, t) => new Worker 
            { 
                Id= p.Id, 
                FirstName = p.FirstName, 
                LastName = p.LastName, 
                DateOfBirth = p.DateOfBirth, 
                Email = p.Email, 
                Position = t, 
                PositionId = p.PositionId 
            });

    }
}
