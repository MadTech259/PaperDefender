using UnityEngine;

namespace Core.Injector
{
    public interface ILinkWithClass<TInterfaceType> where TInterfaceType : IGameService
    {
        ILinkType<TInterfaceType,T> WithImplementation<T>() where T : class, TInterfaceType, new();
        void WithGivenInstance<T>(T instance) where T : TInterfaceType;
        void WithUnityComponentUniqueInstance<T>() where T : Component, TInterfaceType;
    }
}