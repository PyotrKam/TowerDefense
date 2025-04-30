using SpaceShooter;
using UnityEngine;
using System;

namespace TowerDefence
{
    public class TDPlayer : Player
    {
        public static new TDPlayer Instance => Player.Instance as TDPlayer;
        
        private static event Action<int> OnGoldUpdate;
        private static event Action<int> OnCrystalUpdate;
        public static event Action<int> OnLifeUpdate;
        private const string CrystalKey = "PlayerCrystals";

        [SerializeField] private int m_gold = 0;
        [SerializeField] public int m_crystal;
        [SerializeField] private Tower m_towerPrefab;
        [SerializeField] private UpgradeAsset healtUpgrade;

        public int m_defaultCrystals = 55;

        public static void GoldUpdateUnsubscribe(Action<int> act)
        {
            OnGoldUpdate -= act;
        }
        public static void LiveUpdateUnSubscribe(Action<int> act)
        {
            OnLifeUpdate -= act;
        }
        public static void CrystalUpdateUnsubscribe(Action<int> act)
        {
            OnCrystalUpdate -= act;
        }

        public static void GoldUpdateSubscribe(Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instance.m_gold);
        }
        //Trying 25.6
        public static void CrystalUpdateSubscribe(Action<int> act)
        {
            if (OnCrystalUpdate == null)
                OnCrystalUpdate = delegate { };

            OnCrystalUpdate += act;

            
            if (Instance != null)
            {
                act(Instance.m_crystal);
            }
        }

        public static void LiveUpdateSubscribe(Action<int> act)
        {
            OnLifeUpdate += act;
            act(Instance.NumLives);
        }

        protected override void Awake()
        {
            base.Awake();

            var level = Upgrades.GetUpgradeLevel(healtUpgrade);
            TakeDamage(-level * 5);
            m_crystal = PlayerPrefs.GetInt(CrystalKey, m_crystal);
        }

        public void ChangeGold(int change)
        {
            m_gold += change;
            OnGoldUpdate?.Invoke(m_gold);
        }

        public void ReduceLife(int change)
        {
            TakeDamage(change);
            OnLifeUpdate?.Invoke(NumLives);
        }

        public void ChangeCrystals(int change)
        {
            if (change == 0) 
                return;

            m_crystal = Mathf.Max(0, m_crystal + change);
            PlayerPrefs.SetInt(CrystalKey, m_crystal);
            PlayerPrefs.Save();
            OnCrystalUpdate?.Invoke(m_crystal);
        }        

        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.goldCost);
            var tower = Instantiate(m_towerPrefab, buildSite.position, Quaternion.identity);
            tower.Use(towerAsset);            
            Destroy(buildSite.gameObject);
        }
               
    }
}

