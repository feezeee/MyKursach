using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public interface IOperationRepository
    {
        IQueryable<Operation> Operations { get; }
    }
}
