using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.GameSession;
using Core.Injector;
using TorqueGamesCore.Injector;
using UnityEngine;
using UnityEngine.Assertions;

namespace TorqueGamesCore.Character
{
    public class CharacterManager : GameComponent , ICharactersService
    {
        private IDependencyLinker _linker;
        public event Action<ICharactersService> OnCharacterManagerChanges;

        public Transform transformData;
        
        [SerializeField] private GameCharacter gameCharacterPrefab;
        
        public GameCharacter CurrentCharacter { get; private set; }

        public override void WriteDependencies(IDependencyLinker linker, IServicesInjector services)
        {
            _linker = linker;
            LoadGameSession(services);
            CurrentCharacter.WriteDependencies(_linker, services);
        }

        public override void EarlyInitialization(IServicesInjector gamePlayServices)
        {
            CurrentCharacter.EarlyInitialization(gamePlayServices);
        }

        public override async Task StartGame(IServicesInjector services)
        {
            await CurrentCharacter.StartGame(services);
            CurrentCharacter.gameObject.SetActive(true);
        }

        private void LoadGameSession(IServicesInjector services)
        {
            var gameSession = services.Get<IGameSession>();
            
            if (gameCharacterPrefab == null)
            {
                gameCharacterPrefab = gameSession.PrefabToSpawn;
                
            }

            CurrentCharacter = Instantiate(gameCharacterPrefab, transform, false);
            CurrentCharacter.transform.position = transformData.position;
            CurrentCharacter.transform.rotation = transformData.rotation;
            CurrentCharacter.SetupData(gameSession.InstanceData, services);
            transformData.gameObject.SetActive(false);
        }
        
    }

    public interface ICharactersService : IGameService // everything here must be readonly
    {
        event Action<ICharactersService> OnCharacterManagerChanges;
        GameCharacter CurrentCharacter { get; }
        
    }
}