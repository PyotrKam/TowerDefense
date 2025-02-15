using System;
using UnityEngine;
using System.Collections.Generic;


namespace TowerDefence
{
    public class EnemyWave: MonoBehaviour
    {
        [Serializable]
        private class Squad
        {
            public EnemyAsset asset;
            public int count;
        }

        [Serializable]
        private class PathGroup
        {
            public Squad[] squads;
        }

        [SerializeField] private PathGroup[] groups;

        [SerializeField] private float prepareTime = 10f;

        private void Awake()
        {
            enabled = false;
        }

        private event Action OnWaveReady;

        public void Prepare(Action spawnEnemies)
        {
            prepareTime += Time.time;
            enabled = true;
            OnWaveReady += spawnEnemies;
        }

        private void Update()
        {
            if (Time.time >= prepareTime)
            {
                enabled = false;
                OnWaveReady?.Invoke();
            }
        }

        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquads()
        {
            yield return (groups[0].squads[0].asset, groups[0].squads[0].count, 0);
        }

        internal EnemyWave PrepareNext(Action spawnEnemies)
        {
            return null;
        }
    }
}