using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class TDPlayer : Player
    {
        [SerializeField] private int m_gold = 0;

        public void ChangeGold(int change)
        {
            m_gold += change;

        }
    }
}

