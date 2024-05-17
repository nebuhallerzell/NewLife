namespace NewLife.Models
{
    public interface ICarBrandsRepository : IRepository<CarBrands>
    {
        void Update(CarBrands carBrand);
        void Save();
    }
}
