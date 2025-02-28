using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{    
    [CreateAssetMenu]
    public sealed class UpgradeAsset : ScriptableObject
    {
        [Header("Appearance")]
        public Sprite sprite;

        public int[] costByLevel = { 3 };
    }
}