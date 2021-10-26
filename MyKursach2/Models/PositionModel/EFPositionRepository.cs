using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public class EFPositionRepository : IPositionRepository
    {
        private PostalOfficeContext context;

        public EFPositionRepository (PostalOfficeContext context)
        {
            this.context = context;
        }
        public IQueryable<Position> Positions => context.Positions;

    }
}
