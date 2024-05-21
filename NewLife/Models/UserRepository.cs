using NewLife.Utility;

namespace NewLife.Models
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        public UserRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Save()
        {
            _uygulamaDbContext.SaveChanges();
        }

        public void Update(User user)
        {
            _uygulamaDbContext.Update(user);
        }
    }
}
