namespace Quran.Server.Domain.Common
{
    public class BaseEntity<T>:IBaseEntity<T>
    {
        public T Id { get; set; }
    }
}