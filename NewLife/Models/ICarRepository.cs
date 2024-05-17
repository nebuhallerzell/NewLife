namespace NewLife.Models
{
    public interface ICarRepository : IRepository<Car>
    {
        void Update(Car car);
        void Save();
    }
}
