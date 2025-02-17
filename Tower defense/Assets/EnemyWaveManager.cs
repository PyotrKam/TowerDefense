using System;
using UnityEngine;

namespace TowerDefence
{
    public class EnemyWaveManager : MonoBehaviour
    {
        [SerializeField] private Enemy m_EnemyPrefab;
        [SerializeField] private Path[] paths;
        [SerializeField] private EnemyWave currentWave;

        private void Start()
        {            
            currentWave.Prepare(SpawnEnemies);

        }
        public void ForceNextWave()
        {
            TDPlayer.Instance.ChangeGold((int) currentWave.GetRemainigTime());
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            foreach ((EnemyAsset asset, int count, int pathIndex) in currentWave.EnumerateSquads())
            {
                if (pathIndex < paths.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var e = Instantiate(m_EnemyPrefab, paths[pathIndex].StartArea.RandomInsideZone, Quaternion.identity);
                        e.Use(asset);
                        e.GetComponent<TDPatrolController>().SetPath(paths[pathIndex]);
                    }
                                                        
                }
                else
                {
                    Debug.LogWarning($"Invalid pathIndex in {name}");
                }                
            }
                        
            currentWave = currentWave.PrepareNext(SpawnEnemies);            
        }

       
    }
}

