namespace Common.Domain.Base;

public class BaseEntity
{
    public long Id { get;protected set; }
    public DateTime CreationDate { get; set; }

    public BaseEntity()
    {
        CreationDate = DateTime.Now;
    }
}
