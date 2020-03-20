using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.GameSession;
using Core.Injector;
using TorqueGamesCore.Character;
using TorqueGamesCore.Injector;
using UnityEngine;

namespace Core
{
    //central component of the game node
    public class GameManager : MonoBehaviour , IGameManager
    {
        public GameObject gameNodeParent;//instanced prefab with all the game as child

        private readonly HashSet<GameComponent> gameComponents = new HashSet<GameComponent>();
        
        private TorqueGamesInjector _injector;
        private IGameSession _session;
        private ICharacterFactory _factory;

        
        
        private async void Awake()
        {
            await Initialize(new TestSession());
        }
        
        public async Task Initialize(IGameSession session)
        {
            _session = session;
            
            
            gameNodeParent.SetActive(false);
            await EarlyInitializationProcess();//run all initializations
            gameNodeParent.SetActive(true);//and then run game node
        }
        private async Task EarlyInitializationProcess()
        {
            await Task.Yield();
            //initialice injector
            var currentGameInjector = new CoronavirusInjector(); 
            var linker = currentGameInjector.GetLinker();
            _factory = new CharacterFactory(currentGameInjector, linker);
            LoadDependencies(linker);
            _injector = currentGameInjector;
            _injector.Init();
            
            //write dependencies from decoupled registers
            foreach (var register in gameNodeParent.GetComponentsInChildren<IComponentRegister>())
            {
                register.AddLinks(linker);
            }
            
            //retrieve children
            foreach (var component in gameNodeParent.GetComponentsInChildren<GameComponent>())
            {
                gameComponents.Add(component);
            }
            
            //await game components initialization
            foreach (var gameComponent in gameComponents)
            {
                gameComponent.WriteDependencies(linker, _injector);
            }
            
            foreach (var gameComponent in gameComponents)
            {
                gameComponent.EarlyInitialization(_injector);
            }

            var initTasks = gameComponents.Select(g => g.StartGame(_injector)).ToArray();
            await Task.WhenAll(initTasks);
        }

        private void LoadDependencies(IDependencyLinker linker)
        {
            linker
                .LinkInterface<IGameManager>()
                .WithGivenInstance(this);
            linker
                .LinkInterface<IGameSession>()
                .WithGivenInstance(_session);
            linker
                .LinkInterface<ICharacterFactory>()
                .WithGivenInstance(_factory);      
        

        }
    }

    public class CoronavirusInjector : TorqueGamesInjector
    {
        protected override void Register()
        {
            
        }
    }
}