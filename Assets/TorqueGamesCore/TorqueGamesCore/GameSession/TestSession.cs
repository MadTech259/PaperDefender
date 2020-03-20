using TorqueGamesCore.Character;
using UnityEngine;

namespace Core.GameSession
{
    public class TestSession : IGameSession
    {
        public GameCharacter PrefabToSpawn { get; set; }
        public Vector3 SpawnPoint { get; set; }
        public Quaternion SpawnRotation { get; set; }
        public ICharacterInstanceData InstanceData { get; set; }
    }
}