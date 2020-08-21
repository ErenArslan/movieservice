using MovieService.Domain.SeedWork;
using System.Threading.Tasks;

namespace MovieService.Domain.AggregatesModels.User
{
    interface IUserRepository:IRepository<User>
    {
        Task<User> Login(string identifier, string password);
        Task<User> Register(User user);

    }
}
