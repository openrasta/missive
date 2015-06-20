using System.Threading.Tasks;

namespace Missive
{
    public interface ISubscriber<T>
    {
        Task Handle(T message);
    }
}