using NewLife.Utility;

namespace NewLife.Models
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        public CarRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Save()
        {
            _uygulamaDbContext.SaveChanges();
        }

        public void Update(Car car)
        {
            _uygulamaDbContext.Update(car);
        }
    }
}
