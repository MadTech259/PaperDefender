using System.Threading.Tasks;
using Core;
using Core.Injector;
using TorqueGamesCore.Injector;
using UnityEngine;
using Utilities;

namespace TorqueGamesCore.Character
{
    public interface ICharacterFactory : IGameService
    {
        T Spawn<T>(T prefab, ICharacterInstanceData instanceData = null) where T : GameCharacter;
        T Initialize<T>(GameObject characterGO, ICharacterInstanceData instanceData) where T : GameCharacter;
    }

    public class CharacterFactory : ICharacterFactory
    {
        private readonly IServicesInjector _gameInjector;
        private readonly IDependencyLinker _gamePlayLinker;

        public CharacterFactory(IServicesInjector gameInjector, IDependencyLinker gamePlayLinker)
        {
            _gameInjector = gameInjector;
            _gamePlayLinker = gamePlayLinker;
        }

        public T Initialize<T>(GameObject characterGO, ICharacterInstanceData instanceData) where T : GameCharacter
        {
            var character = characterGO.GetOrCreate<T>();
            character.SetupData(instanceData, _gameInjector);
            
            character.EarlyInitialization(_gameInjector);
            Task.WaitAll(character.StartGame(_gameInjector));
            character.gameObject.SetActive(true);
            return character;

        }
        
        public T Spawn<T>(T prefab, ICharacterInstanceData instanceData = null) where T : GameCharacter
        {
            var character = Object.Instantiate(prefab);
            character.SetupData(instanceData, _gameInjector);
            
            character.EarlyInitialization(_gameInjector);
            Task.WaitAll(character.StartGame(_gameInjector));
            character.gameObject.SetActive(true);

            return character;
        }
    }
}