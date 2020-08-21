using MediatR;
using MovieService.Domain.AggregatesModels.User;

namespace MovieService.Domain.Events
{
    public  class UserCreatedDomainEvent :INotification
    {
        public User User { get;private set; }

        public UserCreatedDomainEvent(User user)
        {
            User = user;
        }
    }
}
