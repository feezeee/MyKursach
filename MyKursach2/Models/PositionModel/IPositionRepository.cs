using System.Linq;

namespace MyKursach2.Models
{
    public interface IPositionRepository
    {
        IQueryable<Position> Positions { get; }
    }
}
