using MediatR;


namespace Common.Domain.Base;

public class BaseDomainEvent: INotification
{
    public DateTime CreationDate { get; protected set; }
    public BaseDomainEvent()
    {
        CreationDate = DateTime.Now;
    }
}
