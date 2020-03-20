using System.Threading.Tasks;
using Core.GameSession;

namespace Core
{
    public interface IGameManager : IGameService
    {
        Task Initialize(IGameSession session);
    }
}