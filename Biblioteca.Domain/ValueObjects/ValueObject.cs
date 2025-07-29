using System.Collections.Generic;
using Flunt.Notifications;

namespace Biblioteca.Domain.ValueObjects;

public record class ValueObject : INotifiable
{
    public void AddNotifications(IEnumerable<Notification> notifications)
    {
        throw new System.NotImplementedException();
    }
}