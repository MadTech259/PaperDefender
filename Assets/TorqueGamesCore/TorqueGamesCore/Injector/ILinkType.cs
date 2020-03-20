namespace Core.Injector
{
    public interface ILinkType<TInterfaceType, TImplementationType> where TInterfaceType : IGameService where TImplementationType : class, TInterfaceType, new() 
    {
        /// <summary>
        /// Kind of singleton, always the sema instance when you call Get()
        /// </summary>
        TInterfaceType AsUniqueInstance();
        /// <summary>
        /// Get a different instance each time you call Get()
        /// </summary>
        void AsMultipleInstances();
    }
}