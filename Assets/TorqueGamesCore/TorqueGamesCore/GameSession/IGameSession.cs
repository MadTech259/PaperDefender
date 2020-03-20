using TorqueGamesCore.Character;
using UnityEngine;

namespace Core.GameSession
{
    public interface IGameSession : IGameService
    {
        GameCharacter PrefabToSpawn { get; }
        Vector3 SpawnPoint { get; }
        Quaternion SpawnRotation { get; }
        ICharacterInstanceData InstanceData { get; set; }
    }
}