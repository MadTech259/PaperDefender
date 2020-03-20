using System;
using System.Collections.Generic;
using TorqueGamesCore.Injector;

namespace Core.Injector
{
    internal class DependencyLinker : IDependencyLinker
    {
        private IDictionary<Type, IGameService> UniqueInstanceServices { get; }
        private IDictionary<Type, Getter<IGameService>> MultipleInstanceServices { get; }

        public DependencyLinker(IDictionary<Type, IGameService> uniqueInstanceServices, IDictionary<Type, Getter<IGameService>> multipleInstanceServices)
        {
            UniqueInstanceServices = uniqueInstanceServices;
            MultipleInstanceServices = multipleInstanceServices;
        }
        
        public ILinkWithClass<T> LinkInterface<T>() where T : IGameService
        {
            return new ClassLinker<T>(UniqueInstanceServices,MultipleInstanceServices);
        }
    }
}