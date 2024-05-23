using System.Collections.Generic;

namespace NewLife.Models
{
    public interface IRentRepository
    {
        IEnumerable<Rent> GetAll(string includeProps);
        Rent Get(int id);
        void Add(Rent rent);
        void Update(Rent rent);
        void Save();
        Rent? Get(Func<object, bool> value);
    }
}
