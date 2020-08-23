using DotNetCore.CAP;
using NotificationService.Api.IntegrationEvents.Events;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NotificationService.Api.IntegrationEvents.Handlers
{

    public class MovieRecommendedIntegrationEventHandler : ICapSubscribe
    {
        private readonly SmtpClient _smtpClient;
        public MovieRecommendedIntegrationEventHandler()
        {
            _smtpClient = new SmtpClient();
        }

        [CapSubscribe(nameof(MovieRecommendedIntegrationEvent))]
        public async Task Handle(MovieRecommendedIntegrationEvent @event)
        {

            await _smtpClient.SendMailAsync(new MailMessage(
                  to:@event.TargetEmail,
                  from:"eren.arslan6@gmail.com",
                  subject: "Movie Recommendation - "+@event.Title,
                  body: $"{@event.Title} is perfect. You should watch. Overview:  {@event.Overview} . "
                  ));
        }
    }
}
