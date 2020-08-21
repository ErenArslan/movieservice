using MovieService.Domain.SeedWork;
using System.Threading.Tasks;

namespace MovieService.Domain.AggregatesModels.Movie
{
    public  interface IMovieRepository:IRepository<Movie>
    {
        Task<Movie> GetList(int page=1,int offset=15);
        Task<Movie> Get(int id);
        Task<Movie> AddMovie(Movie movie);
        Task<Movie> Updateovie(Movie movie);
        Task RemoveMovie(Movie movie);

    }
}
