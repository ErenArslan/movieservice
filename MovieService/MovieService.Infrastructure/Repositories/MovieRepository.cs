using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MovieService.Domain.AggregatesModels.Movie;
using MovieService.Infrastructure.Extensions;
using MovieService.Infrastructure.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieService.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IMediator _mediator;
        private readonly IMongoCollection<Movie> _mongoCollection;
     
        public MovieRepository(IOptions<MongoDbSettings> settings, IMediator mediator)
        {
            _mediator = mediator;

            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                var database = client.GetDatabase(settings.Value.Database);
                _mongoCollection = database.GetCollection<Movie>(typeof(Movie).Name.ToLower());

            }

         
        }
        public async Task<Movie> AddMovie(Movie movie)
        {
            await _mediator.DispatchDomainEventsAsync(movie);
            await _mongoCollection.InsertOneAsync(movie);

            return movie;
        }

        public async Task<Movie> Get(int id)
        {
            return await _mongoCollection.FindSync(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Movie>> GetList(int page = 1, int offset = 15)
        {
            return await _mongoCollection.Find(p => true).Skip((page - 1) * offset).Limit(offset).ToListAsync();
        }

        public async Task RemoveMovie(Movie movie)
        {
            await _mongoCollection.DeleteOneAsync(p => p.Id == movie.Id);
        }

        public async Task<Movie> Updateovie(Movie movie)
        {
            await _mediator.DispatchDomainEventsAsync(movie);

            await _mongoCollection.ReplaceOneAsync(p => p.Id == movie.Id, movie);
            return movie;
        }
    }
}
