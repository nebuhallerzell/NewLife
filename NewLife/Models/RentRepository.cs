
using Microsoft.EntityFrameworkCore;
using NewLife.Models;
using NewLife.Utility;


namespace NewLife.Repositories
{
    public class RentRepository : Repository<Rent>, IRentRepository
    {
        private readonly UygulamaDbContext _context;

        public RentRepository(UygulamaDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Rent> GetAll()
        {
            return _context.Rent.ToList();
        }

        public void Add(Rent rent)
        {
            _context.Rent.Add(rent);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public object GetAllRents()
        {
            return _context.Rent.Include(x => x.User).Include(x => x.Car).ToList();
        }
        public string GetById(int value, string includeProperties) 
        {
            throw new NotImplementedException();
        }

        public object GetAll(int value, string includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Update(Rent rent)
        {
            _context.Update(rent);
        }
    }
}
