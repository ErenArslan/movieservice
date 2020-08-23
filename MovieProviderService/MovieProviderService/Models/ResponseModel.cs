using System.Collections.Generic;

namespace MovieProviderService.Models
{
    public  class ResponseModel
    {
        public int Page { get; set; }
        public List<MovieModel> Results { get; set; }
    }
}
