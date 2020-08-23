using MovieService.Domain.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieService.Domain.AggregatesModels.Movie
{
    public  interface IMovieRepository:IRepository<Movie>
    {
        Task<List<Movie>> GetList(int page=1,int offset=15);
        Task<Movie> Get(int id);
        Task AddMovie(Movie movie);
        Task UpdateMovie(Movie movie);

    }
}
