using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NewLife.Utility;

namespace NewLife.Models
{
    public class RentRepository : IRentRepository
    {
        private readonly UygulamaDbContext _uygulamaDbContext;

        public RentRepository(UygulamaDbContext uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public IEnumerable<Rent> GetAll()
        {
            return _uygulamaDbContext.Rent.Include(r => r.Car).Include(r => r.User).ToList();
        }

        public Rent Get(int id)
        {
            return _uygulamaDbContext.Rent.Find(id);
        }

        public void Add(Rent rent)
        {
            _uygulamaDbContext.Rent.Add(rent);
        }

        public void Update(Rent rent)
        {
            _uygulamaDbContext.Rent.Update(rent);
        }

        public void Save()
        {
            _uygulamaDbContext.SaveChanges();
        }

        public IEnumerable<Rent> GetAll(string includeProps)
        {
            throw new NotImplementedException();
        }

        public Rent? Get(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
