using System.Collections.Generic;
using System.Linq;

namespace MyKursach2.Models
{
    public class FakePositionRepository : IPositionRepository
    {        
        public IQueryable<Position> Positions => new List<Position>
        {
            new Position { position_name = "тестовая должность Директор" },
            new Position { position_name = "тестовая должность Кассир" },
            new Position { position_name = "тестовая должность Администратор" }
        }.AsQueryable();
    }
}
