using System.Collections.Generic;
using System.Linq;

namespace MyKursach2.Models
{
    public class FakePositionRepository : IPositionRepository
    {        
        public IQueryable<Position> Positions => new List<Position>
        {
            new Position { PositionName = "тестовая должность Директор" },
            new Position { PositionName = "тестовая должность Кассир" },
            new Position { PositionName = "тестовая должность Администратор" }
        }.AsQueryable();
    }
}
