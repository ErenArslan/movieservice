using JsonNet.PrivateSettersContractResolvers;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MovieService.Domain.AggregatesModels.Movie;
using MovieService.Infrastructure.Extensions;
using MovieService.Infrastructure.Settings;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieService.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IMediator _mediator;
        private readonly IMongoCollection<Movie> _mongoCollection;
        private readonly IDistributedCache _distributedCache;
        public MovieRepository(IOptions<MongoDbSettings> settings, IMediator mediator, IDistributedCache distributedCache)
        {
            _mediator = mediator;
            _distributedCache = distributedCache;


            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                var database = client.GetDatabase(settings.Value.Database);
                _mongoCollection = database.GetCollection<Movie>(typeof(Movie).Name.ToLower());

            }


        }
        public async Task AddMovie(Movie movie)
        {
            if ((await Get(movie.Id)) == null)
            {
                await _mediator.DispatchDomainEventsAsync(movie);
                await _mongoCollection.InsertOneAsync(movie);
            }


        }

        public async Task<Movie> Get(int id)
        {
            Movie movie = null;
            var cache = await _distributedCache.GetStringAsync(id.ToString());
            if (cache == null)
            {
                movie = await _mongoCollection.FindSync(p => p.Id == id).FirstOrDefaultAsync();
                if (movie != null)
                {
                    await _distributedCache.SetStringAsync(id.ToString(), JsonConvert.SerializeObject(movie));
                }

            }
            else
            {
                movie = JsonConvert.DeserializeObject<Movie>(cache, new JsonSerializerSettings
                {
                    ContractResolver = new ReadonlyJsonDefaultContractResolver()
                     

                });

            }

            return movie;

            //  return await _mongoCollection.FindSync(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Movie>> GetList(int page = 1, int offset = 15)
        {
            return await _mongoCollection.Find(p => true).Skip((page - 1) * offset).Limit(offset).ToListAsync();
        }

   
        public async Task UpdateMovie(Movie movie)
        {
            await _mediator.DispatchDomainEventsAsync(movie);

            await _mongoCollection.ReplaceOneAsync(p => p.Id == movie.Id, movie);
        }
    }
}
