using System;
using System.Collections.Generic;
using TorqueGamesCore.Injector;

namespace Core.Injector
{
    public abstract class TorqueGamesInjector : IServicesInjector
    {
        private class TorqueGamesInjectorImplementation : TorqueGamesInjector {}
        public static TorqueGamesInjector GetEmpty()
        {
            return new TorqueGamesInjectorImplementation();
        }
        
        private Dictionary<Type, IGameService> UniqueInstanceServices { get; }
        
        private Dictionary<Type, Getter<IGameService>> MultipleInstanceServices { get; }
        
        protected IDependencyLinker Linker { get; }
        
        
        protected TorqueGamesInjector()
        {
            UniqueInstanceServices = new Dictionary<Type, IGameService>();
            MultipleInstanceServices = new Dictionary<Type, Getter<IGameService>>();
            Linker = new DependencyLinker(UniqueInstanceServices,MultipleInstanceServices);
        }

        public IDependencyLinker GetLinker() => Linker;
        
        public void Init() => Register();

        /// <summary>
        /// override that function to link the implementations with the interfaces
        /// example
        /// <code>
        /// Linker
        ///     .LinkInterface<IPlayerService>()
        ///     .WithImplementation<PlayerServiceImplementation>()
        ///     .AsUniqueInstance();
        /// or
        /// Linker
        ///     .LinkInterface<IPlayerService>()
        ///     .WithImplementation<PlayerServiceImplementation>()
        ///     .AsMultipleInstances();
        /// </code>
        /// </summary>
        protected virtual void Register()
        {
            
        }
        
        public T Get<T>() where T : IGameService
        {
            var t = typeof(T);
            if (UniqueInstanceServices.TryGetValue(t, out var service))
            {
                return (T) service;
            }
            if (MultipleInstanceServices.TryGetValue(t, out var serviceGetter))
            {
                return (T) serviceGetter();
            }
            
            // var implementation = SearchImplementation<T>(t);
// 
            // if (implementation != null)
            // {
            //     // cache that
            //     var gameService = (T) Activator.CreateInstance(implementation);
            //     UniqueInstanceServices[t] = gameService; // potential boxing allocation
            //     return gameService;
            // }
            
            return default(T);
        }
        
        //todo : buscar un modo mas eficiente de encontrar la implementacion
        // por ahora registrarla y ya
        private static Type SearchImplementation<T>(Type t) where T : IGameService
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsAssignableFrom(t) && type.IsClass)
                    {
                        return type;
                    }
                }
            }
            return null;
        }
    }
}