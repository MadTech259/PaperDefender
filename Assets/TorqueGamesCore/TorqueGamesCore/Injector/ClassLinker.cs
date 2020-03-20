using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Injector
{
    internal class ClassLinker<TInterfaceType> : ILinkWithClass<TInterfaceType> where TInterfaceType : IGameService
    {
        private IDictionary<Type, IGameService> UniqueInstanceServices { get; }
        private IDictionary<Type, Getter<IGameService>> MultipleInstanceServices { get; }

        public ClassLinker(IDictionary<Type, IGameService> uniqueInstanceServices, IDictionary<Type, Getter<IGameService>> multipleInstanceServices)
        {
            UniqueInstanceServices = uniqueInstanceServices;
            MultipleInstanceServices = multipleInstanceServices;
        }

        public ILinkType<TInterfaceType, T> WithImplementation<T>() where T : class, TInterfaceType, new()
        {
            return new LinkType<TInterfaceType, T>(UniqueInstanceServices,MultipleInstanceServices);
        }

        public void WithGivenInstance<T>(T instance) where T : TInterfaceType
        {
            UniqueInstanceServices[typeof(TInterfaceType)] = instance;
        }

        public void WithUnityComponentUniqueInstance<T>() where T : Component, TInterfaceType
        {
            UniqueInstanceServices[typeof(TInterfaceType)] = Object.FindObjectOfType<T>();
        }
    }
}