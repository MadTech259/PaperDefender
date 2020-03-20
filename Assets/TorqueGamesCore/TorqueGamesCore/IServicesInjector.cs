namespace Core
{
    public interface IServicesInjector
    {
        T Get<T>() where T : IGameService;
    }
}