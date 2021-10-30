﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public interface IWorkerRepository
    {
        IQueryable<Worker> Workers { get; }
        void Add(Worker worker);
        void SaveChanges();
    }
}
