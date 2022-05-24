using BeautySalonManagementSystem.RepositoryServices.EntityFramework;
using Microsoft.EntityFrameworkCore;

public class MyHostedService : IHostedService
{
    private readonly IDbContextFactory<BeautySalonContext> dbContextFactory;

    public MyHostedService(IDbContextFactory<BeautySalonContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        var passedAppointments = dbContext.ScheduledAppointments
                                 .Include(x => x.User)
                                 .Include(x => x.Treatment)
                                 .Where(x => x.Date.CompareTo(DateTime.Now) <= 0)
                                 .ToList();
        foreach(var a in passedAppointments)
        {
            if(a.State == AppointmentState.ACCEPTED)
            {
                dbContext.Remove(a);
            }
            else
            {
                var notification = new Notification()
                {
                    User = a.User,
                    Message = $"{a.User.FirstName}, Vaš zahtev za zakazivnje termina " +
                    $"{a.Date.ToShortDateString()} u {a.Date.ToShortTimeString()} časova, " +
                    $"za uslugu {a.Treatment.Name.ToUpper()}, NIJE prihvaćen. Molimo Vas da pokušate da" +
                    $" zakažete u nekom drugom terminu.",
                    State = NotificationState.UNREAD
                };
                dbContext.Add(notification);
                dbContext.Remove(a);
            }
        }
        dbContext.SaveChanges();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        //Cleanup logic here
    }
}