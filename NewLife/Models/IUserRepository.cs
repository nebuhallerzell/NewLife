namespace NewLife.Models
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User user);
        void Save();
    }
}
