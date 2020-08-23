using DotNetCore.CAP;
using Microsoft.Extensions.Configuration;
using MovieProviderService.Events;
using MovieProviderService.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MovieProviderService
{
    public class MovieFetcherService : HostedService
    {
        private readonly ICapPublisher _eventBus;
        HttpClient restClient;
        private readonly IConfiguration _configuration;
        public MovieFetcherService(IConfiguration configuration, ICapPublisher eventBus)
        {
            _eventBus = eventBus;
            restClient = new HttpClient();
            _configuration = configuration;
        }
        protected override async Task ExecuteAsync(CancellationToken cToken)
        {
            while (!cToken.IsCancellationRequested)
            {
                var apiKey = _configuration.GetSection("ApiKey").Value;
                var fetcherTimerMinute = int.Parse(_configuration.GetSection("FetcherTimerMinute").Value);
                var fetcherTotalMovie = int.Parse(_configuration.GetSection("FetcherTotalPage").Value);

                for (int i = 1; i <= fetcherTotalMovie; i++)
                {
                    var response = await restClient.GetAsync($"https://api.themoviedb.org/3/movie/popular?api_key={apiKey}&language=en-US&page={i}", cToken);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ResponseModel>(responseJson);
                        var events = result.Results
                            .Select(p => new MovieFetchedIntegrationEvent(p.Id, p.popularity, p.poster_path, p.adult, p.backdrop_path, p.original_language, p.original_title, p.title, p.overview, p.releaseDate)).ToList();
                        events.ForEach(async integrationEvent =>
                       {
                           await _eventBus.PublishAsync<MovieFetchedIntegrationEvent>(typeof(MovieFetchedIntegrationEvent).Name, integrationEvent);

                       });


                    }
                }





                await Task.Delay(TimeSpan.FromMinutes(fetcherTimerMinute), cToken);
            }
        }
    }
}