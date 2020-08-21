using System;

namespace MovieService.Domain.SeedWork
{
    public class GuidGenerator : IGuidGenerator
    {
        public Guid Create()
        {
            return Guid.NewGuid();
        }
    }
}
