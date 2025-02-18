using UnityEngine;
using SpaceShooter;

namespace TowerDefence
{
    public class LevelWaveCondition : MonoBehaviour, ILevelCondition
    {
        private bool isCompleted;

         void Start()
        {
            FindObjectOfType<EnemyWaveManager>().OnAllWavesDead += () =>
            {
                isCompleted = true;
            };
        }
        public bool IsCompleted { get { return isCompleted; } }
    }

}
