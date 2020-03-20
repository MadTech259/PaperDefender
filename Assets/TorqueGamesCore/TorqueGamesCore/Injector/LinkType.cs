using System;
using System.Collections.Generic;

namespace Core.Injector
{
    internal class LinkType<TInterfaceType, TImplementationType> : ILinkType<TInterfaceType, TImplementationType> where TImplementationType : class, TInterfaceType, new() where TInterfaceType : IGameService
    {
        private IDictionary<Type, IGameService> UniqueInstanceServices { get; }
        private IDictionary<Type, Getter<IGameService>> MultipleInstanceServices { get; }

        public LinkType(IDictionary<Type, IGameService> uniqueInstanceServices, IDictionary<Type, Getter<IGameService>> multipleInstanceServices)
        {
            UniqueInstanceServices = uniqueInstanceServices;
            MultipleInstanceServices = multipleInstanceServices;
        }

        public TInterfaceType AsUniqueInstance()
        {
            var instance = new TImplementationType();
            UniqueInstanceServices[typeof(TInterfaceType)] = instance;
            return instance;
        }

        public void AsMultipleInstances()
        {
            MultipleInstanceServices[typeof(TInterfaceType)] = ()=> new TImplementationType();
        }
    }
}