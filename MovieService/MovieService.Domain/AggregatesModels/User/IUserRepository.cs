﻿using MovieService.Domain.SeedWork;
using System.Threading.Tasks;

namespace MovieService.Domain.AggregatesModels.User
{
    interface IUserRepository:IRepository<User>
    {
        Task<User> Login(string username, string password);
        Task<User> Register(User user);

    }
}