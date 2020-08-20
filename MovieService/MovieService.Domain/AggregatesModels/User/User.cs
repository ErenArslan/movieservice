using MovieService.Domain.SeedWork;
using System;
using System.Diagnostics.CodeAnalysis;

namespace MovieService.Domain.AggregatesModels.User
{
    public class User:Entity<Guid>,IAggregateRoot
    {
        public string Fullname { get;private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        private User() { }

        public User([NotNull]Guid id,[NotNull]string fullname,[NotNull]string username,[NotNull]string email,[NotNull]string password)
        {
           
            Id = Check.NotNull(value: id, nameof(id));
            Fullname = Check.NotNullOrWhiteSpace(value: fullname, nameof(fullname));
            Username = Check.NotNullOrWhiteSpace(value: username, nameof(username));
            Email= Check.NotNullOrWhiteSpace(value: email, nameof(email));
            Password = Check.NotNullOrWhiteSpace(value: password, nameof(password));

        }


        public void SetFullname([NotNull]string fullname)
        {
            Fullname = Check.NotNullOrWhiteSpace(value: fullname, nameof(fullname));
        }
        public void SetUsername([NotNull]string username)
        {
            Username = Check.NotNullOrWhiteSpace(value: username, nameof(username));
        }
        public void SetEmail([NotNull]string email)
        {
            Email = Check.NotNullOrWhiteSpace(value: email, nameof(email));
        }
        public void SetPasswordHash([NotNull]string password)
        {
            Password = Check.NotNullOrWhiteSpace(value: password, nameof(password));
        }

    }
}
