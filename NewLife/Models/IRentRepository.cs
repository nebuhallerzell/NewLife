using NewLife.Models;

namespace NewLife.Repositories
{
    public interface IRentRepository : IRepository<Rent>
    {
       object GetAll(int value,string includeProperties);
        string? GetById(int value, string includeProperties);
        object GetAllRents();

        void Save();
        void Update(Rent rent);
        //object ArabaCek();
        //object KullaniciCek();

        
    }
}
