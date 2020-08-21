using System;

namespace MovieService.Domain.SeedWork
{
    public interface IGuidGenerator
    {
        Guid Create();
    }
}
