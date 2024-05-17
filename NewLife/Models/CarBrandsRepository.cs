using NewLife.Utility;

namespace NewLife.Models
{
    public class CarBrandsRepository : Repository<CarBrands>, ICarBrandsRepository
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        public CarBrandsRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Save()
        {
            _uygulamaDbContext.SaveChanges();
        }

        public void Update(CarBrands carBrand)
        {
            _uygulamaDbContext.Update(carBrand);
        }
    }
}
